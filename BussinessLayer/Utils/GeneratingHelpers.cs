using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Utils
{
    public class GeneratingHelpers
    {
        private static readonly int length = 12;
        public static string generateID()
        {
            StringBuilder builder = new StringBuilder(0, length);
            Random rd = new Random();
            while (builder.Length < 12)
            {
                Char c = (char)rd.Next('0', 'Z');
                if (char.IsDigit(c) || char.IsLetter(c))
                {
                    builder.Append(c);
                }
            }
            return builder.ToString().ToUpper();
        }   
    }
}
