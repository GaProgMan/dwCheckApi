using System.Collections.Generic;

namespace dwCheckApi.DTO.ViewModels
{
    public class SeriesViewModel : BaseViewModel
    {
        public SeriesViewModel()
        {
            BookNames = new List<string>();
        }

        public int SeriesId { get; set; }
        public string SeriesName { get; set; }
        public List<string> BookNames { get; set; }
    }
}