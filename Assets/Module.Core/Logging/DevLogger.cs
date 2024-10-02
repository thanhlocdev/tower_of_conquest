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

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Module.Core.Logging
{
    public class DevLogger : ILogger
    {
        public static readonly DevLogger Default = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogException(Exception value)
        {
            DevLoggerAPI.LogException(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogInfo(object message)
        {
            DevLoggerAPI.LogInfo(message);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogInfoFormat(string format, params object[] args)
        {
            DevLoggerAPI.LogInfoFormat(format, args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogWarning(object message)
        {
            DevLoggerAPI.LogWarning(message);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogWarningFormat(string format, params object[] args)
        {
            DevLoggerAPI.LogWarningFormat(format, args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogError(object message)
        {
            DevLoggerAPI.LogError(message);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogErrorFormat(string format, params object[] args)
        {
            DevLoggerAPI.LogErrorFormat(format, args);
        }

        /// <see cref="LogOption.NoStacktrace"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogInfoSlim(object message)
        {
            DevLoggerAPI.LogInfoSlim(message);
        }

        /// <see cref="LogOption.NoStacktrace"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogInfoFormatSlim(string format, params object[] args)
        {
            DevLoggerAPI.LogInfoFormatSlim(format, args);
        }

        /// <see cref="LogOption.NoStacktrace"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogWarningSlim(object message)
        {
            DevLoggerAPI.LogWarningSlim(message);
        }

        /// <see cref="LogOption.NoStacktrace"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogWarningFormatSlim(string format, params object[] args)
        {
            DevLoggerAPI.LogWarningFormatSlim(format, args);
        }

        /// <see cref="LogOption.NoStacktrace"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogErrorSlim(object message)
        {
            DevLoggerAPI.LogErrorSlim(message);
        }

        /// <see cref="LogOption.NoStacktrace"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogErrorFormatSlim(string format, params object[] args)
        {
            DevLoggerAPI.LogErrorFormatSlim(format, args);
        }
    }
}
