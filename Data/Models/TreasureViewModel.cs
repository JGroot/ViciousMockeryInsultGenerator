using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViciousMockeryGenerator.Data.Models
{
    public class TreasureViewModel
    {
        public TreasureViewModel()
        {
            Enemies = new List<Enemy>() { new Enemy() { Id = 1 } };
            Treasure = new List<Treasure>();
        }


        public CalculationType CalculationType { get; set; }

        [ValidateComplexType]
        public List<Enemy> Enemies { get; set; }

        public List<Treasure> Treasure { get; set; }
        public string Message { get; set; }
        
    }

    public class Enemy
    {
        public int Id { get; set; }

        [Required]
        [Description("Challenge Rating")]
        [Range(0.25, 40, ErrorMessage = "Value for Enemy {0} must be between {1} and {2}.")]
        public double ChallengeRating { get; set; }
    }

    public class Treasure
    {
        public Metal Metal { get; set; }
        public int Total { get; set; }
    }
}
