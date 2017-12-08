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
    public class DbfFieldWriterNullTests
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

            dataTable.Rows.Add(null, null, null, null);
            dataTable.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value);

            _dataSource = new DataSetDataSource(dataSet);
        }


        // Character

        [Test]
        public void WriteCharacterFieldImplicitSourceName()
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new TextField("AString", 1));

            AssertCorrectNullValue(dataFileExport);
        }

        [Test]
        public void WriteCharacterFieldWithSourceName()
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new TextField("OtherName", 1, "AString"));

            AssertCorrectNullValue(dataFileExport);
        }

        // Numeric

        [Test]
        public void WriteNumericFieldImplicitSourceName()
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new NumericField("ANumber", 1, 0));

            AssertCorrectNullValue(dataFileExport);
        }

        [Test]
        public void WriteNumericFieldWithSourceName()
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new NumericField("OtherName", 1, 0, "ANumber"));

            AssertCorrectNullValue(dataFileExport);
        }

        // Date

        [Test]
        public void WriteDateFieldImplicitSourceName()
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new DateField("ADate"));

            AssertCorrectNullValue(dataFileExport);
        }

        [Test]
        public void WriteDateFieldWithSourceName()
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new DateField("OtherName", "ADate"));

            AssertCorrectNullValue(dataFileExport);
        }

        // Logical

        [Test]
        public void WriteLogicalFieldImplicitSourceName()
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new BooleanField("ALogical"));

            AssertCorrectNullValue(dataFileExport);
        }

        [Test]
        public void WriteLogicalFieldWithSourceName()
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new BooleanField("OtherName", "ALogical"));

            AssertCorrectNullValue(dataFileExport);
        }

        static void AssertCorrectNullValue(DataFileExport dataFileExport)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                dataFileExport.Write(ms);

                byte[] testData = ms.GetBuffer();

                byte[] expectedBuffer = Encoding.ASCII.GetBytes(new string(' ', dataFileExport.Fields[0].TotalSize));
                
                int firstRowPosition = TestHelpers.GetFieldValuePosition(dataFileExport, 0, 0);
                TestHelpers.AreEqualByteArrays(expectedBuffer, testData, firstRowPosition);

                int secondRowPosition = TestHelpers.GetFieldValuePosition(dataFileExport, 1, 0);
                TestHelpers.AreEqualByteArrays(expectedBuffer, testData, secondRowPosition);

                int endPosition = TestHelpers.GetFieldValueEndPosition(dataFileExport, 1, 0);
                Assert.AreEqual(DbfConstants.RowsEndTerminator, testData[endPosition]);
            }
        }
    }
}