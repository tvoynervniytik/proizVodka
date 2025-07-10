using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mop.DB
{
    partial class Orders
    {
        public decimal Price
        {
            get
            {
                decimal price = 0;

                if (ServiceID == null || CountPeople == null)
                    return price;

                decimal servPrice = this.Services?.Price ?? 0;
                
                price = servPrice*(decimal)CountPeople;

                return price;
            }
            set
            {

            }
        }
        public string DateCorrect
        {
            get
            {
                string day = Date.Value.Day.ToString();
                string month = Date.Value.Month.ToString();
                string year = Date.Value.Year.ToString();
                if (day.Length<2)
                    day = "0" + day;
                if (month.Length<2)
                    month = "0" + month;
                string dateCorrect = day + "." + month + "." + year;
                return dateCorrect;
            }
            set { }
        }
    }
}
