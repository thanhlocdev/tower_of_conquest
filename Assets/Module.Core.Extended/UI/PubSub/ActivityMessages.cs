#if (UNITASK || UNITY_6000_0_OR_NEWER) && ZBASE_UNITY_SCREEN_NAVIGATOR

namespace Module.Core.Extended.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using Module.Core.PubSub;
    using UnityEngine;

    public readonly record struct ShowActivityMessage(
          string ResourcePath
        , bool PlayAnimation
        , Memory<object> Args
    ) : IMessage;

    public readonly record struct HideActivityMessage(string ResourcePath, bool PlayAnimation) : IMessage;

    public static class ResourceKeyActivityMessageExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ShowActivityMessage ToShowActivityMessage(
              this ResourceKey<GameObject> key
            , bool playAnimation = false
            , params object[] args
        )
        {
            return new(key, playAnimation, args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HideActivityMessage ToHideActivityMessage(
              this ResourceKey<GameObject> key
            , bool playAnimation = false
        )
        {
            return new(key, playAnimation);
        }
    }
}

#if UNITY_ADDRESSABLES

namespace Module.Core.Extended.UI
{
    using System.Runtime.CompilerServices;
    using Module.Core.AddressableKeys;
    using UnityEngine;

    public static class AddressableKeyActivityMessageExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ShowActivityMessage ToShowActivityMessage(
              this AddressableKey<GameObject> key
            , bool playAnimation = false
            , params object[] args
        )
        {
            return new(key, playAnimation, args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HideActivityMessage ToHideActivityMessage(
              this AddressableKey<GameObject> key
            , bool playAnimation = false
        )
        {
            return new(key, playAnimation);
        }
    }
}

#endif
#endif
