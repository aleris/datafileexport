using System;
using System.Xml;

namespace TeamNet.Data.FileExport.OfficeXml
{
    class OfficeXmlNumberFieldWriter : OfficeXmlFieldWriterSkeleton
    {
        public OfficeXmlNumberFieldWriter(OfficeXmlDataFileWriter officeXmlDataFileWriter, IField field) 
            : base(officeXmlDataFileWriter, field)
        {}

        public override string FieldType
        {
            get { return "Number"; }
        }

        public override string StyleID
        {
            get { return "Number"; }
        }

        public override string FormatValue(object value)
        {
            return Convert.ToString(value);
        }
    }
}
