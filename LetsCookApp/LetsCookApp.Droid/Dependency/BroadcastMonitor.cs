using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LetsCookApp.Droid.Dependency
{
    public abstract class BroadcastMonitor : BroadcastReceiver
    {
        /// <summary>
        ///  Start monitoring. 
        /// </summary>
        public virtual bool Start()
        {
            var intent = Android.App.Application.Context.RegisterReceiver(this, this.Filter);
            if (intent == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///  Stop monitoring. 
        /// </summary>
        public virtual void Stop()
        {
            Android.App.Application.Context.UnregisterReceiver(this);
        }

        /// <summary>
        /// Gets the intent filter to use for monitoring.
        /// </summary>
        protected abstract IntentFilter Filter { get; }
    }
}