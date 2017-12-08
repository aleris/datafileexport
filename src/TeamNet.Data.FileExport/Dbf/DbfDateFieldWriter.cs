// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
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