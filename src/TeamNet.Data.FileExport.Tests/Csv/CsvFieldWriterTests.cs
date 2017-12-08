using System;
using System.Data;
using System.IO;
using NUnit.Framework;
using TeamNet.Data.FileExport.Csv;
using TeamNet.Data.FileExport.DataSources;
using TeamNet.Data.FileExport.Fields;

namespace TeamNet.Data.FileExport.Tests.Csv
{
    [TestFixture]
    public class CsvFieldWriterTests
    {
        private IDataSource _dataSource;

        [SetUp]
        public void SetUp()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = dataSet.Tables.Add();
            dataTable.Columns.Add("AString", typeof (string));
            dataTable.Columns.Add("ANumber", typeof(decimal));
            dataTable.Columns.Add("ADate", typeof(DateTime));
            dataTable.Columns.Add("ALogical", typeof(bool));

            dataTable.Rows.Add("a", 123.456m, new DateTime(2000, 10, 11), true);

            _dataSource = new DataSetDataSource(dataSet);
        }

        // Character
        
        [Test]
        public void WriteCharacterFieldImplicitSourceName()
        {
            string writtenValue = WriteAndGetStringText(new TextField("AString"));
            AssertHeaderAndValueAreCorrectlyWritten(writtenValue, "AString", "a");
        }

        [Test]
        public void WriteCharacterFieldWithSourceName()
        {
            string writtenValue = WriteAndGetStringText(new TextField("OtherName", "AString"));
            AssertHeaderAndValueAreCorrectlyWritten(writtenValue, "OtherName", "a");
        }

        // Numeric

        [Test]
        public void WriteNumericFieldImplicitSourceName()
        {
            string writtenValue = WriteAndGetStringText(new NumericField("ANumber"));
            AssertHeaderAndValueAreCorrectlyWritten(writtenValue, "ANumber", 123.456m.ToString());
        }

        [Test]
        public void WriteNumericFieldWithSourceName()
        {
            string writtenValue = WriteAndGetStringText(new NumericField("OtherName", "ANumber"));
            AssertHeaderAndValueAreCorrectlyWritten(writtenValue, "OtherName", 123.456m.ToString());
        }

        // Date

        [Test]
        public void WriteDateFieldImplicitSourceName()
        {
			string expectedValue = new DateTime(2000, 10, 11).ToString(CsvDateFieldWriter.DataFormatPattern);
            
            string writtenValue = WriteAndGetStringText(new DateField("ADate"));
            AssertHeaderAndValueAreCorrectlyWritten(writtenValue, "ADate", expectedValue);
        }

        [Test]
        public void WriteDateFieldWithSourceName()
        {
			string expectedValue = new DateTime(2000, 10, 11).ToString(CsvDateFieldWriter.DataFormatPattern);
            
            string writtenValue = WriteAndGetStringText(new DateField("OtherName", "ADate"));
			AssertHeaderAndValueAreCorrectlyWritten(writtenValue, "OtherName", expectedValue);
        }

        // Logical

        [Test]
        public void WriteLogicalFieldImplicitSourceName()
        {
			string expectedValue = true.ToString().Substring(0, 1);

            string writtenValue = WriteAndGetStringText(new BooleanField("ALogical"));
			AssertHeaderAndValueAreCorrectlyWritten(writtenValue, "ALogical", expectedValue);
        }

        [Test]
        public void WriteLogicalFieldWithSourceName()
        {
			string expectedValue = true.ToString().Substring(0, 1);

            string writtenValue = WriteAndGetStringText(new BooleanField("OtherName", "ALogical"));
			AssertHeaderAndValueAreCorrectlyWritten(writtenValue, "OtherName", expectedValue);
        }

        // Generic

        [Test]
        public void WriteFieldImplicitSourceName()
        {
            string writtenValue = WriteAndGetStringText(new GenericField("ANumber"));
            AssertHeaderAndValueAreCorrectlyWritten(writtenValue, "ANumber", 123.456m.ToString());
        }

        [Test]
        public void WriteFieldWithSourceName()
        {
            string writtenValue = WriteAndGetStringText(new GenericField("OtherName", "ANumber"));
            AssertHeaderAndValueAreCorrectlyWritten(writtenValue, "OtherName", 123.456m.ToString());
        }

        string WriteAndGetStringText(IField field)
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new CsvDataFileExportFormatFactory());
            dataFileExport.Fields.Add(field);

            using (MemoryStream ms = new MemoryStream())
            {
                dataFileExport.Write(ms);

                using (StreamReader reader = new StreamReader(new MemoryStream(ms.GetBuffer())))
                {
                    string writtenValue = reader.ReadToEnd();
                    return writtenValue;
                }
            }
        }

        static void AssertHeaderAndValueAreCorrectlyWritten(string writtenValue, string expectedHeader, string expectedValue)
        {
            Assert.AreEqual(expectedHeader, writtenValue.Substring(0, expectedHeader.Length));
            int valuePosition = expectedHeader.Length + CsvDataFileWriter.CsvRowsSeparator.Length;
            Assert.AreEqual(expectedValue, writtenValue.Substring(valuePosition, expectedValue.Length));
        }
    }
}