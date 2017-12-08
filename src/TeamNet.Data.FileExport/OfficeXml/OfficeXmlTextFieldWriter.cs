using System;
using System.Xml;

namespace TeamNet.Data.FileExport.OfficeXml
{
    class OfficeXmlTextFieldWriter : OfficeXmlFieldWriterSkeleton
    {
        public OfficeXmlTextFieldWriter(OfficeXmlDataFileWriter officeXmlDataFileWriter, IField field) 
            : base(officeXmlDataFileWriter, field)
        {}

        public override string FieldType
        {
            get { return "String"; }
        }

        public override string StyleID
        {
            get { return "Text"; }
        }

        public override string FormatValue(object value)
        {
            return Convert.ToString(value);
        }
    }
}
