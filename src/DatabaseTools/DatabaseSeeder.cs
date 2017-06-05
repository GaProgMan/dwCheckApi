using dwCheckApi.DatabaseContexts;
using dwCheckApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace dwCheckApi.DatabaseTools
{
    public class DatabaseSeeder
    {
        private IDwContext _context;

        public DatabaseSeeder(IDwContext context)
        {
            _context = context;
        }

        public int SeedBookEntitiesFromJson(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"Value of {nameof(filePath)} must be supplied to {nameof(SeedBookEntitiesFromJson)}");
            }
            if (!File.Exists(filePath))
            {
                throw new ArgumentException($"The file {nameof(filePath)} does not exist");
            }
            var dataSet = File.ReadAllText(filePath);
            var seedData = JsonConvert.DeserializeObject<List<Book>>(dataSet);

            // ensure that we only get the distinct books (based on their name)
            var distinctSeedData = seedData.GroupBy(b => b.BookName).Select(b => b.First());

            _context.Books.AddRange(seedData);
            return _context.SaveChanges();
        }

        public int SeedBookCharacterEntriesFromJson()
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
                return _context.SaveChanges();
            }

            return default(int);
        }

        public int SeedBookSeriesEntriesFromJson()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "SeriesBookSeedData.json");
            if (File.Exists(filePath))
            {
                var dataSet = File.ReadAllText(filePath);
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

                    for(int ordinal = 0; ordinal < seedSeries.BookNames.Count; ordinal++)
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
                return _context.SaveChanges();
            }
            return default(int);
        }
    }
}