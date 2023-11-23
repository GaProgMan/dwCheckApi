using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace dwCheckApi.Entities
{
    public class Series : BaseAuditClass
    {
        public int SeriesId { get; set; }
        public string SeriesName { get; set; }
        public virtual ICollection<BookSeries> BookSeries { get; set; } = new Collection<BookSeries>();
    }
}