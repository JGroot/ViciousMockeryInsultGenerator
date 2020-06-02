using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace ViciousMockeryGenerator.Data
{
    public class CriticalFailService
    {
        public Task<CriticalFailure> GetMelee()
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + @"\Data\Files\MeleeDamage.json";
                var json = JsonConvert.DeserializeObject<List<CriticalFailure>>(File.ReadAllText(path));

                var rnd = new Random();
                int roll = rnd.Next(1, json.Count);

                var result = json.Where(d => d.Roll == roll).FirstOrDefault();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                return Task.FromResult(
                    new CriticalFailure() { Description = "Error: " + ex.ToString() } );
            }
        }


        public Task<CriticalFailure> GetRanged()
        {
            try
            {
                var rnd = new Random();
                int roll = rnd.Next(1, 5);

                var path = Directory.GetCurrentDirectory() + @"\Data\Files\RangedDamage.json";
                var json = JsonConvert.DeserializeObject<List<CriticalFailure>>(File.ReadAllText(path));

                var result = json.Where(d => d.Roll == roll).FirstOrDefault();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                return Task.FromResult(
                  new CriticalFailure() { Description = "Error: " + ex.ToString() });
            }
        }

        public Task<CriticalFailure> GetSpell()
        {
            try
            {
                var rnd = new Random();
                int roll = rnd.Next(1, 5);

                var path = Directory.GetCurrentDirectory() + @"\Data\Files\SpellDamage.json";
                var json = JsonConvert.DeserializeObject<List<CriticalFailure>>(File.ReadAllText(path));

                var result = json.Where(d => d.Roll == roll).FirstOrDefault();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                return Task.FromResult(
                  new CriticalFailure() { Description = "Error: " + ex.ToString() });
            }
        }
    }
}

