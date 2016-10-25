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
using Winform = System.Windows.Forms;
#endregion

namespace PublishTemplate
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        private CleanTemplate clean = new CleanTemplate();
        private ModifyViewTemplates viewTemplates = new ModifyViewTemplates();
        private FileCommands showTime = new FileCommands();
        private ImportViews copyDetails = new ImportViews();
        private StartingViewSettings modifyTemplate = new StartingViewSettings();
        private TemplateSetup dialog;
        private DateTime templateDate;
        private RenameSheetsViews renameSheets = new RenameSheetsViews();
        string template;// = @"P:\Software\Revit\Templates\Kirksey_Master_2015 - In progress - Copy.rte";
        //string detailLibraryPath = @"P:\Library\Detail Library\Detail Library - Copy.rvt";
        string file;// = @"H:\Prj_BIM.rvt";
        string templateType;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;


            Result formResult = ShowForm();
            if (formResult == Result.Succeeded)
            {
                //Create a new file from the selected template
                Document newTemplate = showTime.newFromTemplate(uiapp, template);

                switch (templateType)
                {
                    case "ARCHITECTURE":
                        setupArchTemplate(newTemplate, uiapp, templateDate, templateType);
                        break;

                    case "INTERIOR":
                        setupIntTemplate(newTemplate, uiapp, templateDate, templateType);
                        break;

                    case "SITE":
                        setupSiteTemplate(newTemplate, uiapp, templateDate, templateType);
                        break;

                    default:
                        break;
                }
                showTime.saveWorksharedFile(uiapp, newTemplate, file);
                return Result.Succeeded;
            }
            else
            {
                return Result.Cancelled;
            }
            
        }

        private Result ShowForm()
        {
            dialog = new TemplateSetup();
            var result = dialog.ShowDialog();
            Result returnResult;
            if (result == Winform.DialogResult.OK)
            {
                this.templateType = dialog.returnType;
                this.template = dialog.masterTemplateFile;
                this.file = dialog.outputFile;
                this.templateDate = dialog.templateDate;
                returnResult = Result.Succeeded;
            }
            else
            {
                returnResult = Result.Cancelled;
            }

            return returnResult;
        }

        private void setupArchTemplate(Document newTemplate, UIApplication uiapp, DateTime templateDate, string templateType)
        {
            modifyTemplate.copySheets(newTemplate, uiapp, templateType, templateDate);
            clean.deleteStuff(uiapp, newTemplate, templateType);
            showTime.deleteCandS(newTemplate);
            showTime.enableWorksharing(newTemplate, templateType);

        }

        private void setupIntTemplate(Document newTemplate, UIApplication uiapp, DateTime templateDate, string templateType)
        {
            modifyTemplate.copySheets(newTemplate, uiapp, templateType, templateDate);
            viewTemplates.modifyTemplates(newTemplate);
            clean.deleteStuff(uiapp, newTemplate, templateType);
            renameSheets.renameSheets(newTemplate);
            showTime.enableWorksharing(newTemplate, templateType);
        }

        private void setupSiteTemplate(Document newTemplate, UIApplication uiapp, DateTime templateDate, string templateType)
        {
            modifyTemplate.copySheets(newTemplate, uiapp, templateType, templateDate);
            clean.deleteStuff(uiapp, newTemplate, templateType);
            showTime.deleteCandS(newTemplate);
            showTime.enableWorksharing(newTemplate, templateType);
        }
    }
}
