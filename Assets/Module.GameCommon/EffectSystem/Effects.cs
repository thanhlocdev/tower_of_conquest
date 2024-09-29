namespace Module.GameCommon.EffectSystem
{
    public partial struct EffectBurn : IEffectModifier
    {
        public bool CanExcute => true;

        public void Execute()
        {
            
        }
    }

    public partial struct EffectStun : IEffectModifier
    {
        public bool CanExcute => true;
        public void Execute()
        { }
    }

    public partial struct EffectFreeze : IEffectModifier
    {
        public bool CanExcute => true;
        public void Execute() 
        { }
    }

    public partial struct EffectSlow : IEffectModifier
    {
        public bool CanExcute => true;
        public void Execute()
        { }
    }
}
