using System.Collections.Generic;

namespace dwCheckApi.DatabaseTools
{
    public class BookSeriesSeedData
    {
        public string BookName { get; set; }
        public List<SeriesAndOrdinal> SeriesEntry { get; set; }
    }

    public class SeriesAndOrdinal
    {
        public string SeriesName { get; set; }
        public int OrdinalWithinSeries { get; set; }
    }
}