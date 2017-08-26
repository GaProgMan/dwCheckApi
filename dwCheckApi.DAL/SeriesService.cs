using System.Collections.Generic;
using System.Linq;
using dwCheckApi.Entities;
using dwCheckApi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace dwCheckApi.DAL
{
    public class SeriesService : ISeriesService
    {
        private DwContext _dwContext;

        public SeriesService (DwContext dwContext)
        {
            _dwContext = dwContext;
        }

        Series ISeriesService.GetById(int id)
        {
            return BaseQuery().FirstOrDefault(s => s.SeriesId == id);
        }

        Series ISeriesService.GetByName(string seriesName)
        {
            if(string.IsNullOrWhiteSpace(seriesName))
            {
                // TODO : what here?
                return null;
            }

            seriesName = seriesName.ToLower();

            return BaseQuery().FirstOrDefault(ch => ch.SeriesName.ToLower() == seriesName);
        }

        IEnumerable<Series> ISeriesService.Search(string searchKey)
        {
            var blankSearchString = string.IsNullOrEmpty(searchKey);

            var results = BaseQuery();

            if (!blankSearchString)
            {
                searchKey = searchKey.ToLower();
                results = BaseQuery()
                    .Where(ch => ch.SeriesName.ToLower().Contains(searchKey));
            }

            return results.OrderBy(ch => ch.SeriesName);
        }

        private IEnumerable<Series> BaseQuery()
        {
            // Explicit joins of entities is taken from here:
            // https://weblogs.asp.net/jeff/ef7-rc-navigation-properties-and-lazy-loading
            // At the time of committing 5da65e093a64d7165178ef47d5c21e8eeb9ae1fc, Entity
            // Framework Core had no built in support for Lazy Loading, so the above was
            // used on all DbSet queries.
            return _dwContext.Series
                .AsNoTracking()
                .Include(bookSeries => bookSeries.BookSeries)
                .ThenInclude(book => book.Book);
        }
    }
}