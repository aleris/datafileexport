// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using System;
using System.Text;
using TeamNet.Data.FileExport.Fields;

namespace TeamNet.Data.FileExport.Dbf
{
    class DbfDateFieldWriter : DbfFieldWriterSkeleton
    {
        public DbfDateFieldWriter(DbfDataFileWriter dbfDataFileWriter, IField field) 
            : base(dbfDataFileWriter, field)
        {}

        public override char FieldType
        {
            get { return 'D'; }
        }

        public const string DataFormatPattern = "yyyyMMdd";

        public override string FormatValue(object value)
        {
            DateTime dateValue = Convert.ToDateTime(value);
            string formatedValue = dateValue.ToString(DataFormatPattern);
            return formatedValue;
        }
    }
}