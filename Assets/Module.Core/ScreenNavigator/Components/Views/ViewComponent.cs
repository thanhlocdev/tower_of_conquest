#if UNITASK

// Copyright 2021 Haruki Yano
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

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Module.Core.ScreenNavigator.Views
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class ViewComponent : UIBehaviour
    {
        [FormerlySerializedAs("_dontUseCanvasGroup")]
        [SerializeField] private bool _dontAddCanvasGroupAutomatically = false;
        [SerializeField] private bool _usePrefabNameAsIdentifier = true;

        [SerializeField]
        [EnableIf(nameof(_usePrefabNameAsIdentifier), false)]
        private string _identifier;

        public string Identifier
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _identifier;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _identifier = value;
        }

        public virtual string Name
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return !IsDestroyed() && gameObject == true ? gameObject.name : string.Empty;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (IsDestroyed() || gameObject == false)
                {
                    return;
                }

                gameObject.name = value;
            }
        }

        private RectTransform _rectTransform;

        public virtual RectTransform RectTransform
        {
            get
            {
                if (IsDestroyed())
                {
                    return null;
                }

                if (_rectTransform == false)
                {
                    _rectTransform = gameObject.GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        private RectTransform _parent;

        public virtual RectTransform Parent
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (IsDestroyed())
                {
                    return null;
                }

                return _parent;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal set => _parent = value;
        }

        public virtual GameObject Owner
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => IsDestroyed() ? gameObject : null;
        }

        public virtual bool ActiveSelf
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                GameObject go;
                return IsDestroyed() == false
                    && (go = gameObject) == true
                    && go.activeSelf == true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (IsDestroyed()
                    || gameObject == false
                    || gameObject.activeSelf == value)
                {
                    return;
                }

                gameObject.SetActive(value);
            }
        }

        public virtual float Alpha
        {
            get
            {
                if (IsDestroyed() || gameObject == false)
                {
                    return 0;
                }

                if (CanvasGroup)
                {
                    return CanvasGroup.alpha;
                }

                return 1F;
            }

            set
            {
                if (IsDestroyed() || gameObject == false)
                {
                    return;
                }

                if (CanvasGroup)
                {
                    CanvasGroup.alpha = value;
                }

            }
        }

        public virtual bool Interactable
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (IsDestroyed() || gameObject == false)
                {
                    return false;
                }

                if (CanvasGroup)
                {
                    return CanvasGroup.interactable;
                }

                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (IsDestroyed() || gameObject == false)
                {
                    return;
                }

                if (CanvasGroup)
                {
                    CanvasGroup.interactable = value;
                }
            }
        }

        private CanvasGroup _canvasGroup;

        public virtual CanvasGroup CanvasGroup
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (IsDestroyed())
                {
                    return null;
                }

                if (_canvasGroup == false)
                {
                    _canvasGroup = gameObject.GetComponent<CanvasGroup>();
                }

                if (_canvasGroup == false && _dontAddCanvasGroupAutomatically == false)
                {
                    _canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }

                return _canvasGroup;
            }
        }

        public bool DontAddCanvasGroupAutomatically
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _dontAddCanvasGroupAutomatically;
        }

        protected void SetIdentifer()
        {
            _identifier = _usePrefabNameAsIdentifier
                ? gameObject.name.Replace("(Clone)", string.Empty)
                : _identifier;
        }

        protected static async UniTask WaitFotAsync(IEnumerable<UniTask> tasks)
        {
            try
            {
                foreach (var task in tasks)
                {
                    try
                    {
                        await task;
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
            }
        }

    }
}

#endif
