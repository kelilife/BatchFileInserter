using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeLi.BatchFileInserter
{
    public partial class MainForm : Form
    {
        private string[] filePaths;

        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (ofdPat.ShowDialog() == DialogResult.OK)
                filePaths = ofdPat.FileNames;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (filePaths == null || filePaths.Length == 0)
                {
                    MessageBox.Show("Please select files!");

                    btnSelect.PerformClick();

                    return;
                }

                foreach (var filePath in filePaths)
                {
                    var context = File.ReadAllLines(filePath, Encoding.Default);

                    var list = context.ToList();

                    var lineNumber = Convert.ToInt32(txtLineNumber.Text) - 1;

                    list.Insert(lineNumber, txtContext.Text);

                    File.WriteAllLines(filePath, list.ToArray(), Encoding.Default);
                }

                MessageBox.Show("Inserts successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}