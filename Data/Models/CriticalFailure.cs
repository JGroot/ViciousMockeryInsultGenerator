using System.ComponentModel;

namespace ViciousMockeryGenerator.Data.Models
{
    public class CriticalFailure
    {
        public int Roll { get; set; }
        public DamageCategory DamageCategory { get; set; }
        public FailureCategory FailureCategory { get; set; }      
        public string Description { get; set; }
        public string Comment { get; set; }
    }

    public enum DamageCategory
    {
        None,
        Melee,
        Ranged,
        Spell
    }

    public enum FailureCategory
    {
        [Description(" ")]
        None,
        [Description("Miss")]
        BasicMiss,
        [Description("Opponent Action")]
        Opponent,
        [Description("Environment Blooper")]
        Surroundings,
        [Description("Contested")]
        Contested,
        [Description("Target Gains Effect")]
        TargetGainsEffects,
        [Description("Wrong Target")]
        WrongTarget,
        [Description("Equipment Damaged")]
        EquipmentDamaged,
        [Description("Counter Movement")]
        CounterMovement,
        [Description("Counter Action")]
        CounterActions,
        [Description("Counter Attack")]
        CounterAttacks,
        [Description("Oh No")]
        OhNo
    }
       
}
