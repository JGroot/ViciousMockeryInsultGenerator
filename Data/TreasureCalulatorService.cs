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
                //TODO: Cache files
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

                var dtos = data.Where(d =>
                            d.CalculationType == userInput.CalculationType &&
                            d.ChallengeRating.Floor <= encounterCR &&
                            d.ChallengeRating.Ceiling >= encounterCR &&
                            d.D100.Floor <= roll100 &&
                            d.D100.Ceiling >= roll100);

                userInput.Treasure.Coins?.Clear();
                userInput.Treasure.ArtGems?.Clear();

                foreach (var dto in dtos)
                {
                    if (dto == null || (dto.Pieces == null && dto.Ornaments == null))
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


                    foreach (var ornament in dto.Ornaments)
                    {
                        int totalRoll = 0;
                        for (var i = 0; i < ornament.Roll?.NumberOfDice; i++)
                        {
                            var roll = rnd.Next(1, ornament.Roll.DiceType);
                            totalRoll += roll;
                        }

                        var art = new ArtGem()
                        {
                            Count = totalRoll,
                            OrnamentType = ornament.OrnamentType,
                            TotalWorth = new Coin() { Metal = Metal.Gold, Total = ornament.SingleValue * totalRoll }
                        };

                        userInput.Treasure.ArtGems.Add(art);
                    }

                    if (dto.MagicItems.Any() && dto.MagicItems.Count > 0)
                    {
                        var magicItemPath = AppContext.BaseDirectory + @"/Data/Files/MagicItemTable.json";
                        var magicItemData = JsonConvert.DeserializeObject<List<MagicItemModel>>(File.ReadAllText(magicItemPath));
                        foreach (var magicItem in dto.MagicItems)
                        {
                            var magicItemRollCount = RollDice(magicItem.Roll);
                            for (var i = 0; i < magicItemRollCount; i++)
                            {

                            }
                        }
                    }
                   
                }
                return userInput;
            }
            catch (Exception ex)
            {
                userInput.Message = "Error while calculating treasure: " + ex.Message.ToString() + ". Stack Trace:  " + ex.StackTrace.ToString();
                return userInput;
            }
        }

        private int RollDice(Roll roll)
        {
            var rnd = new Random();
            int totalRoll = 0;
            for (var i = 0; i < roll.NumberOfDice; i++)
            {
                var result = rnd.Next(1, roll.DiceType);
                totalRoll += result;
            }
            return totalRoll;
        }
    }
}
