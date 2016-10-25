#region Namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

namespace PublishTemplate
{
    class RenameSheetsViews
    {

        public void renameSheets(Document doc)
        {
            //UIApplication app = uiapp;
            //UIDocument uidoc = uiapp.ActiveUIDocument;
            //Document doc = app.ActiveUIDocument.Document;
            FilteredElementCollector col = new FilteredElementCollector(doc);
            col.OfCategory(BuiltInCategory.OST_Sheets);
            col.OfClass(typeof(ViewSheet));
            ArrayList list = new ArrayList();
            foreach (Element sheet in col)
            {
                ViewSheet vs = sheet as ViewSheet;
                var number = vs.SheetNumber;
                //Element e = vs as Element;

                //var subCode = e.LookupParameter("*Discipline Subcode").AsString();
                var subCode = vs.LookupParameter("*Discipline Subcode").AsString();
                if (subCode != null)
                {
                    string replacmentCode = findNewCode(subCode);
                    if (number.StartsWith("A"))
                    {
                        using (Transaction tx = new Transaction(doc))
                        {
                            tx.Start("Rename Sheet for Interior Template");
                            vs.get_Parameter(BuiltInParameter.SHEET_NUMBER).Set(vs.SheetNumber.Replace("A", "IA"));
                            vs.LookupParameter("*Discipline Subcode").Set(replacmentCode);
                            tx.Commit();
                        }
                    }
                }
            }
        }

        private string findNewCode(string subCode)
        {
            var searchString = subCode.ToUpper();
            string replacementCode;
            if (searchString.StartsWith("A"))
            {
                replacementCode = "I" + searchString;
            }
            else
            {
                replacementCode = searchString;
            }
            return replacementCode;
        }
    }
}
