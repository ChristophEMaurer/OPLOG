using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    /// <summary>
    /// This is the part of the BusinessLayer that does NOT contain the ElegantUI License.
    /// The public project contains file BusinessLayerPublic.cs
    /// The private project contains file BusinessLayerPrivate.cs with the license
    /// </summary>
    public partial class BusinessLayer : BusinessLayerCommon
    {
        /// <summary>
        /// Empty funtion that does nothing. Someone with the public source code is not allowed to have my license.
        /// </summary>
        public void ActivateEleganzUiLicense()
        {
            //
            // You must purchase a license for the Elegant UI Runtime Version v2.0.50727, Version 3.3.0.0
            // and activate it like below.
            // Obviously, I cannot hand out my private license key to the public.
            //
            string elegantUiKey = "...";

            Elegant.Ui.RibbonLicenser.LicenseKey = elegantUiKey;
        }
    }
}
