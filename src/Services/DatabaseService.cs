using dwCheckApi.DatabaseContexts;
using dwCheckApi.DatabaseTools;
using System.Linq;
namespace dwCheckApi.Services
{
    public class DatabaseService : IDatabaseService
    {
        private DwContext _context;
        public DatabaseService(DwContext context)
        {
            _context = context;
        }
        public bool ClearDatabase()
        {
            var cleared = _context.Database.EnsureDeleted();
            var created = _context.Database.EnsureCreated();
            var entitiesadded = _context.SaveChanges();

            return (cleared && created && entitiesadded == 0);
        }

        public int SeedDatabase()
        {
            var entitiesAdded = default(int);
            var seeder = new DatabaseSeeder(_context);
            if(!_context.Books.Any())
            {
                entitiesAdded += seeder.SeedBookEntitiesFromJson();
            }

            if (!_context.Characters.Any())
            {
                entitiesAdded += seeder.SeedCharacterEntitiesFromJson();
            }

            if(!_context.BookCharacters.Any())
            {
                entitiesAdded += seeder.SeedBookCharacterEntriesFromJson();
            }

            return entitiesAdded;
        }
    }
}