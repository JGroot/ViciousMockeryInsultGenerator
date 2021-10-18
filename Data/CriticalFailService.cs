using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using DeezNDeezTools.Data.Models;

namespace DeezNDeezTools.Data
{
    public class CriticalFailService
    {
        public async Task<CriticalFailure> GetMelee()
        {
            string path = string.Empty;
            try
            {
                path = AppContext.BaseDirectory + @"Data\Files\MeleeDamage.json";
                return await GetFailure(path);
            }
            catch (Exception ex)
            {
                return new CriticalFailure() { Description = $"Error: {ex.Message}, Path = {path}" };
            }
        }


        public async Task<CriticalFailure> GetRanged()
        {
            try
            {
                var path = AppContext.BaseDirectory + @"Data\Files\RangedDamage.json";
                return await GetFailure(path);
            }
            catch (Exception ex)
            {
                return new CriticalFailure() { Description = "Error: " + ex.ToString() };
            }
        }

        public async Task<CriticalFailure> GetSpell()
        {
            try
            {
                var path = AppContext.BaseDirectory + @"Data\Files\SpellDamage.json";
                return await GetFailure(path);
            }
            catch (Exception ex)
            {
                return new CriticalFailure() { Description = "Error: " + ex.ToString() };
            }
        }

        private async Task<CriticalFailure> GetFailure(string path)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };
            var json = JsonSerializer.Deserialize<List<CriticalFailure>>(File.ReadAllText(path), options);
            var count = json.Count;
            var rnd = new Random();
            int roll = rnd.Next(1, json.Count);

            var result = await Task.Run(() => json.Where(d => d.Roll == roll).FirstOrDefault());
            if (result == null)
            {
                result = SetEmptyResult(roll);
            }
            return result;
        }

        private CriticalFailure SetEmptyResult(int roll)
            => new CriticalFailure()
            {
                Roll = roll,
                Comment = "No result for roll found.",
                Description = "Error.",
                FailureCategory = FailureCategory.None,
                DamageCategory = DamageCategory.None
            };

    }
}

