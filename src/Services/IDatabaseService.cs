namespace dwCheckApi.Services 
{
    public interface IDatabaseService
    {
        void ClearDatabase();

        void SeedDatabase();
    }
}