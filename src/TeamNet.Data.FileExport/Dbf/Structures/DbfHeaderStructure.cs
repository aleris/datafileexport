// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
namespace TeamNet.Data.FileExport.Dbf.Structures
{
    struct DbfHeaderStructure
    {
        public byte VersionNumber;
        public byte LastUpdateYear;
        public byte LastUpdateMonth;
        public byte LastUpdateDay;
        public int NumberOfRecords;
        public short HeaderSize;
        public short RecordSize;
        public short Reserved1;
        public byte IncompleteTransaction;
        public byte EncryptionFlag;
        public int FreeRecordThreadReserved;
        public int ReservedMultiUser1;
        public int ReservedMultiUser2;
        public byte MdxFlag;
        public byte LanguageDriverId;
        public short Reserved2;

        public static readonly int StructureSize = 32; // eg: sizeof(DbfHeaderStructure)
    }
}