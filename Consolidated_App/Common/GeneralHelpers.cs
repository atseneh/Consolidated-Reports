using System.ComponentModel.DataAnnotations;

namespace Consolidated_App
{
    public static class GeneralHelpers
    {
        public static string? formatMultiSelect(List<string> values, Boolean coated = true)
        {

            string? selected = null;
            if (values != null && values.Count > 0)
            {
                if (values?.Count == 1 && values?.FirstOrDefault() == null)
                {
                    return selected;
                }
                if (coated)
                {
                    values.ForEach(v => selected = string.IsNullOrWhiteSpace(selected) ? string.Format("'{0}'", v.ToString()) : string.Format("{0}, '{1}'", selected, v.ToString()));
                }
                else
                {
                    values.ForEach(v => selected = string.IsNullOrWhiteSpace(selected) ? string.Format("{0}", v.ToString()) : string.Format("{0}, {1}", selected, v.ToString()));
                }
            }
            return selected;
        }
        public static string formatMultiSelect(List<string>? values, int position)
        {
            string selected = "";
            if (values != null && values.Count > 0)
            {
                for (var x = 0; x < values.Count; x++)
                {
                    var TempValue = values[x].Split("#");
                    if (string.IsNullOrWhiteSpace(selected))
                    {
                        selected = string.Format("'{0}'", TempValue[position].ToString());
                    }
                    else
                    {
                        string.Format("{0}, '{1}'", selected, TempValue[position].ToString());
                    }
                }
            }
            return selected;
        }
        public static string FirstCharacterToLower(this string str)
        {
            if (String.IsNullOrEmpty(str) || Char.IsLower(str, 0))
            {
                return str;
            }

            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
        public static DateRange parseDateRange(string dateRange)
        {
            DateRange result = null;
            if (!string.IsNullOrWhiteSpace(dateRange))
            {
                var splited = dateRange?.Split("-");
                if (splited != null && splited.Count() > 1)
                {
                    result = new DateRange();

                    DateTime startDate, endDate;
                    result.startDate = DateTime.TryParse(splited[0], out startDate) ? (DateTime?)startDate : null;
                    result.endDate = DateTime.TryParse(splited[1], out endDate) ? (DateTime?)endDate : null;
                    result.startDateString = splited[0];
                    result.endDateString = splited[1];
                }
            }
            return result;
        }
        public static string ConvertToDefaultDate(string? defaultDocumentBrowser)
        {
            DateTime currentDate = DateTime.Now;
            string? startDefaultDate = null, endDefaultDate, resultDate;

            if (defaultDocumentBrowser != null)
            {
                string lowerBrowser = defaultDocumentBrowser.ToLower();

                if (lowerBrowser == "daily" || lowerBrowser == "weekly" || lowerBrowser == "monthly" || lowerBrowser == "annually")
                {
                    if (lowerBrowser == "daily")
                    {
                        startDefaultDate = currentDate.ToString("MM/dd/yyyy");
                    }
                    else if (lowerBrowser == "weekly")
                    {
                        startDefaultDate = currentDate.AddDays(-7).ToString("MM/dd/yyyy");
                    }
                    else if (lowerBrowser == "monthly")
                    {
                        startDefaultDate = currentDate.AddMonths(-1).ToString("MM/dd/yyyy");
                    }
                    else if (lowerBrowser == "annually")
                    {
                        startDefaultDate = currentDate.AddYears(-1).ToString("MM/dd/yyyy");
                    }

                    endDefaultDate = currentDate.ToString("MM/dd/yyyy");
                    resultDate = startDefaultDate + "-" + endDefaultDate;
                }
                else if (lowerBrowser == "showall")
                {
                    resultDate = "showall";
                }
                else if (lowerBrowser == "none")
                {
                    resultDate = "none";
                }
                else
                {
                    resultDate = currentDate.ToString() + "-" + currentDate.ToString();
                }
            }
            else
            {
                resultDate = null;
            }

            return resultDate;
        }
    }
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
