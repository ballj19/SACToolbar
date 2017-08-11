using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Electrical;

namespace SACToolbar
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private ElementId elemId;
        private String circuit = "0";
        private String panel = "No Panels";
        

        public Form1(Document doc, ElementId elemId)
        {
            InitializeComponent();
            this.elemId = elemId;

            FilteredElementCollector collPanelSchedules =
                      new FilteredElementCollector(doc);

            IList<PanelScheduleView> panelSchedules = collPanelSchedules.OfClass(typeof(PanelScheduleView)) as IList<PanelScheduleView>;

            foreach (Parameter param in doc.GetElement(elemId).Parameters)
            {
                if (param.Definition.Name.Equals("Panel"))
                {
                    this.panel = param.AsString();
                }
                if (param.Definition.Name.Equals("Circuit Number"))
                {
                    this.circuit = param.AsString();
                }
            }

            foreach(Element schedule in panelSchedules)
            {
                if(schedule.Name == this.panel)
                {
                    listBox1.Items.Add("True");
                }
            }

            Form1_Load();
        }

        private void Form1_Load()
        {
            listBox1.Items.Add("test1");
            listBox1.Items.Add("test2");
        }

        private void Button1_Click(System.Object sender, System.EventArgs e)
        {
            label1.Text = this.panel;
            label2.Text = this.circuit;
        }
    }
}
