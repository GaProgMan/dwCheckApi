using dwCheckApi.DatabaseContexts;
using dwCheckApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace dwCheckApi.DatabaseTools
{
    public class DatabaseSeeder
    {
        private DwContext _context;

        public DatabaseSeeder(DwContext context)
        {
            _context = context;
        }

        public void SeedBookEntitiesFromJson()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "BookSeedData.json");
            if (File.Exists(filePath))
            {
                var dataSet = File.ReadAllText(filePath);
                var seedData = JsonConvert.DeserializeObject<List<Book>>(dataSet);
                _context.Books.AddRange(seedData);
                _context.SaveChanges();
            }
        }

        public void SeedCharacterEntitiesFromJson()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "CharacterSeedData.json");
            if (File.Exists(filePath))
            {
                var dataSet = File.ReadAllText(filePath);
                var seedData = JsonConvert.DeserializeObject<List<Character>>(dataSet);
                _context.Characters.AddRange(seedData);
                _context.SaveChanges();
            }
        }

        public void SeedBookCharacterEntriesFromJson()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "BookCharacterSeedData.json");
            if (File.Exists(filePath))
            {
                var dataSet = File.ReadAllText(filePath);
                var seedData = JsonConvert.DeserializeObject<List<BookCharacterSeedData>>(dataSet);

                foreach(var bc in seedData)
                {
                    var book = _context.Books.Single(b => b.BookName == bc.BookName);
                    var characters = _context.Characters.Where(c => bc.CharacterNames.Contains(c.CharacterName));

                    foreach (var character in characters)
                    {
                        _context.BookCharacters.Add(new BookCharacter
                        {
                            Book = book,
                            Character = character
                        });
                    }
                }
                _context.SaveChanges();
                }
        }
    }
}