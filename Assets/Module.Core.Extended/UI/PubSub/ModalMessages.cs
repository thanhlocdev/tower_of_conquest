#if (UNITASK || UNITY_6000_0_OR_NEWER) && ZBASE_UNITY_SCREEN_NAVIGATOR

namespace Module.Core.Extended.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using Module.Core.PubSub;
    using UnityEngine;

    public readonly record struct ShowModalMessage(
          string ResourcePath
        , bool PlayAnimation
        , bool LoadAsync
        , float? BackdropAlpha
        , Memory<object> Args
    ) : IMessage
    {
        public ShowModalMessage(
              string resourcePath
            , bool playAnimation
            , bool loadAsync
            , Memory<object> args
        )
            : this(resourcePath, playAnimation, loadAsync, default, args)
        { }
    }

    public readonly record struct HideModalMessage(bool PlayAnimation) : IMessage;

    public readonly record struct HideAllModalMessage(bool PlayAnimation = false, int Count = 0) : IMessage;

    public static class ResourceKeyModalMessageExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ShowModalMessage ToShowModalMessage(
              this ResourceKey<GameObject> key
            , bool playAnimation = false
            , bool loadAsync = true
            , float? backdropAlpha = default
            , params object[] args
        )
        {
            return new(key, playAnimation, loadAsync, backdropAlpha, args);
        }
    }
}

#if UNITY_ADDRESSABLES

namespace Module.Core.Extended.UI
{
    using System.Runtime.CompilerServices;
    using Module.Core.AddressableKeys;
    using UnityEngine;

    public static class AddressableKeyModalMessageExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ShowModalMessage ToShowModalMessage(
              this AddressableKey<GameObject> key
            , bool playAnimation = false
            , bool loadAsync = true
            , float? backdropAlpha = default
            , params object[] args
        )
        {
            return new(key, playAnimation, loadAsync, backdropAlpha, args);
        }
    }
}

#endif
#endif
