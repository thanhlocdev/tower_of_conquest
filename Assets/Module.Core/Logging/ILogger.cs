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

namespace Module.Core.Logging
{
    public interface ILogger
    {
        void LogException(Exception value);

        void LogInfo(object message);

        void LogInfoFormat(string format, params object[] args);

        void LogWarning(object message);

        void LogWarningFormat(string format, params object[] args);

        void LogError(object message);

        void LogErrorFormat(string format, params object[] args);

        void LogInfoSlim(object message);

        void LogInfoFormatSlim(string format, params object[] args);

        void LogWarningSlim(object message);

        void LogWarningFormatSlim(string format, params object[] args);

        void LogErrorSlim(object message);

        void LogErrorFormatSlim(string format, params object[] args);
    }
}
