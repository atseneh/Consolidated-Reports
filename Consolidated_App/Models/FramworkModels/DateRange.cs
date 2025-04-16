using System.ComponentModel.DataAnnotations;

namespace Consolidated_App.Models.FramworkModels
{
    public class DateRange
    {
        [DataType(DataType.Date)]
        public DateTime? startDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? endDate { get; set; }

        public string startDateString { get; set; }
        public string endDateString { get; set; }

    }
}
