using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;

namespace SACToolbar
{
    public class SACToolbar : IExternalApplication
    {

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            AddToolbar(application);
            return Result.Succeeded;
        }

        static void AddToolbar(UIControlledApplication application)
        {
            application.CreateRibbonTab("SAC");

            RibbonPanel ribbonPanel = application.CreateRibbonPanel("SAC", "Tools");

            String thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            PushButtonData b1Data = new PushButtonData("cmdRotateClockwise", "Rotate Clockwise", thisAssemblyPath, "SACToolbar.RotateClockwise");
            PushButtonData b2Data = new PushButtonData("cmdRotateCounterClockwise", "Rotate Counter Clockwise", thisAssemblyPath, "SACToolbar.RotateCounterClockwise");
            PushButtonData b3Data = new PushButtonData("cmdCircuitEditor", "Circuit Editor", thisAssemblyPath, "SACToolbar.CircuitEditor");

            List<RibbonItem> projectbuttons = new List<RibbonItem>();

            projectbuttons.AddRange(ribbonPanel.AddStackedItems(b1Data, b2Data, b3Data));
        }
    }
}
