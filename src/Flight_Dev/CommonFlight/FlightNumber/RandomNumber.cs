using System;
using System.Text;

namespace CommonFlight.FlightNumber
{
    public class RandomNumber
    {
        private static readonly Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden
        public string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
