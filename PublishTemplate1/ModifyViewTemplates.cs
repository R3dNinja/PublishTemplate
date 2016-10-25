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
    class ModifyViewTemplates
    {
        public void modifyTemplates(Document doc)
        {
            ArrayList viewTemplates = new ArrayList();
            ArrayList viewsWithoutTemplate = new ArrayList();
            viewTemplates = collectViewTemplates(doc);
            renameViewsinTemplates(doc, viewTemplates);
            viewsWithoutTemplate = collectViewsWithoutTemplate(doc);
            modifyViewFamilies(doc);
        }

        public void modifyViewFamilies(Document doc)
        {
            ArrayList areaViews = new ArrayList();
            ArrayList viewFamlies = new ArrayList();
            viewFamlies = collectViewFamilies(doc);
            areaViews = collectAreaViews(doc);
            foreach (ViewFamilyType vft in viewFamlies)
            {
                using (Transaction tx = new Transaction(doc))
                {
                    tx.Start("Rename ViewFamilies for Interior Template");
                    var temp = vft.Name;
                    vft.Name = "I" + temp;
                    tx.Commit();
                }
            }
            foreach (AreaScheme AS in areaViews)
            {
                using (Transaction tx = new Transaction(doc))
                {
                    tx.Start("Rename AreaSchemes for Interior Template");
                    var temp = AS.Name;
                    AS.Name = "I" + temp;
                    tx.Commit();
                }
            }

        }

        private ArrayList collectAreaViews(Document doc)
        {
            ArrayList list = new ArrayList();
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector
                .OfClass(typeof(AreaScheme));

            foreach (AreaScheme AS in collector)
            {
                var name = AS.Name;
                if (AS.Name.StartsWith("A"))
                {
                    list.Add(AS);
                }
            }
            return list;
        }

        private ArrayList collectViewFamilies(Document doc)
        {
            ArrayList list = new ArrayList();
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector
                .OfClass(typeof(ViewFamilyType));

            foreach (ViewFamilyType vf in collector)
            {
                var name = vf.Name;
                if (vf.Name.StartsWith("A"))
                {
                    list.Add(vf);
                }
            }
            return list;
        }

        private ArrayList collectViewTemplates(Document doc)
        {
            ArrayList list = new ArrayList();
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector
                .OfClass(typeof(View));

            foreach (View view in collector)
            {
                if (view.IsTemplate)
                {
                    list.Add(view);
                }
            }
            return list;
        }

        private ArrayList collectViewsWithoutTemplate(Document doc)
        {
            ArrayList list = new ArrayList();
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector
                .OfClass(typeof(View));

            ElementId negativeOne = new ElementId(-1);

            foreach (View view in collector)
            {
                if (!view.IsTemplate)
                {
                    switch (view.ViewType)
                    {
                        case ViewType.AreaPlan:
                            if (view.ViewTemplateId == negativeOne)
                            {
                                list.Add(view);
                            }
                            break;
                        case ViewType.CeilingPlan:
                            if (view.ViewTemplateId == negativeOne)
                            {
                                list.Add(view);
                            }
                            break;
                        case ViewType.ColumnSchedule:
                            break;
                        case ViewType.CostReport:
                            break;
                        case ViewType.Detail:
                            if (view.ViewTemplateId == negativeOne)
                            {
                                list.Add(view);
                            }
                            break;
                        case ViewType.DraftingView:
                            if (view.ViewTemplateId == negativeOne)
                            {
                                list.Add(view);
                            }
                            break;
                        case ViewType.DrawingSheet:
                            break;
                        case ViewType.Elevation:
                            if (view.ViewTemplateId == negativeOne)
                            {
                                list.Add(view);
                            }
                            break;
                        case ViewType.EngineeringPlan:
                            break;
                        case ViewType.FloorPlan:
                            if (view.ViewTemplateId == negativeOne)
                            {
                                list.Add(view);
                            }
                            break;
                        case ViewType.Internal:
                            break;
                        case ViewType.Legend:
                            break;
                        case ViewType.LoadsReport:
                            break;
                        case ViewType.PanelSchedule:
                            break;
                        case ViewType.PresureLossReport:
                            break;
                        case ViewType.Rendering:
                            break;
                        case ViewType.Report:
                            break;
                        case ViewType.Schedule:
                            break;
                        case ViewType.Section:
                            if (view.ViewTemplateId == negativeOne)
                            {
                                list.Add(view);
                            }
                            break;
                        case ViewType.ThreeD:
                            break;
                        case ViewType.Undefined:
                            break;
                        case ViewType.Walkthrough:
                            break;
                        default:
                            break;

                    }
                }
            }
            return list;
        }

        private void renameViewsinTemplates(Document doc, ArrayList viewTemplates)
        {
            foreach (View viewTemplate in viewTemplates)
            {
                var existingSubCode = viewTemplate.LookupParameter("*Discipline Subcode").AsString();
                var existingName = viewTemplate.Name;

                String replacementCode = findNewCode(existingSubCode);
                String replacementName = updateViewTemplateName(existingName);
                using (Transaction tx = new Transaction(doc))
                {
                    tx.Start("Rename Subcode in View Templates for Interior Template");
                    var temp = viewTemplate.LookupParameter("*Discipline Subcode").Set(replacementCode);
                    viewTemplate.Name = replacementName;
                    tx.Commit();
                }
            }
        }

        private string updateViewTemplateName(string existingName)
        {
            string newName;
            if (existingName.StartsWith("A"))
            {
                newName = "I" + existingName;
            }
            else
            {
                newName = existingName;
            }
            return newName;
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
