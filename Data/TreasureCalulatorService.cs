using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ViciousMockeryGenerator.Data.Models;

namespace ViciousMockeryGenerator.Data
{
    public class TreasureCalulatorService
    {
        public async Task<TreasureViewModel> CalculateTreasure(TreasureViewModel userInput)
        {
            var treasure = new TreasureViewModel();

            string treasurePath = string.Empty;
            try
            {
                treasurePath = AppContext.BaseDirectory + @"/Data/Files/CoinTreasureTable.json";
                var data = JsonConvert.DeserializeObject<List<TreasureModel>>(File.ReadAllText(treasurePath));
                var multiplierPath = AppContext.BaseDirectory + @"/Data/Files/EncounterMultiplierTable.json";
                var multiplierData = JsonConvert.DeserializeObject<List<EncounterMultiplier>>(File.ReadAllText(multiplierPath));

                if (userInput.CalculationType == CalculationType.Hoard)
                {

                    //int ceiling = (int)Math.Ceiling(enemy.ChallengeRating);
                    //var hoard = data.Where(x => x.CalculationType == CalculationType.Hoard).ToList();

                }
                else if (userInput.CalculationType == CalculationType.Individual)
                {
                    var numberOfEnemies = userInput.Enemies.Count();
                    var multiplier = multiplierData.Where(d =>
                                    d.NumberOfMonsters.Floor <= numberOfEnemies &&
                                    d.NumberOfMonsters.Ceiling >= numberOfEnemies)
                                    .Select(d => d.Muliplier).FirstOrDefault();
                    var enemyCRSum = userInput.Enemies.Sum(e => e.ChallengeRating);
                    var encounterCR = enemyCRSum * multiplier;
                    var encounterCRRounded = Math.Round(encounterCR);
                    var rnd = new Random();
                    int roll100 = rnd.Next(1, 100);

                    var dto = data.Where(d =>
                                d.CalculationType == userInput.CalculationType &&
                                d.ChallengeRating.Floor <= encounterCRRounded &&
                                d.ChallengeRating.Ceiling >= encounterCRRounded &&
                                d.D100.Floor <= roll100 &&
                                d.D100.Ceiling >= roll100).FirstOrDefault();

                    foreach (var piece in dto.Pieces)
                    {
                        int totalRoll = 0;
                        for (var i = 0; i < piece.Roll.NumberOfDice; i++)
                        {
                            var roll = rnd.Next(1, piece.Roll.DiceType);
                            totalRoll += roll;
                        }

                        userInput.Treasure.Add(new Treasure() { Metal = piece.Metal, Total = totalRoll });
                    }
                }
            }
            catch (Exception ex)
            {
                return userInput;
            }

            return treasure;
        }
    }
}
