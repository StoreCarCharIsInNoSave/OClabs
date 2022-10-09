using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toTrenary
{
    public class Con
    {
        public static string toTrenary(int n)
        {
            string s = "";
            while (n > 0)
            {
                int t = n % 3;
                n /= 3;
                s += t.ToString();
            }
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new String(arr);
        }
    }
}
