using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using LetsCookApp.Managers.SettingsManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
       protected readonly IManager userManager;
       protected readonly ISettingsManager settingsManager;
       
        private readonly Dictionary<string, object> PropertyValues = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public BaseViewModel()
        {
            userManager = SimpleIoc.Default.GetInstance<IManager>();
           settingsManager = SimpleIoc.Default.GetInstance<ISettingsManager>();
        }

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            return GetValue(propertyName, default(T));
        }

        protected void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) return;

            var shouldNotify = !PropertyValues.ContainsKey(propertyName) || !object.Equals(value, PropertyValues[propertyName]);

            PropertyValues[propertyName] = value;

            if (shouldNotify)
                RaisePropertyChanged(propertyName);
        }

        /// <summary>
        /// Gets the value of the property specified by propertyName. If no
        /// value is present, defaultValue is returned.
        /// </summary>
        /// <param name="propertyName">The name of the property for which you're
        /// trying to get the value of.</param>
        /// <param name="propertyName">The name of the property (note this is case sensitive)
        /// for which you're trying to get the value of</param>
        private T GetValue<T>(string propertyName, T defaultValue)
        {
            if (PropertyValues.ContainsKey(propertyName))
                return (T)PropertyValues[propertyName];

            return defaultValue;
        }

        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }



    }
}
