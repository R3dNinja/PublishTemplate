using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace PublishTemplate
{
    class ImportViews
    {
        public ElementId titleBlockTypeId;
        public Dictionary<Element, Element> sheetMap;
        public Dictionary<Element, Element> viewMap;
        public Dictionary<ElementId, ElementId> sheetIDMap;
        public Dictionary<ElementId, ElementId> draftingViewIDMap;
        public Dictionary<ElementId, ElementId> scheduleGraphicIDMap;
        public ICollection<Element> sourceSheets;
        public ICollection<Viewport> sourceViewports;
        public ICollection<ViewDrafting> sourceViews;
        private string _Discipline;
        private string _DisciplineCode;
        private string _DisciplineSubCode;

        public void copySheets(Document detailLibrary, Document templateFile, UIApplication uiapp)
        {
            this.sourceSheets = GetListOfSheetsToCopy(detailLibrary);
            RetrieveTitleBlock(templateFile, "A2.30");
            this.sheetMap = new Dictionary<Element, Element>();
            this.sheetIDMap = new Dictionary<ElementId, ElementId>();
            this.scheduleGraphicIDMap = new Dictionary<ElementId, ElementId>();
            this.draftingViewIDMap = new Dictionary<ElementId, ElementId>();
            foreach (Element ss in sourceSheets)
            {
                ViewSheet vs = ss as ViewSheet;
                duplicateSheets(detailLibrary, templateFile, vs);
                //duplicateSchedules(detailLibrary, templateFile, vs);
                duplicateDraftingViews(detailLibrary, templateFile, vs);
                placeViewsOnNewSheets(detailLibrary, templateFile, vs);
            }
            //extractViewportsonSheets(detailLibrary);
            //extractViewsonSheets(detailLibrary);
        }

        private void placeViewsOnNewSheets(Document detailLibrary, Document templateFile, ViewSheet sheet)
        {
            ICollection<ElementId> placedViewports = sheet.GetAllViewports();
            ElementId sourceSheetId = sheet.Id;
            ElementId newSheetId;
            //if (this.sheetIDMap.ContainsKey(sourceSheetId))
            //{
                newSheetId = sheetIDMap[sourceSheetId];
            //}
            ICollection<Viewport> viewsOnSheet = extractViewportsonSheets(detailLibrary, sheet);
            foreach (Viewport viewPort in viewsOnSheet)
            {
                ElementId viewsId = viewPort.ViewId;
                ElementId newViewId;
                //if (this.draftingViewIDMap.ContainsKey(viewsId))
                //{
                    newViewId = draftingViewIDMap[viewsId];
                //}
                string detailNumber = viewPort.get_Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER).AsString();
                XYZ temp = viewPort.GetBoxCenter();
                ElementId temp2 = viewPort.get_Parameter(BuiltInParameter.ELEM_FAMILY_AND_TYPE_PARAM).AsElementId();
                ElementType type = detailLibrary.GetElement(temp2) as ElementType;
                ElementId typeID = type.GetTypeId();
                string typeName = type.Name;

                ElementId viewTitle = temp2;

                Element vpp = new FilteredElementCollector(templateFile).OfCategory(BuiltInCategory.OST_Viewports).FirstOrDefault(q => q.Name == type.Name);

                ElementType type2 = templateFile.GetElement(viewTitle) as ElementType;


                using (Transaction t = new Transaction(templateFile, "Place View"))
                {
                    t.Start();
                    Viewport newvp = Viewport.Create(templateFile, newSheetId, newViewId, temp);
                    newvp.get_Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER).Set(detailNumber);
                    newvp.ChangeTypeId(vpp.GetTypeId());
                    t.Commit();
                }

            }

        }

        private ICollection<Viewport> extractViewportsonSheets(Document _source, ViewSheet sheet)
        {
            IEnumerable<Viewport> sheetViewports = getViewPortsOnSheets(sheet, _source);
            return sheetViewports as ICollection<Viewport>;
        }

        /*private void extractViewsonSheets(Document _source)
        {
            foreach (Element vs in this.sourceSheets)
            {
                //ICollection<ViewDrafting> placedViews = getViewsOnSheets(vs, _source);
                this.sourceViews = getViewsPlacedOnSheets(vs, _source);

            }
        }*/

        private ICollection<Viewport> getViewPortsOnSheets(Element _sheet, Document _source)
        {
            ViewSheet currentSheet = _sheet as ViewSheet;
            ICollection<ElementId> placedViewports = currentSheet.GetAllViewports();
            List<Viewport> viewPorts = new List<Viewport>();
            foreach (ElementId eId in placedViewports)
            {
                Element temp = _source.GetElement(eId);
                if (temp.GetType().ToString() == "Autodesk.Revit.DB.Viewport")
                {
                    viewPorts.Add(temp as Viewport);
                }
            }
            return viewPorts;
        }

        private ICollection<ViewDrafting> getViewsPlacedOnSheets(Element _sheet, Document _source)
        {
            ViewSheet currentSheet = _sheet as ViewSheet;
            ICollection<ElementId> placedViews = currentSheet.GetAllPlacedViews();
            List<ViewDrafting> views = new List<ViewDrafting>();
            foreach (ElementId eId in placedViews)
            {
                Element temp = _source.GetElement(eId);
                if (temp.GetType().ToString() == "Autodesk.Revit.DB.ViewDrafting")
                {
                    views.Add(temp as ViewDrafting);
                }
            }
            return views;
        }

        private void duplicateDraftingViews(Document detailLibrary, Document templateFile, ViewSheet sheet)
        {
            IEnumerable<ViewDrafting> draftingViews = getViewsOnSheets(sheet, detailLibrary);
            Dictionary<ElementId, ElementId> views = DuplicateViewUtils.DuplicateDraftingViews(detailLibrary, draftingViews, templateFile);
            foreach (var view in views)
                this.draftingViewIDMap.Add(view.Key, view.Value);
            //this.draftingViewIDMap = DuplicateViewUtils.DuplicateDraftingViews(detailLibrary, draftingViews, templateFile);
            //int numDrafting = draftingViews.Count<ViewDrafting>(); 
        }

        private void duplicateSchedules(Document detailLibrary, Document templateFile, ViewSheet sheet)
        {
            IEnumerable<ViewSchedule> viewSheets = getViewSchedulesOnSheets(sheet, detailLibrary);
            Dictionary<ElementId, ElementId> schedules = DuplicateViewUtils.DuplicateSchedules(detailLibrary, viewSheets, templateFile);
            foreach (var schedule in schedules)
                this.scheduleGraphicIDMap.Add(schedule.Key, schedule.Value);

        }

        private ICollection<Element> GetListOfSheetsToCopy(Document detailLibrary)
        {
            Configuration config = new Configuration();
            List<SheetData> list = config.sheetNames();
            //List<ElementId> sheetList = new List<ElementId>();
            FilteredElementCollector collector = new FilteredElementCollector(detailLibrary);
            ICollection<Element> copyViewSheets = new Collection<Element>();

            collector
                .OfClass(typeof(ViewSheet));

            foreach (ViewSheet viewSheet in collector)
            {
                foreach (SheetData sheet in list)
                    if (viewSheet.SheetNumber == sheet.sheetNumber)
                    {
                        copyViewSheets.Add(viewSheet);
                    }
            }
            return copyViewSheets;
        }

        private void parameterUtility(Element element)
        {
            List<string> parameterItems = new List<string>();
            ParameterSet parameters = element.Parameters;
            foreach (Parameter param in parameters)
            {
                if (param == null) continue;

                if (param.Definition.Name == "*Discipline")
                {
                    this._Discipline = param.AsString().ToUpper();
                }

                if (param.Definition.Name == "*Discipline Code")
                {
                    this._DisciplineCode = param.AsString().ToUpper();
                }

                if (param.Definition.Name == "*Discipline Subcode")
                {
                    this._DisciplineSubCode = param.AsString().ToUpper();
                }
            }
        }

        private void CopySheets(ICollection<Element> _copySheets, Document _detailLibrary, Document _templateFile, UIApplication uiapp)
        {

            foreach (Element viewsheet in _copySheets)
            {
                ElementId detailSheetId = viewsheet.Id;
                string _Name = viewsheet.Name;
                ViewSheet viewSheet = viewsheet as ViewSheet;
                string _SheetNumber = viewSheet.SheetNumber;

                //Get Custom Sheet Parameters and set them
                parameterUtility(viewsheet);
                //IEnumerable<Viewport> viewPorts = collectViewports(viewsheet, _detailLibrary);
                //IEnumerable<ViewDrafting> viewsDrafting = collectDraftingViews(viewPorts, _detailLibrary);
                
                ElementId _titleBlockId = getTitleBlockID();
                using (Transaction tPain = new Transaction(_templateFile))
                {
                    tPain.Start("Create New Sheet");
                    ViewSheet myViewSheet = ViewSheet.Create(_templateFile, _titleBlockId);
                    myViewSheet.Name = _Name;
                    myViewSheet.SheetNumber = _SheetNumber;
                    myViewSheet.LookupParameter("*Discipline").Set(this._Discipline);
                    myViewSheet.LookupParameter("*Discipline Code").Set(this._DisciplineCode);
                    myViewSheet.LookupParameter("*Discipline Subcode").Set(this._DisciplineSubCode);
                    buildElementMap(viewSheet, myViewSheet);
                    tPain.Commit();
                }
                //MessageBox.Show(_SheetNumber + " " + _Name);
               
            }
        }

        private void buildElementMap(Element sourceSheet, Element destinationSheet)
        {
            this.sheetMap.Add(sourceSheet, destinationSheet);
        }

        private ElementId ViewSheetConvertToElementId(ViewSheet viewsheet)
        {
            return viewsheet.Id;
        }

        private bool RetrieveTitleBlock(Document doc, string searchString)
        {
            FilteredElementCollector a;
            Parameter p;
            a = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_TitleBlocks).OfClass(typeof(FamilyInstance));
            foreach (FamilyInstance e in a)
            {
                p = e.get_Parameter(BuiltInParameter.SHEET_NUMBER);
                string sheet_number = p.AsString();
                if (sheet_number.Contains(searchString))
                {
                    ElementId typeId = e.GetTypeId();
                    setTitleBlockID(typeId);
                    return true;
                }
            }
            return false;
        }

        private void setTitleBlockID(ElementId typeId)
        {
            titleBlockTypeId = typeId;
        }

        private ElementId getTitleBlockID()
        {
            return titleBlockTypeId;
        }

        private IEnumerable<ViewDrafting> getViewsOnSheets(Element _sheet, Document _source)
        {
            ViewSheet currentSheet = _sheet as ViewSheet;
            ICollection<ElementId> placedViews = currentSheet.GetAllPlacedViews();
            List<ViewDrafting> views = new List<ViewDrafting>();
            foreach (ElementId eId in placedViews)
            {
                Element temp = _source.GetElement(eId);
                if (temp.GetType().ToString() == "Autodesk.Revit.DB.ViewDrafting")
                {
                    views.Add(temp as ViewDrafting);
                }
            }
            return views;
        }

        private IEnumerable<ViewSchedule> getViewSchedulesOnSheets(Element _sheet, Document _source)
        {
            ViewSheet currentSheet = _sheet as ViewSheet;

            FilteredElementCollector collector = new FilteredElementCollector(_source);
            collector.OfClass(typeof(ScheduleSheetInstance));
            //collector.WhereElementIsViewIndependent();

            var scheduleColl = from elements in collector
                               let type = elements as ScheduleSheetInstance
                               where type.OwnerViewId == currentSheet.Id
                               select type;
            //ICollection<ElementId> placedViews = currentSheet.GetAllPlacedViews();
            ICollection<ElementId> placedViews = currentSheet.GetAllViewports();
            List<ViewSchedule> views = new List<ViewSchedule>();
            foreach (ScheduleSheetInstance ssi in scheduleColl)
            {
                Element temp = _source.GetElement(ssi.ScheduleId);
                if (temp.GetType().ToString() == "Autodesk.Revit.DB.ViewSchedule")
                {
                    ViewSchedule _temp = temp as ViewSchedule;
                    if (!_temp.IsTitleblockRevisionSchedule)
                    {
                        views.Add(temp as ViewSchedule);
                    }
                }
            }
            return views;
        }

        private void duplicateSheets(Document detailLibrary, Document templateFile, ViewSheet sheet)
        {
            ICollection<ElementId> sheetToCopy = new Collection<ElementId>();
            sheetToCopy.Add(sheet.Id);

            ICollection<ElementId> copyIdsViewSpecific = new Collection<ElementId>();
            foreach (Element e in new FilteredElementCollector(detailLibrary).OwnedByView(sheet.Id))
            {
                // do not put viewports into this collection because they cannot be copied
                if (!(e is Viewport))
                    copyIdsViewSpecific.Add(e.Id);
            }

            ElementId newsheet;
            using (Transaction tDelete = new Transaction(detailLibrary, "Clear Sheet"))
            {
                tDelete.Start();

                IList<Viewport> viewports = new FilteredElementCollector(detailLibrary).OfClass(typeof(Viewport)).Cast<Viewport>()
                    .Where(q => q.SheetId == sheet.Id).ToList();

                foreach (Viewport vp in viewports)
                {
                    detailLibrary.Delete(vp.Id);
                }

                using (Transaction t = new Transaction(templateFile, "Duplicate Sheet"))
                {
                    t.Start();
                    ViewSheet newSheet = templateFile.GetElement(ElementTransformUtils.CopyElements(detailLibrary, sheetToCopy, templateFile, Transform.Identity, new CopyPasteOptions()).First()) as ViewSheet;
                    newsheet = newSheet.Id;
                    ElementTransformUtils.CopyElements(sheet, copyIdsViewSpecific, newSheet, Transform.Identity, new CopyPasteOptions());
                    t.Commit();
                }
                tDelete.RollBack();
            }

            this.sheetIDMap.Add(sheet.Id, newsheet);
        }

        private ICollection<ElementId> sheetToCollection(ViewSheet sheet)
        {
            List<ElementId> sheetToCopy = new List<ElementId>();
            sheetToCopy.Add(sheet.Id);
            return sheetToCopy;
        }
    }
}
