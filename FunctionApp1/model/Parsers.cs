using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FunctionApp1.model
{
    internal class Parsers
    {
        public static Address ParseAddress(string input)
        {
            var address = new Address();

            input = Regex.Replace(input, @"^\s+", "");
            // Wzorzec dla danych
            string pattern = @"(?:(?<Street>ul\. [^,]+), )?(?:(?<Estate>(Osiedle )?[^,]+), )?(?<City>[^,]+), (?<Province>śląskie)";
            var match = Regex.Match(input, pattern);

             


            if (match.Success)
            {
                address.Street = match.Groups["Street"].Value.Trim();
                address.Estate = match.Groups["Estate"].Value.Trim();
                address.City = match.Groups["City"].Value.Trim();
                address.Province = match.Groups["Province"].Value.Trim();
            }

            return address;
        }

        public static FlatDetails ParsePriceAndCurrency(string input, FlatDetails tempFlat)
        {
            string pattern = @"(?<Amount>[\d\s]+)\s*(?<Currency>\S+)";
            var match = Regex.Match(input, pattern);
            if (match.Success)
            {
                string amount = match.Groups["Amount"].Value.Replace(" ", ""); // Usunięcie spacji z liczby
                tempFlat.Currency = match.Groups["Currency"].Value;
                try
                {
                    tempFlat.Price = float.Parse(amount);
                }
                catch
                {
                    tempFlat.Price = null;
                }
                
            }
            return tempFlat;
        }
        }
}
