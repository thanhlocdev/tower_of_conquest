#if (UNITASK || UNITY_6000_0_OR_NEWER) && ZBASE_UNITY_SCREEN_NAVIGATOR

using System;
using Cysharp.Threading.Tasks;
using Module.Core.Extended.PubSub;
using Module.Core.Extended.Timing;
using Module.Core.Vaults;
using TMPro;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Activities;

namespace Module.Core.Extended.UI
{
    public partial class LoadingActivity : Activity
    {
        private const string THREE_DOT = "...";

        [SerializeField] private RectTransform _circle;
        [SerializeField] private TMP_Text _text;

        private Timer _timer;

        protected override void Awake()
        {
            _timer = new Timer(this);
        }

        public override UniTask Initialize(Memory<object> args)
        {
            _text.text = $"Loading{THREE_DOT}";
            return UniTask.CompletedTask;
        }

        public override async UniTask WillEnter(Memory<object> args)
        {
            WorldMessenger.Publisher.UIScope()
                .Publish(new BeginLoadingActivityMessage());

            _timer.OnTimeZero();

            await GlobalValueVault<bool>.WaitUntil(WorldTimer.PresetId, true);

            WorldMessenger.Publisher.TimerScope()
                .Publish(RegisterToTimerMessage.From(_timer));
        }

        public override async UniTask WillExit(Memory<object> args)
        {
            await WorldMessenger.Publisher.UIScope()
                .PublishAsync(new CompleteLoadingActivityMessage());
        }

        public override void DidExit(Memory<object> args)
        {
            WorldMessenger.Publisher.TimerScope()
                .Publish(UnregisterToTimerMessage.From<Timer>());
        }

        private void SetProgress(float value)
        {
            var textLength = _text.text.Length;
            var dotsLength = THREE_DOT.Length;
            _text.maxVisibleCharacters = textLength - dotsLength + Mathf.RoundToInt(value * dotsLength);

            var z = -Mathf.Lerp(-180f, 180f, value);
            _circle.localEulerAngles = new Vector3(0f, 0f, z);
        }

        private class Timer : ITimer
        {
            private readonly LoadingActivity _target;
            private readonly TimeSpan _interval;
            private TimeSpan _time;

            public Timer(LoadingActivity target)
            {
                _target = target;
                _interval = TimeSpan.FromSeconds(1f);
            }

            public TimeSpan RemainTime
            {
                get => _time;

                set
                {
                    _time = value;
                    _target.SetProgress(1f - (float)(value.TotalSeconds / _interval.TotalSeconds));
                }
            }

            public TimeSpan Interval
            {
                get => _interval;
            }

            public bool Enabled => true;

            public void OnTimeZero()
            {
                RemainTime = Interval;
            }
        }
    }
}

#endif
