using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Helper
{
    public class EncodeDecode
    {
        public string EnCode(string valueEnCode)
        {
            byte[] valueArray = Encoding.ASCII.GetBytes(valueEnCode);
            string enCodeValue = Convert.ToBase64String(valueArray);
            return enCodeValue;
        }

        public string DeCode(string valueDeCode)
        {
            byte[] valueArray = Convert.FromBase64String(valueDeCode);
            string enCodeValue = Encoding.ASCII.GetString(valueArray);
            return enCodeValue;
        }

    }
}
