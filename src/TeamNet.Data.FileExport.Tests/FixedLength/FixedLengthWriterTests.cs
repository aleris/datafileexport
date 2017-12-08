using System.Data;
using System.IO;
using NUnit.Framework;
using TeamNet.Data.FileExport.FixedLength;
using TeamNet.Data.FileExport.DataSources;
using TeamNet.Data.FileExport.Fields;

namespace TeamNet.Data.FileExport.Tests.FixedLength
{
    [TestFixture]
    public class FixedLengthWriterTests
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

            DataFileExport dataFileExport = new DataFileExport(dataSource, new FixedLengthDataFileExportFormatFactory());
			dataFileExport.Fields.Add(new TextField("a", 1));
            dataFileExport.Fields.Add(new TextField("b", 1));

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
		public void HeaderFixedLengthSeparatorIsCorrectlyWritten()
		{
			Assert.AreEqual(FixedLengthDataFileExportFormatFactory.DefaultFixedLengthDataFileExportOptions.Separator, _writtenValue.Substring("a".Length, FixedLengthDataFileExportFormatFactory.DefaultFixedLengthDataFileExportOptions.Separator.Length));
		}

		[Test]
		public void ValuesFixedLengthSeparatorIsCorrectlyWritten()
		{
			int position = "a".Length 
				+ FixedLengthDataFileExportFormatFactory.DefaultFixedLengthDataFileExportOptions.Separator.Length 
				+ "b".Length 
				+ FixedLengthDataFileWriter.FixedLengthRowsSeparator.Length 
				+ "v1".Length;
			Assert.AreEqual(FixedLengthDataFileExportFormatFactory.DefaultFixedLengthDataFileExportOptions.Separator, _writtenValue.Substring(position, FixedLengthDataFileExportFormatFactory.DefaultFixedLengthDataFileExportOptions.Separator.Length));
		}

		[Test]
		public void RowsSeparatorIsCorrectlyWritten()
		{
			int position = "a".Length 
				+ FixedLengthDataFileExportFormatFactory.DefaultFixedLengthDataFileExportOptions.Separator.Length 
				+ "b".Length;
			Assert.AreEqual(FixedLengthDataFileWriter.FixedLengthRowsSeparator, _writtenValue.Substring(position, FixedLengthDataFileWriter.FixedLengthRowsSeparator.Length));
		}
    }
}