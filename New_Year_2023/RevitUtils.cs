using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Revit API NameSpace

using Autodesk.Revit.DB;

#endregion

namespace New_Year_2023
{
    public class RevitUtils
    {
       //Get Family Symbol using Name
        public static FamilySymbol GetFamilySymbol(Document revitDoc, string familySymbolName)
        {
            return new FilteredElementCollector(revitDoc)
                .OfClass(typeof(FamilySymbol))
                .OfCategory(BuiltInCategory.OST_GenericModel)
                .Where(x => x.Name.Equals(familySymbolName))
                .Cast<FamilySymbol>()
                .First();
        }

        //Place Family 
        public static FamilyInstance PlaceFamily(Document revitDoc, XYZ location, FamilySymbol familySymbol)
        {

            Level level = new FilteredElementCollector(revitDoc)
                .OfClass(typeof(Level))
                .Where(x => x.Name.Equals("Level 1"))
                .Cast<Level>()
                .First();

            return revitDoc.Create.NewFamilyInstance(location, familySymbol, level, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

        }

        //Activate Family
        public static void ActivateFamily(FamilySymbol familySymbol)
        {
            if (!familySymbol.IsActive)
            {
                familySymbol.Activate();
            }
        }

        //Get Centroid
        public static XYZ GetCentriod(FamilySymbol familySymbol)
        {
            XYZ centroid = null;

            Options opt = new Options();
            opt.ComputeReferences = true;


            GeometryElement geoElement = familySymbol.get_Geometry(opt);

            foreach (GeometryObject obj in geoElement)
            {
                if (obj is Solid)
                {
                    Solid solid = obj as Solid;

                    if (solid.SurfaceArea > 0)
                    {
                        centroid = solid.ComputeCentroid();
                    }

                }

            }

            return centroid;

        }

        //Color Change
        public static void OverrideElement(Document revitDoc, ElementId id, byte r, byte g, byte b)
        {
            ElementId patternId = FillPatternElement.GetFillPatternElementByName(revitDoc, FillPatternTarget.Drafting, "<Solid fill>").Id;
            Color color = new Color(r, g, b);
            OverrideGraphicSettings overrideGraphicSettings = new OverrideGraphicSettings();
            overrideGraphicSettings.SetSurfaceForegroundPatternId(patternId);
            overrideGraphicSettings.SetSurfaceForegroundPatternColor(color);
            revitDoc.ActiveView.SetElementOverrides(id, overrideGraphicSettings);
        }



    }
}
