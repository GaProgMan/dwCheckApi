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
        public void ClearDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _context.SaveChanges();
        }

        public void SeedDatabase()
        {
            var seeder = new DatabaseSeeder(_context);
            if(!_context.Books.Any())
            {
                seeder.SeedBookEntitiesFromJson();
            }

            if (!_context.Characters.Any())
            {
                seeder.SeedCharacterEntitiesFromJson();
            }

            if(!_context.BookCharacters.Any())
            {
                seeder.SeedBookCharacterEntriesFromJson();
            }
        }
    }
}