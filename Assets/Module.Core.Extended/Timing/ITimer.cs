#if UNITASK || UNITY_6000_0_OR_NEWER

using System;

namespace Module.Core.Extended.Timing
{
    public interface ITimer
    {
        TimeSpan RemainTime { get; set; }

        TimeSpan Interval { get; }

        bool Enabled { get; }

        void OnTimeZero();
    }
}

#endif
