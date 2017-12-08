// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using System;
using System.Text;
using TeamNet.Data.FileExport.Dbf.Structures;

namespace TeamNet.Data.FileExport.Dbf
{
    class DbfCharacterFieldWriter : DbfFieldWriterSkeleton
    {
        public DbfCharacterFieldWriter(DbfDataFileWriter dbfDataFileWriter, IField field)
            : base(dbfDataFileWriter, field)
        {}

        public override char FieldType
        {
            get { return 'C'; }
        }

        public override string FormatValue(object value)
        {
            string stringValue = Convert.ToString(value);
            string fixedLengthValue = Utils.GetFixedLengthString(stringValue, this.Field.TotalSize,
                                                                 DbfConstants.FieldValuePaddingChar,
                                                                 PaddingPositions.Right);
            return fixedLengthValue;
        }
    }
}