using System;
using System.Collections.Generic;
using System.Linq;

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;


namespace SACToolbar {

    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    public class RotateCounterClockwise : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData,
        ref string message, ElementSet elements)
        {
            //Get application and document objects
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;

            try
            {
                // Select some elements in Revit before invoking this command

                // Get the handle of current document.
                UIDocument uidoc = commandData.Application.ActiveUIDocument;

                // Get the element selection of current document.
                Selection selection = uidoc.Selection;
                ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();

                if (0 == selectedIds.Count)
                {
                    // If no elements selected.
                    TaskDialog.Show("Revit", "You haven't selected any elements.");
                }
                else
                {
                    String info = "Ids of selected elements in the document are: ";
                    foreach (ElementId id in selectedIds)
                    {


                        using (Transaction trans = new Transaction(doc))
                        {
                            trans.Start("Lab");
                            LocationRotate(doc.GetElement(id));
                            trans.Commit();
                        }
                        return Result.Succeeded;
                    }

                    TaskDialog.Show("Revit", info);
                }
            }
            catch (Exception e)
            {
                message = e.Message;
                return Autodesk.Revit.UI.Result.Failed;
            }

            return Autodesk.Revit.UI.Result.Succeeded;
        }



        bool LocationRotate(Element element)
        {
            bool rotated = false;
            // Rotate the element via its location curve.
            LocationPoint location = element.Location as LocationPoint;
            LocationCurve curve = element.Location as LocationCurve;
            if (null != curve)
            {
                Curve line = curve.Curve;
                XYZ aa = line.GetEndPoint(0);
                XYZ cc = new XYZ(aa.X, aa.Y, aa.Z + 10);
                Line axis = Line.CreateBound(aa, cc);
                rotated = curve.Rotate(axis, Math.PI / 2.0);
            }
            else if (null != location)
            {
                XYZ aa = location.Point;
                XYZ cc = new XYZ(aa.X, aa.Y, aa.Z + 10);
                Line axis = Line.CreateBound(aa, cc);
                rotated = location.Rotate(axis, Math.PI / 2.0);
            }
            return rotated;
        }

    }
}