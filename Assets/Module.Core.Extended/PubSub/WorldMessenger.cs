#if UNITASK || UNITY_6000_0_OR_NEWER

using Module.Core.PubSub;
using UnityEngine;

namespace Module.Core.Extended.PubSub
{
    public static class WorldMessenger
    {
        public static MessagePublisher Publisher => Instance.MessagePublisher;

        public static MessageSubscriber Subscriber => Instance.MessageSubscriber;

        public static AnonPublisher AnonPublisher => Instance.AnonPublisher;

        public static AnonSubscriber AnonSubscriber => Instance.AnonSubscriber;

        private static Messenger Instance => s_instance ??= new();

        private static Messenger s_instance;

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Init()
        {
            s_instance?.Dispose();
            s_instance = null;
        }
#endif
    }
}

#endif
