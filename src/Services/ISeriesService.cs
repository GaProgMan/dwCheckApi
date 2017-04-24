using dwCheckApi.Models;
using System.Collections.Generic;

namespace dwCheckApi.Services
{
    public interface ISeriesService
    {
        Series GetById (int id);
        Series GetByName (string seriesName);
        IEnumerable<Series> Search(string searchKey);
    }
}