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

                var numberOfEnemies = userInput.Enemies.Count();
                var multiplier = multiplierData.Where(d =>
                                d.NumberOfMonsters.Floor <= numberOfEnemies &&
                                d.NumberOfMonsters.Ceiling >= numberOfEnemies)
                                .Select(d => d.Muliplier).FirstOrDefault();

                var enemyCRSum = userInput.Enemies.Sum(e => e.ChallengeRating);
                var encounterCR = Math.Round(enemyCRSum * multiplier);

                var rnd = new Random();
                int roll100 = rnd.Next(1, 100);

                var dto = data.Where(d =>
                            d.CalculationType == userInput.CalculationType &&
                            d.ChallengeRating.Floor <= encounterCR &&
                            d.ChallengeRating.Ceiling >= encounterCR &&
                            d.D100.Floor <= roll100 &&
                            d.D100.Ceiling >= roll100).FirstOrDefault();

                userInput.Treasure.Coins?.Clear();
                userInput.Treasure.Ornaments?.Clear();

                if (dto == null || dto.Pieces == null)
                {
                    userInput.Message = "No treasure found.";
                    return userInput;
                }
              

                foreach (var piece in dto.Pieces)
                {
                    int totalRoll = 0;
                    for (var i = 0; i < piece.Roll?.NumberOfDice; i++)
                    {
                        var roll = rnd.Next(1, piece.Roll.DiceType);
                        totalRoll += roll;
                    }
                    if (piece.Roll.Multiplier > 0)
                    {
                        totalRoll *= piece.Roll.Multiplier;
                    }
                   
                    userInput.Treasure.Coins.Add(new Coin { Metal = piece.Metal, Total = totalRoll });
                }
            }
            catch (Exception ex)
            {
                userInput.Message = "Error while calculating treasure: " + ex.Message.ToString() + ". Stack Trace:  " + ex.StackTrace.ToString();
                return userInput;
            }

            return treasure;
        }
    }
}
