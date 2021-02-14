using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDailyApp.General
{
    public class DateCreate
    {
        public static string[] Begin()
        {
            string[] mydate = new string[365];
            int counter = 0;
            for (int j = 1; j < 13; j++)
            {
                for (int i = 1; i < 31; i++)
                {
                    mydate[counter] = j.ToString("D2") + "/" + i.ToString("D2");
                    counter += 1;
                }
            }
            return mydate;
        }
     

    }
}
