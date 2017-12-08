using System;
using System.Xml;

namespace TeamNet.Data.FileExport.OfficeXml
{
    class OfficeXmlDateTimeFieldWriter : OfficeXmlFieldWriterSkeleton
    {
        public OfficeXmlDateTimeFieldWriter(OfficeXmlDataFileWriter officeXmlDataFileWriter, IField field) 
            : base(officeXmlDataFileWriter, field)
        {}

        public override string FieldType
        {
            get { return "DateTime"; }
        }

        public override string StyleID
        {
            get { return "Date"; }
        }

        public override string FormatValue(object value)
        {
            DateTime dateTimeValue = Convert.ToDateTime(value);
            return dateTimeValue.ToString("yyyy\\-MM\\-dd\\THH\\:mm\\:ss\\.fff");
        }
    }
}
