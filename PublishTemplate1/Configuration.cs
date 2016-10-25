using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishTemplate
{
    class Configuration
    {

        public List<SheetData> sheetNames()
        {
            List<SheetData> list = new List<SheetData>();
            //list.Add(new SheetData("A10.10", "CASEWORK LEGENDS & SCHEDULES"));
            //list.Add(new SheetData("A0.50", "FIRE RESISTANCE ASSEMBLIES - PARTITIONS"));
            list.Add(new SheetData("A0.51", "FIRE RESISTANCE ASSEMBLIES - PENETRATIONS"));
            list.Add(new SheetData("A0.52", "FIRE RESISTANCE ASSEMBLIES - COLUMNS"));
            list.Add(new SheetData("A0.53", "FIRE RESISTANCE ASSEMBLIES - CURTAIN WALL"));
            list.Add(new SheetData("A0.54", "FIRE RESISTANCE ASSEMBLIES - FLOORS AND CEILINGS"));
            list.Add(new SheetData("A0.60", "TYPICAL MOUNTING HEIGHTS"));
            list.Add(new SheetData("A6.10", "DOOR AND FRAME TYPES"));
            list.Add(new SheetData("A6.60", "PARTITON TYPES - INTERIOR"));
            list.Add(new SheetData("A6.61", "PARTITON TYPES - SHAFT WALLS & CMU"));
            list.Add(new SheetData("A6.62", "EXTERIOR WALL TYPES"));
            list.Add(new SheetData("A6.63", "EXTERIOR WALL TYPES"));
            list.Add(new SheetData("A10.10", "CASEWORK LEGEND & SCHEDULES"));
            list.Add(new SheetData("A10.11", "CASEWORK TYPES"));
            list.Add(new SheetData("A10.12", "CASEWORK TYPES"));
            list.Add(new SheetData("A10.13", "CASEWORK DETAILS"));
            list.Add(new SheetData("A10.14", "CASEWORK DETAILS"));
            list.Add(new SheetData("A10.15", "CASEWORK DETAILS"));
            list.Add(new SheetData("A10.16", "CASEWORK DETAILS"));
            list.Add(new SheetData("A10.17", "CASEWORK DETAILS"));
            list.Add(new SheetData("A10.18", "CASEWORK DETAILS"));
            return list;
        }
    }
}
