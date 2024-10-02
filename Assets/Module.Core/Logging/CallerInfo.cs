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

using System.Runtime.CompilerServices;
using Unity.Collections;

namespace Module.Core.Logging
{
    public readonly struct CallerInfo
    {
        public readonly string MemberName;
        public readonly string FilePath;
        public readonly int LineNumber;
        public readonly bool IsValid;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CallerInfo(
              int lineNumber
            , string memberName
            , string filePath
        )
        {
            MemberName = memberName;
            FilePath = filePath;
            LineNumber = lineNumber;
            IsValid = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => IsValid ? $"{LineNumber} :: {MemberName} :: {FilePath}" : string.Empty;

#if UNITY_COLLECTIONS
        public FixedString4096Bytes ToFixedString()
        {
            var fs = new FixedString4096Bytes();

            if (IsValid)
            {
                fs.Append(LineNumber);
                AppendSeparator(ref fs);
                fs.Append(MemberName);
                AppendSeparator(ref fs);
                fs.Append(FilePath);
            }

            return fs;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static void AppendSeparator(ref FixedString4096Bytes fs)
            {
                fs.Append(' ');
                fs.Append(':');
                fs.Append(':');
                fs.Append(' ');
            }
        }
#endif
    }

    public static class CallerInfoExtensions
    {
        public static CallerInfo GetCallerInfo(
              this object _
            , [CallerLineNumber] int lineNumber = 0
            , [CallerMemberName] string memberName = ""
            , [CallerFilePath] string filePath = ""
        )
        {
            return new CallerInfo(lineNumber, memberName, filePath);
        }
    }
}
