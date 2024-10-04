#if UNITASK || UNITY_6000_0_OR_NEWER

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Module.Core.PubSub;

namespace Module.Core.Extended.Timing
{
    public readonly struct TimerScope : IEquatable<TimerScope>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(TimerScope other)
            => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is TimerScope;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => base.GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => nameof(TimerScope);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(TimerScope _, TimerScope __)
            => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(TimerScope _, TimerScope __)
            => false;
    }

    public static class MessengerTimerScopeExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MessageSubscriber.Subscriber<TimerScope> TimerScope([NotNull] this MessageSubscriber self)
            => self.Scope<TimerScope>(default);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MessagePublisher.Publisher<TimerScope> TimerScope([NotNull] this MessagePublisher self)
            => self.Scope<TimerScope>(default);
    }
}

#endif
