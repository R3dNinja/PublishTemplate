#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections;
#endregion

namespace PublishTemplate
{
    class CleanTemplate
    {

        ArrayList annotationList = new ArrayList();


        public void deleteStuff(UIApplication uiapp, Document doc, string templateType)
        {
            UIApplication app = uiapp;
            //UIDocument uidoc = uiapp.ActiveUIDocument;
            //Document doc = app.ActiveUIDocument.Document;

            deleteGeomerty(doc);
            deleteViews(doc, templateType);
            deleteAnnotations(doc);
            deleteSheets(doc, templateType);


        }

        private void deleteGeomerty(Document doc)
        {
            Options opt = new Options();
            ArrayList list = new ArrayList();
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector
                .WhereElementIsNotElementType()
                .WhereElementIsViewIndependent();

            foreach (Element element in collector)
            {
                if (null != element.get_Geometry(opt))
                {
                    list.Add(element.Id);
                }
            }

            foreach (ElementId id in list)
            {
                using (Transaction tx = new Transaction(doc))
                {
                    try
                    {
                        tx.Start("Delete Element");
                        doc.Delete(id);
                        tx.Commit();
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void deleteViews(Document doc, string templateCategory)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector
                .WhereElementIsNotElementType()
                .WhereElementIsViewIndependent()
                .OfCategory(BuiltInCategory.OST_Viewers);
            //.OfClass(typeof(ViewSection));
            ArrayList list = new ArrayList();
            foreach (Element viewer in collector)
            {
                string checkName = viewer.Name.ToString();
                bool needToDelete = checkViews(checkName, templateCategory);
                if (needToDelete == true)
                {
                    list.Add(viewer.Id);
                }
            }
            foreach (ElementId id in list)
            {
                using (Transaction tx = new Transaction(doc))
                {
                    try
                    {
                        tx.Start("Delete Views");
                        doc.Delete(id);
                        tx.Commit();
                    }
                    catch
                    {
                    }
                }
            }

            if (templateCategory == "INTERIOR")
            {
                ArrayList deletelist = new ArrayList();
                FilteredElementCollector col = new FilteredElementCollector(doc);
                col
                    .OfCategory(BuiltInCategory.OST_Views);

                foreach (Element view in col)
                {
                    var viewName = view.Name;
                    
                    if (viewName.StartsWith("EXTERIOR"))
                    {
                        deletelist.Add(view);
                    }

                    if (viewName.StartsWith("SITE"))
                    {
                        deletelist.Add(view);
                    }

                    if (viewName.StartsWith("PARKING"))
                    {
                        deletelist.Add(view);
                    }
                }

                if (deletelist.Count > 0)
                {
                    foreach (Element e in deletelist)
                    {
                        var id = e.Id;
                        using (Transaction tx = new Transaction(doc))
                        {
                            try
                            {
                                tx.Start("Delete Views");
                                doc.Delete(id);
                                tx.Commit();
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }

        private void deleteSheets(Document doc, string templateCategory)
        {
            switch (templateCategory.ToUpper())
            {
                case "INTERIOR":
                    deleteInteriorSheets(doc);
                    break;
                case "ARCHITECTURE":
                    deleteArchitectureSheets(doc);
                    break;
                case "SITE":
                    deleteSiteSheets(doc);
                    break;
                default:
                    break;
            }
        }

        private void deleteInteriorSheets(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_Sheets);
            List<ElementId> deleteList = new List<ElementId>();
            foreach (ViewSheet vs in collector)
            {
                ElementId id = vs.Id;
                switch (vs.SheetNumber)
                {
                    case "A1.10":
                        deleteList.Add(id);
                        break;
                    case "A1.20":
                        deleteList.Add(id);
                        break;
                    case "A1.30":
                        deleteList.Add(id);
                        break;
                    case "A1.40":
                        deleteList.Add(id);
                        break;
                    case "A3.10":
                        deleteList.Add(id);
                        break;
                    case "A3.20":
                        deleteList.Add(id);
                        break;
                    case "A4.10":
                        deleteList.Add(id);
                        break;
                    case "A4.20":
                        deleteList.Add(id);
                        break;
                    case "A5.10":
                        deleteList.Add(id);
                        break;
                    case "A5.20":
                        deleteList.Add(id);
                        break;
                    case "A5.30":
                        deleteList.Add(id);
                        break;
                    case "A5.40":
                        deleteList.Add(id);
                        break;
                    case "A7.10":
                        deleteList.Add(id);
                        break;
                    case "A7.20":
                        deleteList.Add(id);
                        break;
                    case "A7.30":
                        deleteList.Add(id);
                        break;
                    case "A7.40":
                        deleteList.Add(id);
                        break;
                    default:
                        break;
                }
            }
            if (deleteList.Count > 0)
            {
                deleteSheet(doc, deleteList);
            }
        }

        private void deleteArchitectureSheets(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_Sheets);
            List<ElementId> deleteList = new List<ElementId>();
            foreach (ViewSheet vs in collector)
            {
                ElementId id = vs.Id;
                var sn = vs.SheetNumber.Substring(0, Math.Min(6, vs.SheetNumber.Length));
                switch (sn)
                {
                    case "IA0.80":
                        deleteList.Add(id);
                        break;

                    default:
                        break;
                }
            }
            if (deleteList.Count > 0)
            {
                deleteSheet(doc, deleteList);
            }
        }

        private void deleteSiteSheets(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_Sheets);
            List<ElementId> deleteList = new List<ElementId>();
            foreach (ViewSheet vs in collector)
            {
                ElementId id = vs.Id;
                switch (vs.SheetNumber)
                {
                    case "A2.10":
                        deleteList.Add(id);
                        break;
                    case "A2.11":
                        deleteList.Add(id);
                        break;
                    case "A2.20":
                        deleteList.Add(id);
                        break;
                    case "A2.21":
                        deleteList.Add(id);
                        break;
                    case "A2.30":
                        deleteList.Add(id);
                        break;
                    case "A2.31":
                        deleteList.Add(id);
                        break;
                    case "A2.32":
                        deleteList.Add(id);
                        break;
                    case "A2.40":
                        deleteList.Add(id);
                        break;
                    case "A2.41":
                        deleteList.Add(id);
                        break;
                    case "A2.50":
                        deleteList.Add(id);
                        break;
                    case "A2.51":
                        deleteList.Add(id);
                        break;
                    case "A2.52":
                        deleteList.Add(id);
                        break;
                    case "A2.53":
                        deleteList.Add(id);
                        break;
                    case "A2.60":
                        deleteList.Add(id);
                        break;
                    case "A2.61":
                        deleteList.Add(id);
                        break;
                    case "A2.70":
                        deleteList.Add(id);
                        break;
                    case "A2.80":
                        deleteList.Add(id);
                        break;
                    case "A2.90":
                        deleteList.Add(id);
                        break;
                    case "A3.10":
                        deleteList.Add(id);
                        break;
                    case "A3.20":
                        deleteList.Add(id);
                        break;
                    case "A4.10":
                        deleteList.Add(id);
                        break;
                    case "A4.20":
                        deleteList.Add(id);
                        break;
                    case "A5.10":
                        deleteList.Add(id);
                        break;
                    case "A5.20":
                        deleteList.Add(id);
                        break;
                    case "A5.30":
                        deleteList.Add(id);
                        break;
                    case "A5.40":
                        deleteList.Add(id);
                        break;
                    case "A6.10":
                        deleteList.Add(id);
                        break;
                    case "A6.20":
                        deleteList.Add(id);
                        break;
                    case "A6.30":
                        deleteList.Add(id);
                        break;
                    case "A6.40":
                        deleteList.Add(id);
                        break;
                    case "A6.50":
                        deleteList.Add(id);
                        break;
                    case "A7.10":
                        deleteList.Add(id);
                        break;
                    case "A7.20":
                        deleteList.Add(id);
                        break;
                    case "A7.30":
                        deleteList.Add(id);
                        break;
                    case "A7.40":
                        deleteList.Add(id);
                        break;
                    case "A8.10":
                        deleteList.Add(id);
                        break;
                    case "A8.20":
                        deleteList.Add(id);
                        break;
                    case "A9.10":
                        deleteList.Add(id);
                        break;
                    case "A9.20":
                        deleteList.Add(id);
                        break;
                    case "A9.30":
                        deleteList.Add(id);
                        break;
                    default:
                        break;
                }
            }
            if (deleteList.Count > 0)
            {
                deleteSheet(doc, deleteList);
            }
        }

        private void deleteSheet(Document doc, List<ElementId> deleteList)
        {
            using (Transaction tx = new Transaction(doc))
            {
                try
                {
                    foreach (ElementId id in deleteList)
                    {
                        tx.Start("Delete Sheet");
                        doc.Delete(id);
                        tx.Commit();
                    }
                }
                catch
                {
                }
            }
        }

        private bool checkViews(string viewName, string templateCategory)
        {
            bool needToDelete;

            switch (templateCategory.ToUpper())
            {
                case "INTERIOR":
                    needToDelete = checkIntViews(viewName);
                    break;
                case "ARCHITECUTRE":
                    needToDelete = checkArchViews(viewName);
                    break;
                case "SITE":
                    needToDelete = checkArchViews(viewName);
                    break;
                default:
                    needToDelete = checkArchViews(viewName);
                    break;
            }
            return needToDelete;
        }

        private ArrayList collectManagementView(Document doc)
        {
            bool matchingView;
            ArrayList list = new ArrayList();
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.OST_Views);

            foreach (Element element in collector)
            {
                matchingView = searchViewNames(element);
                if (matchingView == true)
                {
                    list.Add(element);
                }
            }

            return list;
        }

        private bool searchViewNames(Element element)
        {
            bool match;
            string viewName = element.Name;

            switch (viewName)
            {
                case "DRAWING SET ORGANIZATION":
                    match = true;
                    break;

                case "FILLED REGIONS":
                    match = true;
                    break;

                case "HATCH PATTERN TEMPLATES - HATCHKIT":
                    match = true;
                    break;

                case "LINE SYLES":
                    match = true;
                    break;

                case "ARCHITECTURAL ABBREVIATIONS":
                    match = true;
                    break;

                case "ARCHITECTURAL GRAPHIC SYMBOLS":
                    match = true;
                    break;

                case "NO PLOT CONTROL":
                    match = true;
                    break;

                case "SHEET NOTES":
                    match = true;
                    break;

                default:
                    match = false;
                    break;
            }
            return match;
        }

        private void itterateCollector(FilteredElementCollector collector, ArrayList viewList)
        {
            foreach (Element element in collector)
            {
                bool elementInManagementView = false;
                var viewID = element.OwnerViewId;
                foreach (Element view in viewList)
                {
                    if (view.Id == viewID)
                    {
                        elementInManagementView = true;
                    }
                }

                if (elementInManagementView == false)
                {
                    annotationList.Add(element.Id);
                }
            }
        }

        private void deleteAnnotations(Document doc)
        {
            ArrayList viewList = new ArrayList();
            ArrayList list = new ArrayList();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            FilteredElementCollector collector2 = new FilteredElementCollector(doc);

            viewList = collectManagementView(doc);

            collector
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.OST_DetailComponents);

            itterateCollector(collector, viewList);

            collector = new FilteredElementCollector(doc);

            collector
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.OST_Lines);

            itterateCollector(collector, viewList);

            collector = new FilteredElementCollector(doc);

            collector
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.OST_TextNotes);
            // 
            itterateCollector(collector, viewList);

            collector = new FilteredElementCollector(doc);

            collector
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.OST_ReferenceViewer);

            itterateCollector(collector, viewList);

            collector = new FilteredElementCollector(doc);

            collector
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.OST_Matchline);

