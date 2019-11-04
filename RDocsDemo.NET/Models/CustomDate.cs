using System;
using System.Globalization;

namespace RDocsDemo.NET.Models
{
    public class CustomDate
    {
        private int year = 0, month = 0, day = 0, score = 0;
        private string origin = "";

        public CustomDate(int year, int month, int day, string origin)
        {
            this.year = year;
            this.month = month;
            this.day = day;
            this.origin = origin;
            DateTime parsedDate;
            if (!DateTime.TryParse(year + "-" + month + "-" + day, CultureInfo.CreateSpecificCulture("es-CL"), DateTimeStyles.None, out parsedDate))
                decreaseScore();
            else if (parsedDate > DateTime.Today.AddDays(1))
                decreaseScore();
        }

        public void decreaseScore()
        {
            --score;
        }
        public void increaseScore()
        {
            ++score;
        }
        public int Year { get { return year; } }
        public int Month { get { return month; } }
        public int Day { get { return day; } }
        public int Score { get { return score; } }
        public string Origin { get { return origin; } }

        public override string ToString()
        {
            DateTime parsedDate;
            if (DateTime.TryParse(year + "-" + month + "-" + day, CultureInfo.CreateSpecificCulture("es-CL"), DateTimeStyles.None, out parsedDate))
                return parsedDate.ToString("dd-MMMM-yyyy", CultureInfo.CreateSpecificCulture("es-CL"));
            else
                return day + "-" + month + "-" + year;
        }
    }
}
