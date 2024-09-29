namespace Module.GameCommon.EffectSystem
{
    public interface IEffectModifier
    {
        bool CanExcute { get; }
        void Execute();
    }
}
