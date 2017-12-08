using System;
using System.Data;
using System.IO;
using NUnit.Framework;
using TeamNet.Data.FileExport.FixedLength;
using TeamNet.Data.FileExport.DataSources;
using TeamNet.Data.FileExport.Fields;

namespace TeamNet.Data.FileExport.Tests.FixedLength
{
    [TestFixture]
    public class FixedLengthFieldWriterTests
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
			string header = "AString";
			string expected = "a".PadRight(header.Length);

            string writtenValue = WriteAndGetStringText(new TextField(header, (byte)header.Length));
			AssertHeaderAndValueAreCorrectlyWritten(writtenValue, "AString", expected);
        }

        [Test]
        public void WriteCharacterFieldWithSourceName()
        {
			string header = "OtherName";
			string expected = "a".PadRight(header.Length);

            string writtenValue = WriteAndGetStringText(new TextField(header, (byte)header.Length, "AString"));
            AssertHeaderAndValueAreCorrectlyWritten(writtenValue, header, expected);
        }

        // Numeric

        [Test]
        public void WriteNumericFieldImplicitSourceName()
        {
			string header = "ANumber";
			string expected = 123.456m.ToString().PadLeft(header.Length);

			string writtenValue = WriteAndGetStringText(new NumericField(header, (byte)header.Length, 3));
            AssertHeaderAndValueAreCorrectlyWritten(writtenValue, header, expected);
        }

        [Test]
        public void WriteNumericFieldWithSourceName()
        {
			string header = "OtherName";
			string expected = 123.456m.ToString().PadLeft(header.Length);

			string writtenValue = WriteAndGetStringText(new NumericField(header, (byte)header.Length, 3, "ANumber"));
			AssertHeaderAndValueAreCorrectlyWritten(writtenValue, header, expected);
        }

        // Date

        [Test]
        public void WriteDateFieldImplicitSourceName()
        {
			string header = "ADate";
			string expectedHeader = header.PadRight(8);
			string expectedValue = new DateTime(2000, 10, 11).ToString(FixedLengthDateFieldWriter.DataFormatPattern);
            
			string writtenValue = WriteAndGetStringText(new DateField(header));
            AssertHeaderAndValueAreCorrectlyWritten(writtenValue, expectedHeader, expectedValue);
        }

        [Test]
        public void WriteDateFieldWithSourceName()
        {
			string header = "OtherName";
			string expectedHeader = header.Substring(0, 8);
			string expectedValue = new DateTime(2000, 10, 11).ToString(FixedLengthDateFieldWriter.DataFormatPattern);
            
            string writtenValue = WriteAndGetStringText(new DateField(header, "ADate"));
			AssertHeaderAndValueAreCorrectlyWritten(writtenValue, expectedHeader, expectedValue);
        }

        // Logical

        [Test]
        public void WriteLogicalFieldImplicitSourceName()
        {
			string header = "ALogical";
			string expectedHeader = header.Substring(0, 1);
			string expectedValue = true.ToString().Substring(0, 1);
            
            string writtenValue = WriteAndGetStringText(new BooleanField(header));
            AssertHeaderAndValueAreCorrectlyWritten(writtenValue, expectedHeader, expectedValue);
        }

        [Test]
        public void WriteLogicalFieldWithSourceName()
        {
			string header = "OtherName";
			string expectedHeader = header.Substring(0, 1);
			string expectedValue = true.ToString().Substring(0, 1);

			string writtenValue = WriteAndGetStringText(new BooleanField(header, "ALogical"));
			AssertHeaderAndValueAreCorrectlyWritten(writtenValue, expectedHeader, expectedValue);
        }

        // Generic

        [Test]
        public void WriteFieldImplicitSourceName()
        {
			string header = "ANumber";
			IField field = new GenericField(header);
			string expectedHeader = header.PadRight(field.TotalSize);
			string expectedValue = 123.456m.ToString().PadRight(field.TotalSize);

            string writtenValue = WriteAndGetStringText(field);
            AssertHeaderAndValueAreCorrectlyWritten(writtenValue, expectedHeader, expectedValue);
        }

		[Test]
		public void WriteFieldWithSourceName()
		{
			string header = "OtherName";
			IField field = new GenericField(header, "ANumber");
			string expectedHeader = header.PadRight(field.TotalSize);
			string expectedValue = 123.456m.ToString().PadRight(field.TotalSize);

			string writtenValue = WriteAndGetStringText(field);
			AssertHeaderAndValueAreCorrectlyWritten(writtenValue, expectedHeader, expectedValue);
		}

        string WriteAndGetStringText(IField field)
        {
            DataFileExport dataFileExport = new DataFileExport(_dataSource, new FixedLengthDataFileExportFormatFactory());
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
            int valuePosition = expectedHeader.Length + FixedLengthDataFileWriter.FixedLengthRowsSeparator.Length;
            Assert.AreEqual(expectedValue, writtenValue.Substring(valuePosition, expectedValue.Length));
        }
    }
}