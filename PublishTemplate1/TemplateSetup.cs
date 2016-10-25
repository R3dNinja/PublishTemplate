using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PublishTemplate
{
    public partial class TemplateSetup : Form
    {
        public string masterTemplateFile { get; set; }
        public string outputFile { get; set; }
        public string returnType { get; set; }
        public DateTime templateDate { get; set; }
        public ConfigurationProperties returnConfig { get; set; }

        public TemplateSetup()
        {
            InitializeComponent();

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var checkedButton = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            this.returnType = checkedButton.Name;
            this.masterTemplateFile = txtBoxMasterTemplate.Text;
            this.outputFile = txtBoxWorksharedTemplate.Text;
            this.templateDate = dateTimePicker1.Value.Date;
            this.DialogResult = DialogResult.OK;
            Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Revit Template Files (*.rte)|*.rte|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;
            ofd.Title = "Select Master Revit Template";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtBoxMasterTemplate.Text = ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Revit Template Files (*.rvt)|*.rvt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;
            ofd.Title = "Select Worksared Revit Template";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtBoxWorksharedTemplate.Text = ofd.FileName;
            }
        }

    }
}
