using System;
using System.Data;
using System.Windows.Forms;
using TeamNet.Data.FileExport;

namespace WinFormsExample
{
    public partial class DataExportTestForm : Form
    {
        readonly DataSet _sourceDataSet;

        public DataExportTestForm()
        {
            InitializeComponent();

            _sourceDataSet = GetTestDataSet();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _dataGridView.DataSource = _sourceDataSet;
            _dataGridView.DataMember = "TestDataTable";
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _exportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "dBase file (*.dbf)|*.dbf|CSV (Comma delimited) (*.csv)|*.csv|Fixed length strings file (*.txt)|*.txt|XML Spreadsheet 2003 (*.xls)|*.xls";
            saveFileDialog.FileName = "DataExportTest";

            DialogResult result = saveFileDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                DataFileExport dataFileExport;
                switch (saveFileDialog.FilterIndex)
                {
                    case 1: 
                        dataFileExport = DataFileExport.CreateDbf(_sourceDataSet);
                        break;
                    case 2: 
                        dataFileExport = DataFileExport.CreateCsv(_sourceDataSet);
                        break;
					case 3:
						dataFileExport = DataFileExport.CreateFixedLength(_sourceDataSet);
						break;
                    case 4:
                        dataFileExport = DataFileExport.CreateOfficeXml(_sourceDataSet);
                        break;
                    default:
                        throw new NotImplementedException("Filter index " + saveFileDialog.FilterIndex);
                }

                dataFileExport
                    .AddTextField("Name", 50)
                    .AddNumericField("Balance", 10, 2)
                    .AddDateField("Last activity")
                    .AddBooleanField("Active")
                    .Write(saveFileDialog.FileName);
            }
        }

        private static DataSet GetTestDataSet()
        {
            DataSet sourceDataSet = new DataSet("TestDataSet");
            DataTable table = sourceDataSet.Tables.Add("TestDataTable");
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Balance", typeof(decimal));
            table.Columns.Add("Last activity", typeof(DateTime));
            table.Columns.Add("Active", typeof(bool));

            table.Rows.Add("Anitta Maybourne", 95000.45m, new DateTime(2009, 02, 03, 15, 30, 45, 10), true);
            table.Rows.Add("John Smith", 3450.10m, new DateTime(2009, 05, 04, 21, 12, 35, 10), false);

            return sourceDataSet;
        }
    }
}
