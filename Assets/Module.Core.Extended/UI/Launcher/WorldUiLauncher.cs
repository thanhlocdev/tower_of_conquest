#if (UNITASK || UNITY_6000_0_OR_NEWER) && ZBASE_UNITY_SCREEN_NAVIGATOR

using System;
using System.Diagnostics.CodeAnalysis;
using Cysharp.Threading.Tasks;
using Module.Core.Extended.PubSub;
using Module.Core.PubSub;
using Module.Core.Vaults;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core;
using ZBase.UnityScreenNavigator.Core.Activities;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Windows;

namespace Module.Core.Extended.UI
{
    public sealed class WorldUiLauncher : UnityScreenNavigatorLauncher
    {
        public static readonly Id<WorldUiLauncher> PresetId = default;

        [SerializeField]
        private WindowContainerId _screenId = new("ScreenContainer", 1);

        [SerializeField]
        private WindowContainerId _modalId = new("ModalContainer", 2);

        [SerializeField]
        private WindowContainerId _activityId = new("ActivityContainer", 3);

        [SerializeField]
        private WindowContainerId _toastId = new("ToastContainer", 4);

        [SerializeField]
        private ResourceKey.Serializable _loadingActivityKey;

        [SerializeField]
        private ResourceKey.Serializable _toastActivityKey;

        private WorldUiLauncherCallback[] _callbacks;
        private ScreenContainer _screenContainer;
        private ModalContainer _modalContainer;
        private ActivityContainer _activityContainer;
        private ActivityContainer _toastContainer;

        protected override void OnAwake()
        {
            _callbacks = GetComponents<WorldUiLauncherCallback>();

            var callbacks = _callbacks.AsSpan();

            foreach (var callback in callbacks)
            {
                if (callback) callback.OnAwake(this);
            }
        }

        protected override void OnPreCreateContainers()
        {
            var callbacks = _callbacks.AsSpan();

            foreach (var callback in callbacks)
            {
                if (callback) callback.OnPreCreateContainers(this);
            }
        }

        protected override void OnCreateContainer(
              [NotNull] WindowContainerConfig config
            , [NotNull] WindowContainerBase container
        )
        {
            switch (config.containerType)
            {
                case WindowContainerType.Screen:
                {
                    if (container.TryAddToGlobalObjectVaultAs(_screenId, ref _screenContainer))
                    {
                        return;
                    }
                    break;
                }

                case WindowContainerType.Modal:
                {
                    if (container.TryAddToGlobalObjectVaultAs(_modalId, ref _modalContainer))
                    {
                        return;
                    }
                    break;
                }

                case WindowContainerType.Activity:
                {
                    if (container.TryAddToGlobalObjectVaultAs(_activityId, ref _activityContainer)
                        || container.TryAddToGlobalObjectVaultAs(_toastId, ref _toastContainer)
                    )
                    {
                        return;
                    }
                    break;
                }
            }

            var callbacks = _callbacks.AsSpan();

            foreach (var callback in callbacks)
            {
                if (callback && callback.OnCreateContainer(this, config, container))
                {
                    // 1 callback only process 1 container
                    // so this must be `return`, not `continue`.
                    return;
                }
            }
        }

        protected override void OnPostCreateContainers()
        {
            Initialize().Forget();
        }

        private async UniTaskVoid Initialize()
        {
            if (_loadingActivityKey.IsValid)
            {
                var loadingOptions = new ActivityOptions(_loadingActivityKey, false);
                await _activityContainer.ShowAsync(loadingOptions);
            }

            if (_toastActivityKey.IsValid)
            {
                var toastOptions = new ActivityOptions(_toastActivityKey, false);
                await _toastContainer.ShowAsync(toastOptions);
            }

            var callbacks = _callbacks;

            if (callbacks.AsSpan().Length > 0)
            {
                var tasks = new UniTask[callbacks.Length];

                for (var i = 0; i < callbacks.Length; i++)
                {
                    var callback = callbacks[i];
                    tasks[i] = callback ? callback.OnInitializeAsync(this) : UniTask.CompletedTask;
                }

                await UniTask.WhenAll(tasks);
            }

            var subscriber = WorldMessenger.Subscriber.UIScope().WithState(this);

            subscriber.Subscribe<AsyncMessage<ShowScreenMessage>>(static (state, msg) => state.HandleAsync(msg));
            subscriber.Subscribe<ShowScreenMessage>(static (state, msg) => state.Handle(msg));
            subscriber.Subscribe<HideScreenMessage>(static (state, msg) => state.Handle(msg));

            subscriber.Subscribe<AsyncMessage<ShowModalMessage>>(static (state, msg) => state.HandleAsync(msg));
            subscriber.Subscribe<ShowModalMessage>(static (state, msg) => state.Handle(msg));
            subscriber.Subscribe<HideModalMessage>(static (state, msg) => state.Handle(msg));

            subscriber.Subscribe<AsyncMessage<HideAllModalMessage>>(static (state, msg) => state.HandleAsync(msg));
            subscriber.Subscribe<HideAllModalMessage>(static (state, msg) => state.Handle(msg));

            subscriber.Subscribe<AsyncMessage<ShowActivityMessage>>(static (state, msg) => state.HandleAsync(msg));
            subscriber.Subscribe<ShowActivityMessage>(static (state, msg) => state.Handle(msg));
            subscriber.Subscribe<HideActivityMessage>(static (state, msg) => state.Handle(msg));

            GlobalObjectVault.TryAdd(PresetId, this);
            GlobalValueVault<bool>.TrySet(PresetId, true);
        }


