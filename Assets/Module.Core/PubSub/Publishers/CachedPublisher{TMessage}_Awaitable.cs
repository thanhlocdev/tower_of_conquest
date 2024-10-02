#if !UNITASK && UNITY_6000_0_OR_NEWER

// MIT License
// 
// Copyright (c) 2024 Laicasaane
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#if !(UNITY_EDITOR || DEBUG) || DISABLE_DEBUG
#define __MODULE_CORE_PUBSUB_NO_VALIDATION__
#else
#define __MODULE_CORE_PUBSUB_VALIDATION__
#endif

using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace Module.Core.PubSub
{
    using CallerInfo = Module.Core.Logging.CallerInfo;
    using DevLogger = Module.Core.Logging.DevLogger;

    partial struct CachedPublisher<TMessage>
    {
#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public readonly void Publish(
              TMessage message
            , CancellationToken token = default
            , Module.Core.Logging.ILogger logger = null
            , CallerInfo callerInfo = default
        )
        {
#if __MODULE_CORE_PUBSUB_VALIDATION__
            if (Validate(message, logger) == false)
            {
                return;
            }
#endif

            _broker.PublishAsync(message, new(callerInfo), token, logger ?? DevLogger.Default).Forget();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Awaitable PublishAsync(
              CancellationToken token = default
            , Module.Core.Logging.ILogger logger = null
            , CallerInfo callerInfo = default
        )
        {
            return PublishAsync(new TMessage(), token, logger, callerInfo);
        }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public readonly Awaitable PublishAsync(
              TMessage message
            , CancellationToken token = default
            , Module.Core.Logging.ILogger logger = null
            , CallerInfo callerInfo = default
        )
        {
#if __MODULE_CORE_PUBSUB_VALIDATION__
            if (Validate(message, logger) == false)
            {
                return Awaitables.GetCompleted();
            }
#endif

            return _broker.PublishAsync(message, new(callerInfo), token, logger ?? DevLogger.Default);
        }
    }
}

#endif
