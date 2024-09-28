namespace Module.GameCommon.StatSystem
{
    public interface IStat
    {
        float Value { get; }
        float AlterableBaseValue { get; set; }
        void AddModifier(StatModifier modifier);
        void RemoveModifier(StatModifier modifier);
    }
}
