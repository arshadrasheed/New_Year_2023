using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

#region Revit API Namespace

using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

#endregion

namespace New_Year_2023
{
    [Transaction(TransactionMode.Manual)]
    public class App_Main : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {

            string ribbonName = "New Year 2023";
            application.CreateRibbonTab(ribbonName);

            RibbonPanel panel = application.CreateRibbonPanel(ribbonName,"Wishes");
            PushButtonData wishesButtonData = new PushButtonData("wishes", "PRESS ME!!",this.GetType().Assembly.Location, typeof(New_Year_2023.Cmd_RevitMain).FullName);
            PushButton button_01 = panel.AddItem(wishesButtonData) as PushButton;
            BitmapImage pb1Image = new BitmapImage(new Uri("pack://application:,,,/New_Year_2023;component/Resources/fire.ico"));
            button_01.LargeImage = pb1Image;

            return Result.Succeeded;
        }

    }
}
