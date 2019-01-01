using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Managers.SettingsManager
{
    public class SettingsManager : ISettingsManager
    {
        public string ApiHost
        {
            get
            {
                return "https://indianrasoicom.000webhostapp.com/webapis/";
            }
        }
    }
}
