using System;
using System.Data;
using System.IO;
using NUnit.Framework;
using TeamNet.Data.FileExport.Csv;
using TeamNet.Data.FileExport.DataSources;
using TeamNet.Data.FileExport.Dbf;
using TeamNet.Data.FileExport.Fields;

namespace TeamNet.Data.FileExport.Tests.Csv
{
    [TestFixture]
    public class CsvEscapeValueTests
    {
        const string HeaderName = "AField";

        [Test]
        public void SimpleValueShouldBeUnescaped()
        {
            AssertCorrectCsvValue("a", "a");
        }

        [Test]
        public void EmptyValueShouldBeEmpty()
        {
            AssertCorrectCsvValue("", "");
        }

        [Test]
        public void ValueWithSeparatorShouldBeEscaped()
        {
            AssertCorrectCsvValue("\"" + CsvDataFileExportFormatFactory.DefaultCsvDataFileExportOptions.Separator + "\"", ",");
        }

        [Test]
        public void ValueWithRowSeparatorShouldBeEscaped()
        {
            AssertCorrectCsvValue("\"" + CsvDataFileWriter.CsvRowsSeparator + "\"", CsvDataFileWriter.CsvRowsSeparator);
        }

        [Test]
        public void ValueWithQuoteShouldBeEscaped()
        {
            AssertCorrectCsvValue("\"\"\"\"", "\"");
        }

        private static void AssertCorrectCsvValue(string expected, string test)
        {
            string val = GetOutputForSingleStringValue(test);
            Assert.AreEqual(expected, val.Substring(HeaderName.Length + CsvDataFileWriter.CsvRowsSeparator.Length, expected.Length));            
        }

        private static string GetOutputForSingleStringValue(string value)
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = dataSet.Tables.Add();
            dataTable.Columns.Add(HeaderName, typeof(string));

            dataTable.Rows.Add(value);

            IDataSource dataSource = new DataSetDataSource(dataSet);

            DataFileExport dataFileExport = new DataFileExport(dataSource, new CsvDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new GenericField("AField"));

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
    }
}
