// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using System;
using TeamNet.Data.FileExport.Dbf.Structures;
using TeamNet.Data.FileExport.Fields;

namespace TeamNet.Data.FileExport.Dbf
{
    class DbfLogicalFieldWriter : DbfFieldWriterSkeleton
    {
        public DbfLogicalFieldWriter(DbfDataFileWriter dbfDataFileWriter, IField field)
            : base(dbfDataFileWriter, field)
        {}

        public override char FieldType
        {
            get { return 'L'; }
        }

        public override string FormatValue(object value)
        {
            bool booleanValue = Convert.ToBoolean(value);
            string formattedValue = booleanValue ? DbfLogicalValues.True : DbfLogicalValues.False;
            return formattedValue;
        }
    }
}