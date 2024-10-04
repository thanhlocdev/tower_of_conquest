#if (UNITASK || UNITY_6000_0_OR_NEWER) && ZBASE_UNITY_SCREEN_NAVIGATOR

using System;
using System.Diagnostics.CodeAnalysis;
using Module.Core.Vaults;
using ZBase.UnityScreenNavigator.Core.Windows;

namespace Module.Core.Extended.UI
{
    public static class WindowContainerExtensions
    {
        public static bool TryAddToGlobalObjectVaultAs<T>(
              [NotNull] this WindowContainerBase container
            , WindowContainerId id
            , ref T result
        )
            where T : WindowContainerBase
        {
            if (string.Equals(container.name, id.name, StringComparison.Ordinal) == false
                || container is not T containerT
            )
            {
                return false;
            }

            GlobalObjectVault.TryAdd(new PresetId(id.id), containerT);
            result = containerT;
            return true;
        }
    }
}

#endif
