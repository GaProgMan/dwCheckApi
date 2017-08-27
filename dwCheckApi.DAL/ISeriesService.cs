using System.Collections.Generic;
using dwCheckApi.Entities;

namespace dwCheckApi.DAL
{
    public interface ISeriesService
    {
        Series GetById (int id);
        Series GetByName (string seriesName);
        IEnumerable<Series> Search(string searchKey);
    }
}