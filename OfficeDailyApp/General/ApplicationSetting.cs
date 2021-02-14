using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace OfficeDailyApp.General
{
    public class ApplicationSetting
    {
        public static string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["rdx"].ConnectionString;
        } 
    }
}
