using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using dwCheckApi.Entities;

namespace dwCheckApi.Persistence.Helpers
{
    public class DatabaseSeeder
    {
        private readonly IDwContext _context;

        public DatabaseSeeder(IDwContext context)
        {
            _context = context;
        }

        public async Task<int> SeedBookEntitiesFromJson(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"Value of {filePath} must be supplied to {nameof(SeedBookEntitiesFromJson)}");
            }
            if (!File.Exists(filePath))
            {
                throw new ArgumentException($"The file { filePath} does not exist");
            }
            var dataSet = await File.ReadAllTextAsync(filePath);
            var seedData = JsonConvert.DeserializeObject<List<Book>>(dataSet);

            // ensure that we only get the distinct books (based on their name)
            var distinctSeedData = seedData.GroupBy(b => b.BookName).Select(b => b.First());

            _context.Books.AddRange(distinctSeedData);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> SeedBookCharacterEntriesFromJson()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "BookCharacterSeedData.json");
            if (File.Exists(filePath))
            {
                var dataSet = await File.ReadAllTextAsync(filePath);
                var seedData = JsonConvert.DeserializeObject<List<BookCharacterSeedData>>(dataSet);

                foreach(var seedBook in seedData)
                {
                    var dbBook = _context.Books.Single(b => b.BookName == seedBook.BookName);

                    foreach (var seedChar in seedBook.CharacterNames)
                    {
                        var dbChar = _context.Characters.FirstOrDefault(c => c.CharacterName == seedChar);
                        if (dbChar == null)
                        {
                            dbChar = new Character{
                                CharacterName = seedChar
                            };
                        }
                        _context.BookCharacters.Add(new BookCharacter
                        {
                            Book = dbBook,
                            Character = dbChar
                        });
                    }
                }
                return await _context.SaveChangesAsync();
            }

            return default(int);
        }

        public async Task<int> SeedBookSeriesEntriesFromJson()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "SeriesBookSeedData.json");
            if (File.Exists(filePath))
            {
                var dataSet = await File.ReadAllTextAsync(filePath);
                var seedData = JsonConvert.DeserializeObject<List<SeriesBookSeedData>>(dataSet);

                var entitiesToAdd = new List<BookSeries>();
                foreach (var seedSeries in seedData)
                {
                    var dbSeries = _context.Series.FirstOrDefault(s => s.SeriesName == seedSeries.SeriesName);
                    if (dbSeries == null)
                    {
                        dbSeries = new Series
                        {
                            SeriesName = seedSeries.SeriesName
                        };
                    }

                    for(var ordinal = 0; ordinal < seedSeries.BookNames.Count; ordinal++)
                    {
                        var dbBook = _context.Books.Single(b => b.BookName == seedSeries.BookNames[ordinal]);
                        entitiesToAdd.Add(new BookSeries
                        {
                            Series = dbSeries,
                            Book = dbBook,
                            Ordinal = ordinal
                        });
                    }
                }

                _context.BookSeries.AddRange(entitiesToAdd);
                return await _context.SaveChangesAsync();
            }
            return default(int);
        }
    }
}