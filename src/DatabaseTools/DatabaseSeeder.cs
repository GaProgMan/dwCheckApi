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

                // ensure that we only get the distinct books (based on their name)
                var distinctSeedData = seedData.GroupBy(b => b.BookName).Select(b => b.First());

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
                var seedData = JsonConvert.DeserializeObject<IEnumerable<Character>>(dataSet);

                // ensure that we only get the distinct characters (based on their name)
                var distinctSeedData = seedData.GroupBy(c => c.CharacterName).Select(c => c.First());

                _context.Characters.AddRange(distinctSeedData);
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

                foreach(var seedBook in seedData)
                {
                    var dbBook = _context.Books.Single(b => b.BookName == seedBook.BookName);

                    foreach (var seedChar in seedBook.CharacterNames)
                    {
                        var dbChar = _context.Characters.FirstOrDefault(c => c.CharacterName == seedChar);
                        if (dbChar != null)
                        {
                            _context.BookCharacters.Add(new BookCharacter
                            {
                                Book = dbBook,
                                Character = dbChar
                            }); 
                        }
                    }
                }
                _context.SaveChanges();
            }
        }
    }
}