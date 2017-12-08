// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using System;
using System.Text;
using TeamNet.Data.FileExport.Dbf.Structures;

namespace TeamNet.Data.FileExport.Dbf
{
    abstract class DbfFieldWriterSkeleton : IFieldWriter
    {
        public IField Field { get; private set; }

        public abstract char FieldType { get; }
        
        public DbfDataFileWriter DbfDataFileWriter { get; private set; }

        protected DbfFieldWriterSkeleton(DbfDataFileWriter dbfDataFileWriter, IField field)
        {
            this.DbfDataFileWriter = dbfDataFileWriter;
            this.Field = field;
        }

        public abstract string FormatValue(object value);

        public virtual void WriteValue(object value)
        {
            if (IsNullValue(value))
            {
                WriteNull();
            }
            else
            {
                string formattedValue = this.FormatValue(value);
                byte[] bufferToWrite = Encoding.ASCII.GetBytes(formattedValue);
                this.DbfDataFileWriter.BinaryWriter.Write(bufferToWrite);
            }
        }

        public void WriteHeader(int offsetInRecord)
        {
            DbfFieldHeaderStructure fieldHeaderStructure =
                new DbfFieldHeaderStructure
                {
                    FieldName = this.Field.Name,
                    FieldType = this.FieldType,
                    TotalSize = this.Field.TotalSize,
                    OffsetInRecord = offsetInRecord,
                    DecimalPlaces = this.Field.DecimalPlaces,
                    Reserved1 = 0,
                    WorkAreaId = 0,
                    MultiUser = 0,
                    SetField = 0,
                    Reserved21 = 0,
                    Reserver22 = 0,
                    Reserved23 = 0,
                    IncludeInMdx = 0
                };

            DbfStructureBinaryWriter.Write(this.DbfDataFileWriter.BinaryWriter, fieldHeaderStructure);
        }

        protected bool IsNullValue(object value)
        {
            if (null == value) return true;
            if (DBNull.Value == value) return true;
            return false;
        }

        protected void WriteNull()
        {
            // null values are filled with spaces in DBF
            string nullValueString = Utils.GetFixedLengthString(String.Empty, this.Field.TotalSize, ' ', PaddingPositions.Right);
            this.DbfDataFileWriter.BinaryWriter.Write(Encoding.ASCII.GetBytes(nullValueString));
        }
    }
}