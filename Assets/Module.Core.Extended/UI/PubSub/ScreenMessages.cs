#if (UNITASK || UNITY_6000_0_OR_NEWER) && ZBASE_UNITY_SCREEN_NAVIGATOR

namespace Module.Core.Extended.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using Module.Core.PubSub;
    using UnityEngine;

    public readonly record struct ShowScreenMessage(
          string ResourcePath
        , bool PlayAnimation
        , bool LoadAsync
        , Memory<object> Args
    ) : IMessage;

    public readonly record struct HideScreenMessage(bool PlayAnimation) : IMessage;

    public static class ResourceKeyScreenMessageExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ShowScreenMessage ToShowScreenMessage(
              this ResourceKey<GameObject> key
            , bool playAnimation = false
            , bool loadAsync = true
            , params object[] args
        )
        {
            return new(key, playAnimation, loadAsync, args);
        }
    }
}

#if UNITY_ADDRESSABLES

namespace Module.Core.Extended.UI
{
    using System.Runtime.CompilerServices;
    using Module.Core.AddressableKeys;
    using UnityEngine;

    public static class AddressableKeyScreenMessageExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ShowScreenMessage ToShowScreenMessage(
              this AddressableKey<GameObject> key
            , bool playAnimation = false
            , bool loadAsync = true
            , params object[] args
        )
        {
            return new(key, playAnimation, loadAsync, args);
        }
    }
}

#endif
#endif