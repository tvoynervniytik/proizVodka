using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mop.DB
{
    partial class Address
    {
        public string StreetName
        {
            get
            {
                string result = "";
                result = Street.Trim();
                return result;

            }
            set { }
        }
        public string HouseCalc(string name, string number) 
        {
            string result = "";
            if (number != null) 
                result = name.Trim() + number.Trim();
            else result = name;
            return result;
        }
        public string House
        {
            get
            {
                string result = "";
                result = HouseNumber.Trim();
                return result;
            }
            set { }
        }
        public string HouseDigit
        {
            get
            {
                if (string.IsNullOrEmpty(HouseNumber))
                    return string.Empty;

                string digits = new string(HouseNumber.Where(char.IsDigit).ToArray());
                return digits;
            }
            set { }
        }
        public string HouseLetter
        {
            get
            {
                if (string.IsNullOrEmpty(HouseNumber))
                    return string.Empty;

                string letters = new string(HouseNumber.Where(c => char.IsLetter(c)).ToArray());
                return letters;
            }
            set { }
        }
        public string Room
        {
            get
            {
                string result = "";
                result = RoomNumber.Trim();
                return result;

            }
            set { }
        }

    }
}
