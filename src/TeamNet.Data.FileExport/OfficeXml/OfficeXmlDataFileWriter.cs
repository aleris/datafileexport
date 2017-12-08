using System;
using System.IO;
using System.Xml;
using TeamNet.Data.FileExport.Csv;

namespace TeamNet.Data.FileExport.OfficeXml
{
	/// <summary>
	/// Writes XML streams for Office.
	/// </summary>
    public class OfficeXmlDataFileWriter : IDataFileWriter, IDisposable
    {
        /// <summary>
        /// Gets the output stream.
        /// </summary>
        /// <value>The output stream.</value>
        public Stream Stream { get; private set; }

        /// <summary>
        /// Gets the xml text writer used to write to the output stream.
        /// </summary>
        /// <value>The xml text writer.</value>
        public XmlTextWriter XmlTextWriter { get; private set; }

        internal const string OfficeXmlSpreadSheetSchema = "urn:schemas-microsoft-com:office:spreadsheet";

        /// <summary>
        /// Initializes a new instance of the <see cref="OfficeXmlDataFileWriter"/> class.
        /// </summary>
        /// <param name="stream">The output stream to write to.</param>
        public OfficeXmlDataFileWriter(Stream stream)
        {
            this.Stream = stream;
            this.XmlTextWriter = new XmlTextWriter(new StreamWriter(stream));
            this.XmlTextWriter.Formatting = Formatting.Indented;
        }

        /// <summary>
        /// Writes the data file to the output stream.
        /// </summary>
        /// <param name="dataFileExport">The data file export definition.</param>
        public void Write(DataFileExport dataFileExport)
        {
            WriteStartDocument();
            WriteStartWorksheet("Sheet 1");

            WriteStartRow();
            for (int fieldIndex = 0; fieldIndex < dataFileExport.Fields.Count; fieldIndex++)
            {
                IField field = dataFileExport.Fields[fieldIndex];
                field.GetFieldWriter(dataFileExport.ExportFormatFactory).WriteHeader(fieldIndex);
            }
            WriteEndRow();

            int rowCount = dataFileExport.DataSource.RowCount;
            for (int rowIndex = 0; rowIndex != rowCount; rowIndex++)
            {
                WriteStartRow();
                for (int fieldIndex = 0; fieldIndex != dataFileExport.Fields.Count; fieldIndex++)
                {
                    IField field = dataFileExport.Fields[fieldIndex];
                    IFieldWriter fieldWriter = field.GetFieldWriter(dataFileExport.ExportFormatFactory);

                    object value = dataFileExport.DataSource.GetData(rowIndex, field.SourceName);
                    fieldWriter.WriteValue(value);
                }
                WriteEndRow();
            }
            WriteEndWorksheet();
            
            WriteEndDocument();

            this.XmlTextWriter.Flush();
        }

        void WriteStartDocument()
        {
            this.XmlTextWriter.WriteProcessingInstruction("xml", "version=\"1.0\"");
            this.XmlTextWriter.WriteProcessingInstruction("mso-application", "progid=\"Excel.Sheet\"");
            this.XmlTextWriter.WriteStartElement("ss", "Workbook", OfficeXmlSpreadSheetSchema);
            WriteExcelStyles();
        }

        void WriteEndDocument()
        {
            this.XmlTextWriter.WriteEndElement();
        }

        void WriteExcelStyles()
        {
            this.XmlTextWriter.WriteStartElement("Styles", OfficeXmlSpreadSheetSchema);

            WriteExcelStyleElement("General", null);
            WriteExcelStyleElement("Number", "Fixed");
            WriteExcelStyleElement("Date", "General Date");
            WriteExcelStyleElement("Text", "@");

            this.XmlTextWriter.WriteEndElement();
        }

        void WriteExcelStyleElement(string styleName, string numberFormat)
        {
            this.XmlTextWriter.WriteStartElement("Style", OfficeXmlSpreadSheetSchema);
            this.XmlTextWriter.WriteAttributeString("ID", OfficeXmlSpreadSheetSchema, styleName);

            if (!String.IsNullOrEmpty(numberFormat))
            {
                this.XmlTextWriter.WriteStartElement("NumberFormat", OfficeXmlSpreadSheetSchema);
                this.XmlTextWriter.WriteAttributeString("ExportFormatFactory", OfficeXmlSpreadSheetSchema, numberFormat);
                this.XmlTextWriter.WriteEndElement();
            }

            this.XmlTextWriter.WriteEndElement();
        }

        void WriteStartWorksheet(string name)
        {
            this.XmlTextWriter.WriteStartElement("Worksheet", OfficeXmlSpreadSheetSchema);
            this.XmlTextWriter.WriteAttributeString("Name", OfficeXmlSpreadSheetSchema, name);
            this.XmlTextWriter.WriteStartElement("Table", OfficeXmlSpreadSheetSchema);
        }

        void WriteEndWorksheet()
        {
            this.XmlTextWriter.WriteEndElement();
            this.XmlTextWriter.WriteEndElement();
        }

        void WriteStartRow()
        {
            this.XmlTextWriter.WriteStartElement("Row", OfficeXmlSpreadSheetSchema);
        }

        void WriteEndRow()
        {
            this.XmlTextWriter.WriteEndElement();
        }

        void IDisposable.Dispose()
        {
            this.XmlTextWriter.BaseStream.Dispose();
        }
    }
}
