using System.Data;
using System.IO;
using NUnit.Framework;
using TeamNet.Data.FileExport.Csv;
using TeamNet.Data.FileExport.DataSources;
using TeamNet.Data.FileExport.Fields;

namespace TeamNet.Data.FileExport.Tests.Csv
{
    [TestFixture]
    public class CsvWriterTests
    {
        private string _writtenValue;

        [SetUp]
        public void SetUp()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = dataSet.Tables.Add();
            dataTable.Columns.Add("a", typeof(string));
            dataTable.Columns.Add("b", typeof(string));

            dataTable.Rows.Add("v1", "v2");

            IDataSource dataSource = new DataSetDataSource(dataSet);

            DataFileExport dataFileExport = new DataFileExport(dataSource, new CsvDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new TextField("a"));
            dataFileExport.Fields.Add(new TextField("b"));

            using (MemoryStream ms = new MemoryStream())
            {
                dataFileExport.Write(ms);
                using (StreamReader reader = new StreamReader(new MemoryStream(ms.GetBuffer())))
                {
                    _writtenValue = reader.ReadToEnd();
                }
            }
        }

		[Test]
		public void HeaderCsvSeparatorIsCorrectlyWritten()
		{
			Assert.AreEqual(CsvDataFileExportFormatFactory.DefaultCsvDataFileExportOptions.Separator, _writtenValue.Substring("a".Length, CsvDataFileExportFormatFactory.DefaultCsvDataFileExportOptions.Separator.Length));
		}

		[Test]
		public void ValuesCsvSeparatorIsCorrectlyWritten()
		{
			int position = "a".Length + CsvDataFileExportFormatFactory.DefaultCsvDataFileExportOptions.Separator.Length + "b".Length +
						   CsvDataFileWriter.CsvRowsSeparator.Length + "v1".Length;
			Assert.AreEqual(CsvDataFileExportFormatFactory.DefaultCsvDataFileExportOptions.Separator, _writtenValue.Substring(position, CsvDataFileExportFormatFactory.DefaultCsvDataFileExportOptions.Separator.Length));
		}

		[Test]
		public void RowsSeparatorIsCorrectlyWritten()
		{
			int position = "a".Length + CsvDataFileExportFormatFactory.DefaultCsvDataFileExportOptions.Separator.Length + "b".Length;
			Assert.AreEqual(CsvDataFileWriter.CsvRowsSeparator, _writtenValue.Substring(position, CsvDataFileWriter.CsvRowsSeparator.Length));
		}
    }
}