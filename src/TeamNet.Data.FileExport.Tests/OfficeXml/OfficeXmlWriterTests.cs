using System;
using System.Data;
using System.IO;
using System.Xml;
using NUnit.Framework;
using TeamNet.Data.FileExport.DataSources;
using TeamNet.Data.FileExport.Fields;
using TeamNet.Data.FileExport.OfficeXml;

namespace TeamNet.Data.FileExport.Tests.OfficeXml
{
    [TestFixture]
    public class OfficeXmlWriterTests
    {
        [Test]
        public void OutputStreamIsValidXml()
        {
            DataSet dataSet = new DataSet();
            DataTable table = dataSet.Tables.Add();
            table.Columns.Add("ANumber", typeof(int));
            table.Columns.Add("AString", typeof(string));
            table.Columns.Add("ADate", typeof(DateTime));
            table.Columns.Add("ALogical", typeof(bool));
            table.Rows.Add(1234567890, "aaaaaaaaaa", new DateTime(2000, 10, 5), true);
            table.Rows.Add(1357924680, "bbbb,bbbbb", new DateTime(2001, 11, 6), false);

            IDataSource dataSource = new DataSetDataSource(dataSet);

            DataFileExport dataFileExport = new DataFileExport(dataSource, new OfficeXmlDataFileExportFormatFactory());
            dataFileExport.Fields.Add(new TextField("AString"));
            dataFileExport.Fields.Add(new NumericField("ANumber"));
            dataFileExport.Fields.Add(new DateField("ADate"));
            dataFileExport.Fields.Add(new BooleanField("ALogical"));

            using (MemoryStream ms = new MemoryStream())
            {
                dataFileExport.Write(ms);
                using (StreamReader reader = new StreamReader(new MemoryStream(ms.GetBuffer())))
                {
                    string val = reader.ReadToEnd();
                    XmlDocument xdoc = new XmlDocument();

                    Assert.DoesNotThrow(() => xdoc.LoadXml(val));
                }
            }
        }
    }
}
