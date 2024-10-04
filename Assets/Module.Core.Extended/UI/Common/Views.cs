#if (UNITASK || UNITY_6000_0_OR_NEWER) && ZBASE_UNITY_SCREEN_NAVIGATOR

namespace Module.Core.Extended.UI
{
    public abstract class NavActivity : ZBase.UnityScreenNavigator.Core.Activities.Activity { }

    public abstract class NavControl : ZBase.UnityScreenNavigator.Core.Controls.Control { }

    public abstract class NavModal : ZBase.UnityScreenNavigator.Core.Modals.Modal { }

    public abstract class NavScreen : ZBase.UnityScreenNavigator.Core.Screens.Screen { }

    public abstract class NavSheet : ZBase.UnityScreenNavigator.Core.Sheets.Sheet { }
}

#endif
