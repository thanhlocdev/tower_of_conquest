#if (UNITASK || UNITY_6000_0_OR_NEWER) && ZBASE_UNITY_SCREEN_NAVIGATOR

using System;

namespace Module.Core.Extended.UI
{
    [Serializable]
    public struct WindowContainerId
    {
        public string name;
        public int id;

        public WindowContainerId(string name, int id)
        {
            this.name = name;
            this.id = id;
        }
    }
}

#endif
