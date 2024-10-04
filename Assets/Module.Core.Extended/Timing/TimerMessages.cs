#if UNITASK || UNITY_6000_0_OR_NEWER

using System.Runtime.CompilerServices;
using Module.Core.PubSub;

namespace Module.Core.Extended.Timing
{
    public readonly struct RegisterToTimerMessage : IMessage
    {
        public readonly ComplexId Id;
        public readonly ITimer Timer;

        public RegisterToTimerMessage(ComplexId id, ITimer timer)
        {
            Id = id;
            Timer = timer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegisterToTimerMessage From<T>(T timer) where T : ITimer
            => new(TypeId.Get<T>().ToComplexId(), timer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegisterToTimerMessage From<T>(Id id, T timer) where T : ITimer
            => new(TypeId.Get<T>().ToComplexId(id), timer);
    }

    public readonly struct UnregisterToTimerMessage : IMessage
    {
        public readonly ComplexId Id;

        public UnregisterToTimerMessage(ComplexId id)
        {
            Id = id;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnregisterToTimerMessage From<T>() where T : ITimer
            => new(TypeId.Get<T>().ToComplexId());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnregisterToTimerMessage From<T>(Id id) where T : ITimer
            => new(TypeId.Get<T>().ToComplexId(id));
    }

    public readonly record struct SetCurrentTimeMessage(long ServerTimeDeltaInSeconds) : IMessage;

    public readonly record struct StopUpdateTimeMessage(bool IsStop) : IMessage;
}

#endif