            itterateCollector(collector, viewList);

            foreach (ElementId id in annotationList)
            {
                using (Transaction tx = new Transaction(doc))
                {
                    try
                    {
                        tx.Start("Delete detail elements");
                        doc.Delete(id);
                        tx.Commit();
                    }
                    catch
                    {
                    }
                }
            }
        }





        private bool checkArchViews(string viewName)
        {
            bool needToDelete = true;
            if (viewName.Contains("WORKING"))
            {
                needToDelete = false;
            }
            if (viewName.StartsWith("NORTH"))
            {
                needToDelete = false;
            }
            if (viewName.StartsWith("SOUTH"))
            {
                needToDelete = false;
            }
            if (viewName.StartsWith("EAST"))
            {
                needToDelete = false;
            }
            if (viewName.StartsWith("WEST"))
            {
                needToDelete = false;
            }
            if (viewName.StartsWith("ARCHITECTURAL ABBREVIATIONS"))
            {
                needToDelete = false;
            }
            return needToDelete;
        }

        private bool checkSiteViews(string viewName)
        {
            bool needToDelete = true;
            if (viewName.Contains("WORKING"))
            {
                needToDelete = false;
            }
            if (viewName.StartsWith("NORTH"))
            {
                needToDelete = false;
            }
            if (viewName.StartsWith("SOUTH"))
            {
                needToDelete = false;
            }
            if (viewName.StartsWith("EAST"))
            {
                needToDelete = false;
            }
            if (viewName.StartsWith("WEST"))
            {
                needToDelete = false;
            }
            if (viewName.StartsWith("ARCHITECTURAL ABBREVIATIONS"))
            {
                needToDelete = false;
            }
            return needToDelete;
        }

        private bool checkIntViews(string viewName)
        {
            bool needToDelete = true;
            if (viewName.Contains("WORKING"))
            {
                needToDelete = false;
            }
            if (viewName.StartsWith("ARCHITECTURAL ABBREVIATIONS"))
            {
                needToDelete = false;
            }
            return needToDelete;
        }

        private ArrayList AddToCollector(ArrayList list, FilteredElementCollector collector)
        {
            foreach (Element detail in collector)
            {
                string checkName = detail.Name.ToString();
                list.Add(detail.Id);
            }
            return list;
        }
    }
}
