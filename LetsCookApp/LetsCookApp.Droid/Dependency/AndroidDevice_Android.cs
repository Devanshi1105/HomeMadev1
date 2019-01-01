using System;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Java.IO;
using Java.Util;
using Java.Util.Concurrent;
using Xamarin.Forms;
using LetsCookApp.Services;
using LetsCookApp.Droid.Dependency;

[assembly: Dependency(typeof(AndroidDevice_Android))]
namespace LetsCookApp.Droid.Dependency
{
    public class AndroidDevice_Android : IDevice
    {
        private static readonly long DeviceTotalMemory = GetTotalMemory();
        private static IDevice currentDevice;

        private INetwork network;

        /// <summary>
        /// Prevents a default instance of the <see cref="AndroidDevice"/> class from being created. 
        /// </summary>
        public AndroidDevice_Android()
        {
            var manager = PhoneService_Android.Manager;

            //TODO:: PhoneService Is Null And We get Exception on 

            //if (manager != null && manager.PhoneType != PhoneType.None)
            //{
            //    this.PhoneService = new PhoneService_Android();
            //}
            if (manager != null)
            {
                this.PhoneService = new PhoneService_Android();
            }

            this.Display = new Display_Android();

            this.Manufacturer = Build.Manufacturer;
            this.Name = Build.Model;
            this.HardwareVersion = Build.Hardware;
            this.FirmwareVersion = Build.VERSION.Release;
        }

        /// <summary>
        /// Gets the current device.
        /// </summary>
        /// <value>
        /// The current device.
        /// </value>
        public IDevice CurrentDevice { get { return currentDevice ?? (currentDevice = new AndroidDevice_Android()); } }

        /// <summary>
        /// Gets Unique Id for the device.
        /// </summary>
        /// <value>
        /// The id for the device.
        /// </value>
        public string Id
        {
            // TODO: Verify what is the best combination of Unique Id for Android
            get
            {
                return Build.Serial;
            }
        }

        /// <summary>
        /// Gets the phone service for this device.
        /// </summary>
        /// <value>Phone service instance if available, otherwise null.</value>
        public IPhoneService PhoneService { get; private set; }

        /// <summary>
        /// Gets the display information for the device.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        public IDisplay Display { get; private set; }

        /// <summary>
        /// Gets the network service.
        /// </summary>
        /// <value>The network service.</value>
        public INetwork Network { get { return this.network ?? (this.network = new Network()); } }

        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        /// <value>
        /// The name of the device.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the firmware version.
        /// </summary>
        /// <value>
        /// The firmware version.
        /// </value>
        public string FirmwareVersion { get; private set; }

        /// <summary>
        /// Gets the hardware version.
        /// </summary>
        /// <value>
        /// The hardware version.
        /// </value>
        public string HardwareVersion { get; private set; }

        /// <summary>
        /// Gets the manufacturer.
        /// </summary>
        /// <value>
        /// The manufacturer.
        /// </value>
        public string Manufacturer { get; private set; }

        public string LanguageCode
        {
            get { return Locale.Default.Language; }
        }

        public double TimeZoneOffset
        {
            get
            {
                var calendar = new Java.Util.GregorianCalendar();
                var timezone = calendar.TimeZone;
                return TimeUnit.Hours.Convert(timezone.RawOffset, TimeUnit.Milliseconds) / 3600;
            }
        }

        public string TimeZone
        {
            get { return Java.Util.TimeZone.Default.ID; }
        }

        public Orientation Orientation
        {
            get
            {
                var wm = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

                switch (wm.DefaultDisplay.Rotation)
                {
                    case SurfaceOrientation.Rotation0:
                        return Orientation.Portrait & Orientation.PortraitUp;
                    case SurfaceOrientation.Rotation90:
                        return Orientation.Landscape & Orientation.LandscapeLeft;
                    case SurfaceOrientation.Rotation180:
                        return Orientation.Portrait & Orientation.PortraitDown;
                    case SurfaceOrientation.Rotation270:
                        return Orientation.Landscape & Orientation.LandscapeRight;
                    default:
                        return Orientation.None;
                }
            }
        }

        /// <summary>
        /// Gets the total memory in bytes.
        /// </summary>
        /// <value>The total memory in bytes.</value>
        public long TotalMemory
        {
            get
            {
                return DeviceTotalMemory;
            }
        }

        private static long GetTotalMemory()
        {
            using (var reader = new RandomAccessFile("/proc/meminfo", "r"))
            {
                var line = reader.ReadLine(); // first line --> MemTotal: xxxxxx kB
                var split = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                return Convert.ToInt64(split[1]) * 1024;
            }
        }

        public Task<bool> LaunchUriAsync(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}