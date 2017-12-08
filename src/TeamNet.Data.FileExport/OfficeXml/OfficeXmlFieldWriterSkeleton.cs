// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using System;
using System.Xml;

namespace TeamNet.Data.FileExport.OfficeXml
{
    abstract class OfficeXmlFieldWriterSkeleton : IFieldWriter
    {
        public IField Field { get; private set; }

        public OfficeXmlDataFileWriter OfficeXmlDataFileWriter { get; private set; }

        public abstract string FieldType { get; }
        public abstract string StyleID { get; }

        public OfficeXmlFieldWriterSkeleton(OfficeXmlDataFileWriter officeXmlDataFileWriter, IField field)
        {
            this.OfficeXmlDataFileWriter = officeXmlDataFileWriter;
            this.Field = field;
        }

        public abstract string FormatValue(object value);

        public void WriteValue(object value)
        {
            XmlTextWriter writer = this.OfficeXmlDataFileWriter.XmlTextWriter;
            writer.WriteStartElement("Cell", OfficeXmlDataFileWriter.OfficeXmlSpreadSheetSchema);
            writer.WriteAttributeString("StyleID", OfficeXmlDataFileWriter.OfficeXmlSpreadSheetSchema, this.StyleID);
            writer.WriteStartElement("Data", OfficeXmlDataFileWriter.OfficeXmlSpreadSheetSchema);
            writer.WriteAttributeString("Type", OfficeXmlDataFileWriter.OfficeXmlSpreadSheetSchema, this.FieldType);
            writer.WriteValue(FormatValue(value));
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        public void WriteHeader(int offsetInRecord)
        {
            XmlTextWriter writer = this.OfficeXmlDataFileWriter.XmlTextWriter;
            writer.WriteStartElement("Cell", OfficeXmlDataFileWriter.OfficeXmlSpreadSheetSchema);
            writer.WriteStartElement("Data", OfficeXmlDataFileWriter.OfficeXmlSpreadSheetSchema);
            writer.WriteAttributeString("Type", OfficeXmlDataFileWriter.OfficeXmlSpreadSheetSchema, "String");
            writer.WriteValue(this.Field.Name);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
    }
}