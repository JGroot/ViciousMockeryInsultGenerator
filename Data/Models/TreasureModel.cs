using System.Collections.Generic;

namespace ViciousMockeryGenerator.Data.Models
{
    public class TreasureModel
    {
        public int Id { get; set; }
        public CalculationType CalculationType { get; set; }
        public Range ChallengeRating { get; set; }
        public Range D100 { get; set; }
        public List<Piece> Pieces { get; set; }
            
    }

    public class Range
    {
        public int Floor { get; set; }
        public int Ceiling { get; set; }
    }
    public class Piece
    {
        public Metal Metal { get; set; }
        public Roll Roll { get; set;}       
    }

    public class Roll
    {
        public int NumberOfDice { get; set; }
        public int DiceType { get; set; }
        public int Multiplier { get; set; }
    }

    public class EncounterMultiplier
    {
        public Range NumberOfMonsters { get; set; }
        public double Muliplier { get; set; }
    }

    public enum Metal
    {
        None,
        Platnium,
        Gold,
        Silver,
        Electrum,
        Copper
    }

    public enum CalculationType
    {
        Individual,
        Hoard
    }
}
