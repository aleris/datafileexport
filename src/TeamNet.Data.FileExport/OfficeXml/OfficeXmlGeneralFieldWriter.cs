using System;

namespace TeamNet.Data.FileExport.OfficeXml
{
    class OfficeXmlGeneralFieldWriter : OfficeXmlFieldWriterSkeleton
    {
        public OfficeXmlGeneralFieldWriter(OfficeXmlDataFileWriter officeXmlDataFileWriter, IField field) 
            : base(officeXmlDataFileWriter, field)
        {}

        public override string FieldType
        {
            get { return "String"; }
        }

        public override string StyleID
        {
            get { return "General"; }
        }

        public override string FormatValue(object value)
        {
            return Convert.ToString(value);
        }
    }
}
