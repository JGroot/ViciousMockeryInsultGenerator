using System.ComponentModel;

namespace ViciousMockeryGenerator.Data
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
        Melee,
        Ranged,
        Spell
    }

    public enum FailureCategory
    {
        [Description(" ")]
        None,
        [Description("Basic Miss")]
        BasicMiss,
        [Description("Opponent")]
        Opponent,
        [Description("Surroundings")]
        Surroundings,
        [Description("Contested")]
        Contested,
        [Description("Target Gains Effects")]
        TargetGainsEffects,
        [Description("Wrong Target")]
        WrongTarget,
        [Description("Equipment Damaged")]
        EquipmentDamaged,
        [Description("Counter Movement")]
        CounterMovement,
        [Description("Counter Actions")]
        CounterActions,
        [Description("Counter Attacks")]
        CounterAttacks,
        [Description("Oh No!")]
        OhNo
    }


    //public class FailureCategory
    //{
    //    private FailureCategory(string value) { Value = value; }

    //    public string Value { get; set; }

    //    public static FailureCategory None { get { return new FailureCategory(string.Empty);  } }
    //    public static FailureCategory BasicMiss { get { return new FailureCategory("Basic Miss"); } }
    //    public static FailureCategory Opponent { get { return new FailureCategory("Opponent"); } }
    //    public static FailureCategory Surroundings { get { return new FailureCategory("Surroundings"); } }
    //    public static FailureCategory Contested { get { return new FailureCategory("Contested"); } }
    //    public static FailureCategory TargetGainsEffects { get { return new FailureCategory("Target Gains Effects"); } }
    //    public static FailureCategory WrongTarget { get { return new FailureCategory("Wrong Target"); } }
    //    public static FailureCategory EquipmentDamaged { get { return new FailureCategory("Equipment Damaged"); } }
    //    public static FailureCategory CounterMovement { get { return new FailureCategory("Counter Movement"); } }
    //    public static FailureCategory CounterActions { get { return new FailureCategory("Counter Actions"); } }
    //    public static FailureCategory CounterAttacks { get { return new FailureCategory("Counter Attacks"); } }
    //    public static FailureCategory OhNo { get { return new FailureCategory("Oh No"); } }
    //}
       
}
