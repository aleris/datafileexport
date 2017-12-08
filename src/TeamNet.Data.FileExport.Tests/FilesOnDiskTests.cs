using System;
using System.Data;
using System.IO;
using NUnit.Framework;

namespace TeamNet.Data.FileExport.Tests
{
    [TestFixture]
    public class FilesOnDiskTests
    {
        private string _outputFolder;

        private DataSet _sourceDataSet;

        [SetUp]
        public void SetUp()
        {
            _outputFolder = Path.GetFullPath("Output");

            if (!Directory.Exists(_outputFolder))
            {
                Directory.CreateDirectory(_outputFolder);
            }

            _sourceDataSet = new DataSet();
            DataTable table = _sourceDataSet.Tables.Add();
            table.Columns.Add("ANumber", typeof(int));
            table.Columns.Add("AString", typeof(string));
            table.Columns.Add("ADate", typeof(DateTime));
            table.Columns.Add("ALogical", typeof(bool));
            table.Rows.Add(1234567890, "aaaaaaaaaa", new DateTime(2000, 10, 5), true);
            table.Rows.Add(1357924680, "bbbb,bbbbb", new DateTime(2001, 11, 6), false);
        }

        [Test]
        public void CanWriteDbfStreamToDisk()
        {

            string filePath = Path.Combine(_outputFolder, "CanWriteDbfStreamToDisk.dbf");
            DataFileExport
                .CreateDbf(_sourceDataSet)
                .AddTextField("AString", 10)
                .AddNumericField("ANumber", 10, 0)
                .AddDateField("ADate")
                .AddBooleanField("ALogical")
                .Write(filePath);

            Assert.IsTrue(File.Exists(filePath));
        }

        [Test]
        public void CanWriteCsvStreamToDisk()
        {
            string filePath = Path.Combine(_outputFolder, "CanWriteCsvStreamToDisk.csv");
            DataFileExport
                .CreateCsv(_sourceDataSet)
                .AddGenericField("AString")
                .AddGenericField("ANumber")
                .AddGenericField("ADate")
                .AddGenericField("ALogical")
                .Write(filePath);

            Assert.IsTrue(File.Exists(filePath));
        }

        [Test]
        public void CanWriteDbfThenCsvStreamToDisk()
        {

            string filePathDbf = Path.Combine(_outputFolder, "CanWriteDbfThenCsvStreamToDisk.dbf");
            string filePathCsv = Path.Combine(_outputFolder, "CanWriteDbfThenCsvStreamToDisk.csv");
            
            DataFileExport
                .CreateDbf(_sourceDataSet)
                .AddTextField("AString", 10)
                .AddNumericField("ANumber", 10, 0)
                .AddDateField("ADate")
                .AddBooleanField("ALogical")
                .Write(filePathDbf)
                .AsCsv()
                .Write(filePathCsv);

            Assert.IsTrue(File.Exists(filePathDbf));
            Assert.IsTrue(File.Exists(filePathCsv));
        }

        [Test]
        public void CanWriteOfficeXmlStreamToDisk()
        {
            string filePath = Path.Combine(_outputFolder, "CanWriteOfficeXmlStreamToDisk.xls");
            DataFileExport
                .CreateOfficeXml(_sourceDataSet)
                .AddTextField("AString")
                .AddNumericField("ANumber")
                .AddDateField("ADate")
                .AddGenericField("ALogical")
                .Write(filePath);

            Assert.IsTrue(File.Exists(filePath));
        }
    }
}
