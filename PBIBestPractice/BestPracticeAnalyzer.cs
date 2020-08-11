using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBIBestPractice
{
    public partial class BestPracticeAnalyzer : Form
    {
        private ModelHelper desktopModel;


        public BestPracticeAnalyzer()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadModel();
        }


        private void LoadModel()
        {

            try
            {
                string[] args = Environment.GetCommandLineArgs();
                if (args.Count() != 3)
                {
                    throw new ApplicationException("Please use this application directly from Power BI.");
                }
                string DesktopConnString = args[1];
                string DesktopModel = args[2];

                desktopModel = new ModelHelper(DesktopConnString, DesktopModel);
                richTextBox1.Text = "Connected to Power BI Dekstop: " + DesktopConnString + " with model:" + DesktopModel;
            }
            catch (Exception ex)
            {
                richTextBox1.Text = richTextBox1.Text + "\r\n Error: " + ex.Message;
                btnAnalyzeModel.Enabled = false;
            }
        }

        private void btnAnalyzeModel_Click(object sender, EventArgs e)
        {
            List<ModelInformation> info = new List<ModelInformation>();
            info = desktopModel.getBasicModelInfo();
            foreach (ModelInformation infoItem in info)
            {
                richTextBox1.Text = richTextBox1.Text + "\r\n" + infoItem.informationText;
            }

            List<ModelInformation> rowinfo = new List<ModelInformation>();
            rowinfo = desktopModel.getTableInfo();
            foreach (ModelInformation infoItem in rowinfo)
            {
                richTextBox1.Text = richTextBox1.Text + "\r\n" + infoItem.informationText;
            }
        }
    }
}
