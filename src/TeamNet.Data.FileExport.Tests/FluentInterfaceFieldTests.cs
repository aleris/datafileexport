using System;
using System.Data;
using System.IO;
using System.Text;
using NUnit.Framework;
using TeamNet.Data.FileExport.Csv;
using TeamNet.Data.FileExport.DataSources;
using TeamNet.Data.FileExport.Dbf.Structures;
using TeamNet.Data.FileExport.FixedLength;
using TeamNet.Data.FileExport.Fields;

namespace TeamNet.Data.FileExport.Tests
{
    [TestFixture]
    public class FluentInterfaceFieldTests
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
            dataTable.Rows.Add("b", 2, new DateTime(2001, 11, 12), false);

            _dataSource = new DataSetDataSource(dataSet);
        }

        [Test]
        public void WriteCharacterField()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataFileExport dataFileExport =
                    DataFileExport
                        .CreateDbf(_dataSource)
                        .AddTextField("AString", 10)
                        .AddTextField("OtherName", 10, "AString")
                        .AddTextField("ANumber")
                        .AddTextField("OtherName", "AString")
                        .Write(ms);
                
                byte[] testData = ms.GetBuffer();

                int position = TestHelpers.GetFieldValuePosition(dataFileExport, 0, 0);

                byte expected = Encoding.ASCII.GetBytes((string)_dataSource.GetData(0, "AString"))[0];
                Assert.AreEqual(expected, testData[position]);

                int endPosition = TestHelpers.GetFieldValueEndPosition(dataFileExport, 1, 3);
                Assert.AreEqual(DbfConstants.RowsEndTerminator, testData[endPosition]);
            }
        }

        [Test]
        public void WriteNumericField()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataFileExport dataFileExport =
                    DataFileExport
                        .CreateDbf(_dataSource)
                        .AddNumericField("ANumber", 10, 0)
                        .AddNumericField("OtherName", 10, 0, "ANumber")
                        .AddNumericField("ANumber")
                        .AddNumericField("OtherName", "ANumber")
                        .Write(ms);

                byte[] testData = ms.GetBuffer();

                int position = TestHelpers.GetFieldValuePosition(dataFileExport, 0, 0);

                string stringValue = _dataSource.GetData(0, "ANumber").ToString().PadLeft(10, ' ');
                byte[] expected = Encoding.ASCII.GetBytes(stringValue);
                Assert.IsTrue(TestHelpers.AreEqualByteArrays(expected, testData, position));

                int endPosition = TestHelpers.GetFieldValueEndPosition(dataFileExport, 1, 3);
                Assert.AreEqual(DbfConstants.RowsEndTerminator, testData[endPosition]);
            }
        }

        [Test]
        public void WriteDateField()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataFileExport dataFileExport =
                    DataFileExport
                        .CreateDbf(_dataSource)
                        .AddDateField("ADate")
                        .AddDateField("OtherName", "ADate")
                        .Write(ms);

                byte[] testData = ms.GetBuffer();

                int position = TestHelpers.GetFieldValuePosition(dataFileExport, 0, 0);

                byte[] expected = Encoding.ASCII.GetBytes(((DateTime)_dataSource.GetData(0, "ADate")).ToString("yyyyMMdd"));
                Assert.IsTrue(TestHelpers.AreEqualByteArrays(expected, testData, position));

                int endPosition = TestHelpers.GetFieldValueEndPosition(dataFileExport, 1, 1);
                Assert.AreEqual(DbfConstants.RowsEndTerminator, testData[endPosition]);
            }
        }

        [Test]
        public void WriteLogicalField()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataFileExport dataFileExport =
                    DataFileExport
                        .CreateDbf(_dataSource)
                        .AddBooleanField("ALogical")
                        .AddBooleanField("OtherName", "ALogical")
                        .Write(ms);

                byte[] testData = ms.GetBuffer();

                int position = TestHelpers.GetFieldValuePosition(dataFileExport, 0, 0);

                byte expected = Encoding.ASCII.GetBytes(((bool)_dataSource.GetData(0, "ALogical") ? "T" : "F"))[0];
                Assert.AreEqual(expected, testData[position]);

                int endPosition = TestHelpers.GetFieldValueEndPosition(dataFileExport, 1, 1);
                Assert.AreEqual(DbfConstants.RowsEndTerminator, testData[endPosition]);
            }
        }

        [Test]
        public void WriteCsvNumericField()
        {
            using (MemoryStream ms = new MemoryStream())
            {
				DataFileExport dataFileExport = DataFileExport
					.CreateCsv(_dataSource)
					.AddGenericField("ANumber")
					.AddGenericField("OtherName", "ANumber");
				CsvDataFileExportFormatFactory csvDataFileExportFormatFactory =
					(CsvDataFileExportFormatFactory)dataFileExport.ExportFormatFactory;
				dataFileExport.Write(ms);

                using (StreamReader reader = new StreamReader(new MemoryStream(ms.GetBuffer())))
                {
                    string writtenValue = reader.ReadToEnd();

                    const string expectedHeader = "ANumber";
                    Assert.AreEqual(expectedHeader, writtenValue.Substring(0, expectedHeader.Length));
                    const string expectedValue = "1";
                    int valuePosition = 
						expectedHeader.Length + 
						csvDataFileExportFormatFactory.Options.Separator.Length +
						"OtherName".Length + 
						CsvDataFileWriter.CsvRowsSeparator.Length;
                    Assert.AreEqual(expectedValue, writtenValue.Substring(valuePosition, expectedValue.Length));
                }
            }
        }

		[Test]
		public void WriteFixedLengthNumericField()
		{
			using(MemoryStream ms = new MemoryStream())
			{
				string fieldName1 = "ANumber";
				string fieldName2 = "OtherName";

				DataFileExport dataFileExport = DataFileExport.CreateFixedLength(_dataSource);
				dataFileExport.Fields.Add(new NumericField(fieldName1, (byte)fieldName1.Length, 0));
				dataFileExport.Fields.Add(new NumericField(fieldName2, (byte)fieldName2.Length, 0, "ANumber"));

				FixedLengthDataFileExportFormatFactory fixedLengthDataFileExportFormatFactory =
					(FixedLengthDataFileExportFormatFactory)dataFileExport.ExportFormatFactory;
				dataFileExport.Write(ms);

				using(StreamReader reader = new StreamReader(new MemoryStream(ms.GetBuffer())))
				{
					string writtenValue = reader.ReadToEnd();

					const string expectedHeader = "ANumber";
					Assert.AreEqual(expectedHeader, writtenValue.Substring(0, expectedHeader.Length));
					const string expectedValue = "1";
					int valuePosition =
						fieldName1.Length
						+ fixedLengthDataFileExportFormatFactory.Options.Separator.Length
						+ fieldName2.Length
						+ CsvDataFileWriter.CsvRowsSeparator.Length
						+ fieldName1.Length - 1;	//numeric fields are aligned to the right
					Assert.AreEqual(expectedValue, writtenValue.Substring(valuePosition, expectedValue.Length));
				}
			}
		}
    }
}