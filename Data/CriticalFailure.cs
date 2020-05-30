using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

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
        BasicMiss,
        Opponent,
        Surroundings,
        Contested,
        TargetGainsEffects,
        WrongTarget,
        EquipmentDamaged,
        CounterMovement,
        CounterActions,
        CounterAttacks,
        OhNo
    }
}
