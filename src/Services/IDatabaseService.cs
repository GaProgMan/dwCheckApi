namespace dwCheckApi.Services 
{
    public interface IDatabaseService
    {
        bool ClearDatabase();

        int SeedDatabase();
    }
}