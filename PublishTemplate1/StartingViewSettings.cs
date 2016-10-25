using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace PublishTemplate
{
    class StartingViewSettings
    {
        private string _TemplateCategory;
        private string _TemplateVersion;
        private string _TemplateModifiedDate;

        public void copySheets(Document templateFile, UIApplication uiapp, string TemplateCategory, DateTime currentDate)
        {
            this._TemplateCategory = TemplateCategory;
            GenerateRevisionDate(currentDate);
            GenerateModifiedDate(currentDate);
            ModifyStartingView(templateFile);
        }

        private void GenerateRevisionDate(DateTime currentDate)
        {
            string year = currentDate.Year.ToString();
            string month = currentDate.Month.ToString();
            this._TemplateVersion = year + "." + month;
        }

        private void GenerateModifiedDate(DateTime currentDate)
        {
            this._TemplateModifiedDate = currentDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        }

        private void ModifyStartingView(Document templateFile)
        {
            using (Transaction t = new Transaction(templateFile, "Update Starting View"))
            {
                t.Start();
                templateFile.ProjectInformation.LookupParameter("TemplateCategory").Set(this._TemplateCategory.ToUpper());
                templateFile.ProjectInformation.LookupParameter("TemplateVersion").Set(this._TemplateVersion);
                templateFile.ProjectInformation.LookupParameter("TemplateModifiedDate").Set(this._TemplateModifiedDate);
                t.Commit();
            }
        }
    }
}
