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
        }

        [Required]
        [Description("Number of Players")]
        [Range(1, 1000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int NumberOfPlayers { get; set; }

        [Required]
        [Description("Average Level of Players")]
        [Range(1, 1000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int LevelOfCharacters { get; set; }

        [ValidateComplexType]
        public List<Enemy> Enemies { get; set; }

        public List<Piece> Pieces { get; set; }
        public EnemyType EnemyType { get; set; }
    }

    public class Enemy
    {
        private string _strId = "zero";
        private int _id;

        public int Id { get => _id; set { _strId = value.ToString(); _id = value; } }

        [Required]
        [Description("Challenge Rating")]
        [Range(1, 1000, ErrorMessage = "Value for Enemy {0} must be between {1} and {2}.")]
        public int ChallengeRating { get; set; }
    }
}
