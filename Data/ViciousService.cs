using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

using ViciousMockeryGenerator.Data.Models;

namespace ViciousMockeryGenerator.Data
{
    public class ViciousService
    {

        public Task<ViciousMockery> GetVicious()
        {
            var path = AppContext.BaseDirectory + @"/Data/Files/ViciousMockeryData.json";
            var dto = JsonConvert.DeserializeObject<ViciousMockeryDTO>(File.ReadAllText(path));

            var Nouns = dto.Nouns;
            var Adjectives = dto.Adjectives;
            var Clauses = dto.Clauses;

            var rnd = new Random();
            int n1Index = rnd.Next(Nouns.Length);
            int n2Index = rnd.Next(Nouns.Length);
            int a1Index = rnd.Next(Adjectives.Length);
            int a2Index = rnd.Next(Adjectives.Length);
            int cIndex = rnd.Next(Clauses.Length);

            string article1;
            string article2;

            var adj1 = Adjectives[a1Index];
            var noun2 = Nouns[n2Index];

            if (StartsWithVowel(adj1))
            {
                article1 = "an";
            }
            else
            {
                article1 = "a";
            }

            if (StartsWithVowel(noun2))
            {
                article2 = "an";
            }
            else
            {
                article2 = "a";
            }

            var viciousmock = new ViciousMockery() { Insult = $"You're {article1} {adj1} {Adjectives[a2Index]} {Nouns[n1Index]} {Clauses[cIndex]} you are {article2} {Nouns[n2Index]}!!!" };
            return Task.FromResult(viciousmock);
        }

        private bool StartsWithVowel(string str)
        {
            if (str.StartsWith("a") || str.StartsWith("i") || str.StartsWith("o") || str.StartsWith("e") || str.StartsWith("y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
