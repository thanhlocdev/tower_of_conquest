#if (UNITASK || UNITY_6000_0_OR_NEWER) && ZBASE_UNITY_SCREEN_NAVIGATOR

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Module.Core.PubSub;

namespace Module.Core.Extended.UI
{
    public readonly struct UIScope : IEquatable<UIScope>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(UIScope other)
            => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is UIScope;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => base.GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => nameof(UIScope);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(UIScope _, UIScope __)
            => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(UIScope _, UIScope __)
            => false;
    }

    public static class MessengerUIScopeExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MessageSubscriber.Subscriber<UIScope> UIScope([NotNull] this MessageSubscriber self)
            => self.Scope<UIScope>(default);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MessagePublisher.Publisher<UIScope> UIScope([NotNull] this MessagePublisher self)
            => self.Scope<UIScope>(default);
    }
}

#endif
