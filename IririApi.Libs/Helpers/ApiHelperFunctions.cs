using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Helpers
{
    public class ApiHelperFunctions
    {
        public static string DeductPaystackCharges(decimal amount)
        {
            if (amount <= 0)
            {
                return "0";
            }

            decimal charges;

            if (amount < 2500)
            {
                charges = (decimal)0.015 * amount;
            }

            else
            {
                charges = ((decimal)0.015 * amount) + 100;
            }

            charges = charges > 2000 ? 2000 : charges;

            return (amount - charges).ToString();
        }
    }
}
