#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace PublishTemplate
{
    class FileCommands
    {
        string levelsAndGrids;
        string workset1;

        public Document newFromTemplate(UIApplication uiapp, string templateFile)
        {
            /*UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;*/
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;

            Document NewDoc = app.NewProjectDocument(templateFile);
            return NewDoc;
        }

        public Document openDetailLibrary(UIApplication uiapp, string detailLibraryFile)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;

            Document detailLibrary = app.OpenDocumentFile(detailLibraryFile);
            //UIDocument detailLibrary = uiapp.OpenAndActivateDocument(detailLibraryFile);
            return detailLibrary;
        }

        public void closeDetailLibrary(UIApplication uiapp, Document detailLibrary)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;

            detailLibrary.Close();
        }

        public void enableWorksharing(Document NewDoc, string templateType)
        {
            templateTypeSettings(templateType);
            NewDoc.EnableWorksharing(levelsAndGrids, workset1);
            if (templateType != "SITE")
            {
                createWorksets(NewDoc, templateType);
                moveLinkedFiles(NewDoc);
            }
        }

        public void deleteCandS(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector
                .OfCategory(BuiltInCategory.OST_RvtLinks);
                //.OfClass(typeof(RevitLinkInstance));
            List<Element> links = new List<Element>();
            foreach (Element ele in collector)
            {
                string searchString = ele.Name;
                if (searchString == "CORE_AND_SHELL_MODEL.rvt")
                {
                    links.Add(ele);
                }
            }
            foreach (Element ele in links)
            {
                using (Transaction tx = new Transaction(doc))
                {
                    tx.Start("Removing Core and Shell Link");
                    doc.Delete(ele.Id);
                    tx.Commit();
                }
            }
        }

        private void moveLinkedFiles(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector
                .OfCategory(BuiltInCategory.OST_RvtLinks)
                .OfClass(typeof(RevitLinkInstance));

            WorksetTable worksetTable = doc.GetWorksetTable();

            FilteredWorksetCollector coll = new FilteredWorksetCollector(doc);
            //StringBuilder worksetNames = new StringBuilder();
            //foreach (Workset workset in coll)
            //{
            //    worksetNames.AppendFormat("{0}: {1}\n", workset.Name, workset.Kind);
            //}
            //TaskDialog.Show("Worksets", worksetNames.ToString());

            foreach (Element ele in collector)
            { 
                string searchString = ele.Name;
                Match result = Regex.Match(searchString, @"^.*?(?=_)");
                Parameter wsparam = ele.get_Parameter(BuiltInParameter.ELEM_PARTITION_PARAM);
                //Workset matchingWorkset = null;
                foreach (Workset workset in coll)
                {
                    string worksetName = workset.Name;
                    Match result2 = Regex.Match(worksetName, @"^.*?(?=\s)");
                    if (result.ToString() == result2.ToString())
                    {
                        using (Transaction tx = new Transaction(doc))
                        {
                            tx.Start("Change workset id");
                            wsparam.Set(workset.Id.IntegerValue);
                            tx.Commit();
                        }
                    }
                }
            }
        }

        public void saveWorksharedFile(UIApplication uiapp, Document NewDoc, string savedProject)
        {
            SaveAsOptions options = new SaveAsOptions();
            WorksharingSaveAsOptions wsOptions = new WorksharingSaveAsOptions();
            wsOptions.SaveAsCentral = true;
            options.SetWorksharingOptions(wsOptions);
            options.OverwriteExistingFile = true;
            NewDoc.SaveAs(savedProject, options);
            //NewDoc.Close();
            uiapp.OpenAndActivateDocument(savedProject);
        }

        private void templateTypeSettings(string templateType)
        {
            switch (templateType.ToUpper())
            {
                case "ARCHITECTURE":
                    levelsAndGrids = "ARCH - Shared Levels and Grids";
                    workset1 = "ARCH - Workset1";
                    break;

                case "INTERIOR":
                    levelsAndGrids = "INT - Shared Levels and Grids";
                    workset1 = "INT - Workset1";
                    break;

                case "SITE":
                    levelsAndGrids = "SITE - Shared Levels and Grids";
                    workset1 = "SITE - Workset1";
                    break;

                default:
                    levelsAndGrids = "ARCH - Shared Levels and Grids";
                    workset1 = "ARCH - Workset1";
                    break;

            }
        }

        private void createWorksets(Document NewDoc, string templateType)
        {
            using (Transaction tx = new Transaction(NewDoc))
            {
                tx.Start("CreateWorkset");
                Workset.Create(NewDoc, "ELEC - Link");
                Workset.Create(NewDoc, "MECH - Link");
                Workset.Create(NewDoc, "MEP - Link");
                Workset.Create(NewDoc, "PLUMB - Link");
                Workset.Create(NewDoc, "STRUCT - Link");
                if (templateType == "INTERIOR")
                    Workset.Create(NewDoc, "CORE & SHELL - Link");
                tx.Commit();
            }
        }
    }
}
