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
            Treasure = new Treasure();
            MagicItems = new List<MagicItemModel>();
        }


        public CalculationType CalculationType { get; set; }

        [ValidateComplexType]
        public List<Enemy> Enemies { get; set; }
        public Treasure Treasure { get; set; }
        public List<MagicItemModel> MagicItems { get; set; }
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
        public Treasure()
        {
            Coins = new List<Coin>();
            ArtGems = new List<ArtGem>();
        }

        public List<Coin> Coins {get; set;}
        public List<ArtGem> ArtGems { get; set; }
    }

    public class Coin
    {
        public Metal Metal { get; set; }
        public int Total { get; set; }
    }

    public class ArtGem
    {
        public OrnamentType OrnamentType { get; set; }
        public int Count { get; set; }
        public Coin TotalWorth { get; set; }
    }

}
