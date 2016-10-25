using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishTemplate
{
    class SheetData
    {
        public string sheetNumber { get; set; }
        public string sheetName { get; set; }
        public string discipline { get; set; }
        public string disciplineCode { get; set; }
        public string disciplineSubCode { get; set; }

        public SheetData(string _number, string _name)
        {
            this.sheetNumber = _number;
            this.sheetName = _name;
        }
    }
}
