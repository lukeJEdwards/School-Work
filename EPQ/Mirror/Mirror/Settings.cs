using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Mirror
{
    class Settings
    {

        private readonly string SettingFile = "Settings.json";
        private string SettingsString = " ";

        public static string Country { set; get; }
        public static string Town { set; get; }

        public Settings()
        {

        }

        private void ReadFile()
        {
            try
            {
                using (StreamReader file = new StreamReader(SettingFile))
                {
                    SettingsString = file.ReadToEnd();
                }
            }catch(Exception e)
            {
                new ErrorFrom(e.Message);
            }
        }
    }
}
