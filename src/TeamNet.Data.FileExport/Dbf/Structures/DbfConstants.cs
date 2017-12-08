// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
namespace TeamNet.Data.FileExport.Dbf.Structures
{
    class DbfConstants
    {
        public static readonly byte HeaderTerminator = 0x0D;
        public static readonly int HeaderTerminatorSize = sizeof(byte); //eg: sizeof(DbfConstants.HeaderTerminator)

        public static readonly byte RowsEndTerminator = 0x1A;
        public static readonly int RowsEndTerminatorSize = sizeof(byte);//eg: sizeof(DbfConstants.RowsEndTerminator)

        public static readonly char FieldNamePaddingChar = (char)0;
        public static readonly char FieldValuePaddingChar = ' ';

        public static readonly int FieldNameLength = 11;

        public static readonly byte DateFieldLength = 8;
        public static readonly byte LogicalFieldLength = 1;

        public static readonly byte DbfVersionDBase3 = 0x03;

        public static readonly byte FieldMaximumLength = 255;
        public static readonly byte FieldMaximumDecimals = 15;
    }
}