        protected override void OnDestroy()
        {
            GlobalValueVault<bool>.TrySet(PresetId, false);
            GlobalObjectVault.TryRemove(PresetId, out _);
        }

        private async UniTask HandleAsync(AsyncMessage<ShowScreenMessage> asyncMessage)
        {
            if (_screenContainer == false) return;

            var message = asyncMessage.Message;
            var options = new ScreenOptions(
                  message.ResourcePath
                , message.PlayAnimation
                , loadAsync: message.LoadAsync
            );

            await _screenContainer.PushAsync(options, message.Args);
        }

        private void Handle(ShowScreenMessage message)
        {
            if (_screenContainer == false) return;

            var options = new ScreenOptions(
                message.ResourcePath
                , message.PlayAnimation
                , loadAsync: message.LoadAsync
            );

            _screenContainer.Push(options, message.Args);
        }

        private void Handle(HideScreenMessage message)
        {
            if (_screenContainer == false) return;

            _screenContainer.Pop(message.PlayAnimation);
        }

        private async UniTask HandleAsync(AsyncMessage<ShowModalMessage> asyncMessage)
        {
            if (_modalContainer == false) return;

            var message = asyncMessage.Message;
            var options = new ModalOptions(
                  message.ResourcePath
                , message.PlayAnimation
                , loadAsync: message.LoadAsync
            );

            await _modalContainer.PushAsync(options, message.Args);
        }

        private void Handle(ShowModalMessage message)
        {
            if (_modalContainer == false) return;

            var options = new ModalOptions(
                  message.ResourcePath
                , message.PlayAnimation
                , loadAsync: message.LoadAsync
                , backdropAlpha: message.BackdropAlpha
            );

            _modalContainer.Push(options, message.Args);
        }

        private void Handle(HideModalMessage message)
        {
            if (_modalContainer == false) return;

            _modalContainer.Pop(message.PlayAnimation);
        }

        private async UniTask HandleAsync(AsyncMessage<HideAllModalMessage> asyncMessage)
        {
            if (_modalContainer == false) return;
            if (_modalContainer.Modals == null) return;

            var message = asyncMessage.Message;

            if (message.Count <= 0)
            {
                while (_modalContainer.Modals.Count > 0)
                {
                    await _modalContainer.PopAsync(message.PlayAnimation);
                }
                return;
            }

            var count = 0;

            while (_modalContainer.Modals.Count > 0 && count < message.Count)
            {
                await _modalContainer.PopAsync(message.PlayAnimation);
                count++;
            }
        }

        private void Handle(HideAllModalMessage message)
        {
            if (_modalContainer == false) return;

            if (_modalContainer.Modals != null)
            {
                HideAll(message.Count).Forget();
            }

            async UniTaskVoid HideAll(int hideCount)
            {
                if (hideCount <= 0)
                {
                    while (_modalContainer.Modals.Count > 0)
                    {
                        await _modalContainer.PopAsync(message.PlayAnimation);
                    }
                    return;
                }

                var count = 0;

                while (_modalContainer.Modals.Count > 0 && count < message.Count)
                {
                    await _modalContainer.PopAsync(message.PlayAnimation);
                    count++;
                }
            }
        }

        private async UniTask HandleAsync(AsyncMessage<ShowActivityMessage> asyncMessage)
        {
            if (_activityContainer == false) return;

            var message = asyncMessage.Message;
            var options = new ActivityOptions(message.ResourcePath, message.PlayAnimation);
            await _activityContainer.ShowAsync(options, message.Args);
        }

        private void Handle(ShowActivityMessage message)
        {
            if (_activityContainer == false) return;

            var options = new ActivityOptions(message.ResourcePath, message.PlayAnimation);
            _activityContainer.Show(options, message.Args);
        }

        private void Handle(HideActivityMessage message)
        {
            if (_activityContainer == false) return;

            _activityContainer.Hide(message.ResourcePath, message.PlayAnimation);
        }
    }
}

#endif
