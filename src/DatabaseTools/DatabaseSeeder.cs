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

        public int SeedBookEntitiesFromJson()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "BookSeedData.json");
            if (File.Exists(filePath))
            {
                var dataSet = File.ReadAllText(filePath);
                var seedData = JsonConvert.DeserializeObject<List<Book>>(dataSet);

                // ensure that we only get the distinct books (based on their name)
                var distinctSeedData = seedData.GroupBy(b => b.BookName).Select(b => b.First());

                _context.Books.AddRange(seedData);
                return _context.SaveChanges();
            }

            return default(int);
        }

        public int SeedCharacterEntitiesFromJson()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "CharacterSeedData.json");
            if (File.Exists(filePath))
            {
                var dataSet = File.ReadAllText(filePath);
                var seedData = JsonConvert.DeserializeObject<IEnumerable<Character>>(dataSet);

                // ensure that we only get the distinct characters (based on their name)
                var distinctSeedData = seedData.GroupBy(c => c.CharacterName).Select(c => c.First());

                _context.Characters.AddRange(distinctSeedData);
                return _context.SaveChanges();
            }

            return default(int);
        }

        public int SeedSeriesEntitiesFromJson()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "SeriesSeedData.json");
            if (File.Exists(filePath))
            {
                var dataSet = File.ReadAllText(filePath);
                var seedData = JsonConvert.DeserializeObject<List<Series>>(dataSet);

                // ensure that we only get the distinct books (based on their name)
                var distinctSeedData = seedData.GroupBy(b => b.SeriesName).Select(b => b.First());

                _context.Series.AddRange(seedData);
                return _context.SaveChanges();
            }

            return default(int);
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
                return _context.SaveChanges();
            }

            return default(int);
        }

        public int SeedBookSeriesEntriesFromJson()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "BookSeriesSeedData.json");
            if (File.Exists(filePath))
            {
                var dataSet = File.ReadAllText(filePath);
                var seedData = JsonConvert.DeserializeObject<List<BookSeriesSeedData>>(dataSet);

                foreach(var seedBook in seedData)
                {
                    var dbBook = _context.Books.Single(b => b.BookName == seedBook.BookName);

                    foreach (var seriesData in seedBook.SeriesEntry)
                    {
                        var dbSeries = _context.Series.FirstOrDefault(s => s.SeriesName == seriesData.SeriesName);
                        if (dbSeries != null)
                        {
                            _context.BookSeries.Add(new BookSeries
                            {
                                Book = dbBook,
                                Ordinal = seriesData.OrdinalWithinSeries,
                                Series = dbSeries
                            }); 
                        }
                    }
                }
                return _context.SaveChanges();
            }

            return default(int);
        }
    }
}