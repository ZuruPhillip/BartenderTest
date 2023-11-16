using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Windows.Forms;
using BartenderHelper;

namespace TestForHelper
{
    public partial class fTestForHelper : Form
    {
        //定义打印对象
        BartenderHelper.BartenderHelper Printer = new BartenderHelper.BartenderHelper();

        public fTestForHelper()
        {
            InitializeComponent();
        }

        private void fTestForHelper_Load(object sender, EventArgs e)
        {
            btnInitial_Click(sender,e);
        }

        /// <summary>
        /// Initial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInitial_Click(object sender, EventArgs e)
        {
            if (cmbLabel.Items.Count>0)
            {
                cmbLabel.SelectedIndex = 0;
            }
            //初始化打印机
            cmbPrinter.Items.AddRange(ConfigLoad.GetLocalPrinterList());
            //初始化标签模板
            cmbLabel.Items.AddRange(ConfigLoad.GetLocalBTWFile());
        }

        /// <summary>
        /// 选择标签模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbLabel_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbLabel.Text = cmbLabel.SelectedItem.ToString();
        }

        /// <summary>
        /// 选择打印机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbPrinter.Text = cmbPrinter.SelectedItem.ToString();
            //cmbPrinter.Text = GetDefaultPrinterName();
        }

        /// <summary>
        /// 打印条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //BartenderHeperUtils.PrintLabel("", "","SERIAL_NUMBER", listSN,2);
                List<string> listSubStringName = new List<string> { "SERIAL_NUMBER" };
                List<List<string>> listSubStringValue = new List<List<string>>{ };
                for (int i = 0; i < lstSNNew.Items.Count; i++)
                {
                    listSubStringValue.Add(new List<string> { lstSNNew.Items[i].ToString() });
                }

                Printer.PrintStart(cmbLabel.Text, cmbPrinter.Text);
                Printer.lstSubStringName = listSubStringName;
                Printer.lstSubStringValue = listSubStringValue;
                Printer.PrintLabel();
                Printer.PrintEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public string[] GetLocalPrinter()
        {
            string[] ListPrinter = new string[PrinterSettings.InstalledPrinters.Count];
            //获取当前打印机
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                ListPrinter[i] = (PrinterSettings.InstalledPrinters[i].ToString());
            }
            return ListPrinter;
        }
    }
}
