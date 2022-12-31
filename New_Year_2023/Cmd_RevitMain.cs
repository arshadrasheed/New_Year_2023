using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Revit API Namespace

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

#endregion


namespace New_Year_2023
{
    [Transaction(TransactionMode.Manual)]
    public class Cmd_RevitMain : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            ///<summary>
            ///Load Families Before Run this Plugin
            ///New_Year_2023\Families ---<<Family_Location
            /// </summary>

            //Revit Data Base
            Document revitDoc = commandData.Application.ActiveUIDocument.Document;

            //Activate Families
            using (Transaction activateFamilies = new Transaction(revitDoc, "Activate Family"))
            {
                activateFamilies.Start();

                foreach (string familySymbolName in Global_Properties.Happy)
                {
                    //Get Family Symbol
                    FamilySymbol sym = RevitUtils.GetFamilySymbol(revitDoc, familySymbolName);

                    RevitUtils.ActivateFamily(sym);

                }

                foreach (string familySymbolName in Global_Properties.New)
                {
                    //Get Family Symbol
                    FamilySymbol sym = RevitUtils.GetFamilySymbol(revitDoc, familySymbolName);

                    RevitUtils.ActivateFamily(sym);
                }

                foreach (string familySymbolName in Global_Properties.Year)
                {
                    //Get Family Symbol
                    FamilySymbol sym = RevitUtils.GetFamilySymbol(revitDoc, familySymbolName);

                    RevitUtils.ActivateFamily(sym);
                }

                activateFamilies.Commit();
            }

            //HAPPY---<<<Text Creation
            using (Transaction createHappy = new Transaction(revitDoc, "Create Happy Text"))
            {
                createHappy.Start();

                double alphabetPitch = 700 / 304.8;

                foreach (string familySymbolName in Global_Properties.Happy)
                {
                    //Get Family Symbol
                    FamilySymbol sym = RevitUtils.GetFamilySymbol(revitDoc, familySymbolName);

                    //Place FamilySymbol
                    FamilyInstance instance = RevitUtils.PlaceFamily(revitDoc, new XYZ(0, 0, 0), sym);

                    //Traslation Vector
                    XYZ p1 = RevitUtils.GetCentriod(sym);
                    XYZ p2 = new XYZ(alphabetPitch, 0, 0);
                    XYZ transVector = p2.Subtract(p1);

                    //Move Family
                    ElementTransformUtils.MoveElement(revitDoc, instance.Id, new XYZ(transVector.X, 0, 0));

                    alphabetPitch += 700 / 304.8;

                    RevitUtils.OverrideElement(revitDoc, instance.Id, 255, 0, 255);

                }

                createHappy.Commit();
            }

            //NEW----<<<Text Creation
            using (Transaction createNew = new Transaction(revitDoc, "Create New Text"))
            {
                createNew.Start();

                double alphabetPitch = 2100 / 304.8;

                foreach (string familySymbolName in Global_Properties.New)
                {
                    //Get Family Symbol
                    FamilySymbol sym = RevitUtils.GetFamilySymbol(revitDoc, familySymbolName);

                    //Place FamilySymbol
                    FamilyInstance instance = RevitUtils.PlaceFamily(revitDoc, new XYZ(0, -(750/304.8), 0), sym);

                    //Traslation Vector
                    XYZ p1 = RevitUtils.GetCentriod(sym);
                    XYZ p2 = new XYZ(alphabetPitch, 0, 0);
                    XYZ transVector = p2.Subtract(p1);

                    //Move Family
                    ElementTransformUtils.MoveElement(revitDoc, instance.Id, new XYZ(transVector.X, 0, 0));

                    alphabetPitch += 700 / 304.8;

                    RevitUtils.OverrideElement(revitDoc, instance.Id, 255, 128, 64);


                }

                createNew.Commit();
            }

            //YEAR----<<<Text Creation
            using (Transaction createNew = new Transaction(revitDoc, "Create New Text"))
            {
                createNew.Start();

                double alphabetPitch = 2800 / 304.8;

                foreach (string familySymbolName in Global_Properties.Year)
                {
                    //Get Family Symbol
                    FamilySymbol sym = RevitUtils.GetFamilySymbol(revitDoc, familySymbolName);

                    //Place FamilySymbol
                    FamilyInstance instance = RevitUtils.PlaceFamily(revitDoc, new XYZ(0, -(1500 / 304.8), 0), sym);

                    //Traslation Vector
                    XYZ p1 = RevitUtils.GetCentriod(sym);
                    XYZ p2 = new XYZ(alphabetPitch, 0, 0);
                    XYZ transVector = p2.Subtract(p1);

                    //Move Family
                    ElementTransformUtils.MoveElement(revitDoc, instance.Id, new XYZ(transVector.X, 0, 0));

                    alphabetPitch += 700 / 304.8;

                    RevitUtils.OverrideElement(revitDoc, instance.Id, 0, 255, 0);

                }

                createNew.Commit();
            }


            return Result.Succeeded;
        }
    }
}
