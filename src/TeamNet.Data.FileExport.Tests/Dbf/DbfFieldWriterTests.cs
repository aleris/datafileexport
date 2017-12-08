using System;
using System.Data;
using System.IO;
using System.Text;
using NUnit.Framework;
using TeamNet.Data.FileExport.DataSources;
using TeamNet.Data.FileExport.Dbf;
using TeamNet.Data.FileExport.Dbf.Structures;
using TeamNet.Data.FileExport.Fields;

namespace TeamNet.Data.FileExport.Tests.Dbf
{
    [TestFixture]
    public class DbfFieldWriterTests
    {
        private IDataSource _dataSource;

        [SetUp]
        public void SetUp()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = dataSet.Tables.Add();
            dataTable.Columns.Add("AString", typeof(string));
            dataTable.Columns.Add("ANumber", typeof(decimal));
            dataTable.Columns.Add("ADate", typeof(DateTime));
            dataTable.Columns.Add("ALogical", typeof(bool));

            dataTable.Rows.Add("a", 1, new DateTime(2000, 10, 11), true);

            _dataSource = new DataSetDataSource(dataSet);
        }

        // Character

        [Test]
        public void WriteCharacterFieldImplicitSourceName()
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new TextField("AString", 1));

            AssertCorrectValue(dataFileExport, (string)_dataSource.GetData(0, "AString"));
        }

        [Test]
        public void WriteCharacterFieldWithSourceName()
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new TextField("OtherName", 1, "AString"));

            AssertCorrectValue(dataFileExport, (string)_dataSource.GetData(0, "AString"));
        }

        // Numeric

        [Test]
        public void WriteNumericFieldImplicitSourceName()
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new NumericField("ANumber", 1, 0));

            AssertCorrectValue(dataFileExport, ((decimal)_dataSource.GetData(0, "ANumber")).ToString());
        }

        [Test]
        public void WriteNumericFieldWithSourceName()
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new NumericField("OtherName", 1, 0, "ANumber"));

            AssertCorrectValue(dataFileExport, ((decimal)_dataSource.GetData(0, "ANumber")).ToString());
        }

        // Date

        [Test]
        public void WriteDateFieldImplicitSourceName()
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new DateField("ADate"));

            AssertCorrectValue(dataFileExport, ((DateTime)_dataSource.GetData(0, "ADate")).ToString("yyyyMMdd"));
        }

        [Test]
        public void WriteDateFieldWithSourceName()
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new DateField("OtherName", "ADate"));

            AssertCorrectValue(dataFileExport, ((DateTime)_dataSource.GetData(0, "ADate")).ToString("yyyyMMdd"));
        }

        // Logical

        [Test]
        public void WriteLogicalFieldImplicitSourceName()
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new BooleanField("ALogical"));

            AssertCorrectValue(dataFileExport, ((bool)_dataSource.GetData(0, "ALogical") ? "T" : "F"));
        }

        [Test]
        public void WriteLogicalFieldWithSourceName()
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new BooleanField("OtherName", "ALogical"));

            AssertCorrectValue(dataFileExport, ((bool)_dataSource.GetData(0, "ALogical") ? "T" : "F"));
        }

        static void AssertCorrectValue(DataFileExport dataFileExport, string expectedValue)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                dataFileExport.Write(ms);

                byte[] testData = ms.GetBuffer();

                byte[] expectedBuffer = Encoding.ASCII.GetBytes(expectedValue);

                int position = TestHelpers.GetFieldValuePosition(dataFileExport, 0, 0);
                TestHelpers.AreEqualByteArrays(expectedBuffer, testData, position);

                int endPosition = TestHelpers.GetFieldValueEndPosition(dataFileExport, 0, 0);
                Assert.AreEqual(DbfConstants.RowsEndTerminator, testData[endPosition]);
            }
        }
    }
}