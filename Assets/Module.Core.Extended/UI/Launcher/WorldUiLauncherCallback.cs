#if (UNITASK || UNITY_6000_0_OR_NEWER) && ZBASE_UNITY_SCREEN_NAVIGATOR

using System.Diagnostics.CodeAnalysis;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core;
using ZBase.UnityScreenNavigator.Core.Windows;

namespace Module.Core.Extended.UI
{
    public abstract class WorldUiLauncherCallback : MonoBehaviour
    {
        public virtual void OnAwake([NotNull] WorldUiLauncher launcher) { }

        public virtual void OnPreCreateContainers([NotNull] WorldUiLauncher launcher) { }

        public virtual bool OnCreateContainer(
              [NotNull] WorldUiLauncher launcher
            , [NotNull] WindowContainerConfig config
            , [NotNull] WindowContainerBase container
        )
        {
            return false;
        }

        public virtual UniTask OnInitializeAsync([NotNull] WorldUiLauncher launcher) => UniTask.CompletedTask;
    }
}

#endif
