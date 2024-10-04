#if (UNITASK || UNITY_6000_0_OR_NEWER) && ZBASE_UNITY_SCREEN_NAVIGATOR

using Module.Core.PubSub;

namespace Module.Core.Extended.UI
{
    public readonly record struct BeginLoadingActivityMessage : IMessage;

    public readonly record struct CompleteLoadingActivityMessage : IMessage;
}

#endif
