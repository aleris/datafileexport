﻿// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using System;

namespace TeamNet.Data.FileExport.Dbf.Structures
{
    struct DbfFieldHeaderStructure
    {
        public string FieldName;
        public char FieldType;
        public byte TotalSize;
        public int OffsetInRecord;
        public byte DecimalPlaces;
        public short Reserved1;
        public byte WorkAreaId;
        public short MultiUser;
        public byte SetField;
        public Int32 Reserved21;
        public Int16 Reserver22;
        public byte Reserved23;
        public byte IncludeInMdx;

        public static readonly int StructureSize = 32; // eg: sizeof(DbfFieldHeaderStructure)
    }
}