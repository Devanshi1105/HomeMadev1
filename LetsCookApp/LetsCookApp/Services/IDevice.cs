using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Services
{
    public interface IDevice
    {
        /// <summary>
        /// Gets Unique Id for the device.
        /// </summary>
        string Id { get; }

        IDevice CurrentDevice { get; }

        /// <summary>
        /// Gets the display information for the device.
        /// </summary>
        IDisplay Display { get; }

        /// <summary>
        /// Gets the phone service for this device.
        /// </summary>
        /// <value>Phone service instance if available, otherwise null.</value>
        IPhoneService PhoneService { get; }

        /// <summary>
        /// Gets the network service.
        /// </summary>
        /// <value>The network service.</value>
        INetwork Network { get; }

        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        /// <value>The name of the device.</value>
        string Name { get; }

        /// <summary>
        /// Gets the firmware version.
        /// </summary>
        string FirmwareVersion { get; }

        /// <summary>
        /// Gets the hardware version.
        /// </summary>
        string HardwareVersion { get; }

        /// <summary>
        /// Gets the manufacturer.
        /// </summary>
        string Manufacturer { get; }

        /// <summary>
        /// Gets the total memory in bytes.
        /// </summary>
        long TotalMemory { get; }

        /// <summary>
        /// Gets the ISO Language Code
        /// </summary>
        string LanguageCode { get; }

        /// <summary>
        /// Gets the UTC offset
        /// </summary>
        double TimeZoneOffset { get; }

        /// <summary>
        /// Gets the timezone name
        /// </summary>
        string TimeZone { get; }

        /// <summary>
        /// Gets the <see cref="Orientation"/> of the device.
        /// </summary>
        Orientation Orientation { get; }

        /// <summary>
        /// Starts the default app associated with the URI for the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>The launch operation.</returns>
        Task<bool> LaunchUriAsync(Uri uri);
    }

    public interface INetwork
    {
        /// <summary>
        /// Determines whether the specified host is reachable.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="timeout">The timeout.</param>
        Task<bool> IsReachable(string host, TimeSpan timeout);
        /// <summary>
        /// Determines whether [is reachable by wifi] [the specified host].
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="timeout">The timeout.</param>
        Task<bool> IsReachableByWifi(string host, TimeSpan timeout);
        /// <summary>
        /// Internets the connection status.
        /// </summary>
        /// <returns>NetworkStatus.</returns>
        NetworkStatus InternetConnectionStatus();
    }
    public interface IDisplay
    {
        /// <summary>
        /// Gets the screen height in pixels
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the screen width in pixels
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the screens X pixel density per inch
        /// </summary>
        double Xdpi { get; }

        /// <summary>
        /// Gets the screens Y pixel density per inch
        /// </summary>
        double Ydpi { get; }

        /// <summary>
        /// Gets the font manager
        /// </summary>
        //IFontManager FontManager { get; }

        /// <summary>
        /// Convert width in inches to runtime pixels
        /// </summary>
        double WidthRequestInInches(double inches);

        /// <summary>
        /// Convert height in inches to runtime pixels
        /// </summary>
        double HeightRequestInInches(double inches);
    }

    public interface IPhoneService
    {
        /// <summary>
        /// Gets the cellular provider.
        /// </summary>
        string CellularProvider { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has cellular data enabled.
        /// </summary>
        bool? IsCellularDataEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has cellular data roaming enabled.
        /// </summary>
        bool? IsCellularDataRoamingEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has network available.
        /// </summary>
        bool? IsNetworkAvailable { get; }

        /// <summary>
        /// Gets the ISO Country Code.
        /// </summary>
        string ICC { get; }

        /// <summary>
        /// Gets the Mobile Country Code.
        /// </summary>
        string MCC { get; }

        /// <summary>
        /// Gets the Mobile Network Code.
        /// </summary>
        string MNC { get; }

        /// <summary>
        /// Gets whether the service can send SMS
        /// </summary>
        bool CanSendSMS { get; }

        /// <summary>
        /// Opens native dialog to dial the specified number.
        /// </summary>
        /// <param name="number">Number to dial.</param>
        void DialNumber(string number);

        void SendSMS(string to, string body);
    }

    public enum NetworkStatus
    {
        /// <summary>
        /// Network not reachable.
        /// </summary>
        NotReachable,

        /// <summary>
        /// Network reachable via carrier data network.
        /// </summary>
        ReachableViaCarrierDataNetwork,

        /// <summary>
        /// Network reachable via WiFi network.
        /// </summary>
        ReachableViaWiFiNetwork,

        /// <summary>
        /// Network reachable via an unknown network
        /// </summary>
        ReachableViaUnknownNetwork
    }

    public enum Orientation
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,
        /// <summary>
        /// The portrait
        /// </summary>
        Portrait = 1,
        /// <summary>
        /// The landscape
        /// </summary>
        Landscape = 2,
        /// <summary>
        /// The portrait up
        /// </summary>
        PortraitUp = 5,
        /// <summary>
        /// The portrait down
        /// </summary>
        PortraitDown = 9,
        /// <summary>
        /// The landscape left
        /// </summary>
        LandscapeLeft = 18,
        /// <summary>
        /// The landscape right
        /// </summary>
        LandscapeRight = 34,
    }

}
