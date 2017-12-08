// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeamNet.Data.FileExport.Dbf.Structures;

namespace TeamNet.Data.FileExport.Dbf
{
    /// <summary>
    /// Writes DBF streams.
    /// </summary>
    /// <example>
    /// The folowing example shows how to write a DBF file usind a DataSet as a data source:
    /// <code>
    /// DataSet source = new DataSet();
    /// DataTable table = source.Tables.Add();
    /// table.Columns.Add("AString", typeof(string));
    /// table.Rows.Add("a");
    /// using (FileStream stream = new FileStream(filePath, FileMode.Create))
    /// {
    ///     DbfWriter.Create(new DataSetDataSource(source))
    ///         .AddTextField("AString", 10)
    ///         .Write(stream);
    /// }
    /// </code>
    /// </example>
    public class DbfDataFileWriter : IDataFileWriter, IDisposable
    {
        /// <summary>
        /// Gets the output stream.
        /// </summary>
        /// <value>The output stream.</value>
        public Stream Stream { get; private set; }

        /// <summary>
        /// Gets the binary writer used to write to the output stream.
        /// </summary>
        /// <value>The binary writer.</value>
        public BinaryWriter BinaryWriter { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbfDataFileWriter"/> class.
        /// </summary>
        /// <param name="stream">The output stream to write to.</param>
        public DbfDataFileWriter(Stream stream)
        {
            this.Stream = stream;
            this.BinaryWriter = new BinaryWriter(stream);
        }

        /// <summary>
        /// Writes the data file to the output stream.
        /// </summary>
        /// <param name="dataFileExport">The data file export definition.</param>
        public void Write(DataFileExport dataFileExport)
        {
            WriteFileHeader(dataFileExport);
            WriteRows(dataFileExport);
            this.BinaryWriter.Flush();
        }

        void WriteFileHeader(DataFileExport dataFileExport)
        {
            DbfHeaderStructure headerStructure = new DbfHeaderStructure();
            FillDbfFileHeaderStructure(dataFileExport, ref headerStructure);

            DbfStructureBinaryWriter.Write(this.BinaryWriter, headerStructure);

            WriteFieldsHeader(dataFileExport);

            this.BinaryWriter.Write(DbfConstants.HeaderTerminator);
        }

        void WriteFieldsHeader(DataFileExport dataFileExport)
        {
            int offset = 1;
            for (int i = 0; i < dataFileExport.Fields.Count; i++)
            {
                IField field = dataFileExport.Fields[i];
                field.GetFieldWriter(dataFileExport.ExportFormatFactory).WriteHeader(offset);
                offset += field.TotalSize;
            }
        }

        void WriteRows(DataFileExport dataFileExport)
        {
            int rowCount = dataFileExport.DataSource.RowCount;
            if (rowCount > 0 )
            {
                for (int rowIndex = 0; rowIndex != rowCount; rowIndex++)
                {
                    WriteRow(dataFileExport, rowIndex);
                }
                
                // rows end separator is written only if there are rows in the data source
                this.BinaryWriter.Write(DbfConstants.RowsEndTerminator);
            }
        }

        void WriteRow(DataFileExport dataFileExport, int rowIndex)
        {
            // deleted marker
            this.BinaryWriter.Write(DbfDeletedFlags.Valid);

            // values
            for (int fieldIndex = 0; fieldIndex != dataFileExport.Fields.Count; fieldIndex++)
            {
                IField field = dataFileExport.Fields[fieldIndex];
                IFieldWriter fieldWriter = field.GetFieldWriter(dataFileExport.ExportFormatFactory);

                object value = dataFileExport.DataSource.GetData(rowIndex, field.SourceName);
                fieldWriter.WriteValue(value);
            }
        }

        static void FillDbfFileHeaderStructure(DataFileExport dataFileExport, ref DbfHeaderStructure hdr)
        {
            hdr.VersionNumber = DbfConstants.DbfVersionDBase3;
            DateTime lastModifiedDate = DateTime.Now;
            hdr.LastUpdateYear = (byte)(lastModifiedDate.Year % 100);
            hdr.LastUpdateMonth = (byte)lastModifiedDate.Month;
            hdr.LastUpdateDay = (byte)lastModifiedDate.Day;
            hdr.EncryptionFlag = 0;
            hdr.HeaderSize = GetHeaderSize(dataFileExport);
            hdr.LanguageDriverId = DbfLanguageDrivers.WindowsAnsi;
            hdr.MdxFlag = 0;
            hdr.NumberOfRecords = dataFileExport.DataSource.RowCount;
            hdr.RecordSize = GetTotalRecordSize(dataFileExport);
            hdr.Reserved1 = 0;
            hdr.IncompleteTransaction = 0;
            hdr.FreeRecordThreadReserved = 0;
            hdr.ReservedMultiUser1 = 0;
            hdr.ReservedMultiUser2 = 0;
            hdr.Reserved2 = 0;
        }

        static short GetTotalRecordSize(DataFileExport dataFileExport)
        {
            int totalSize = dataFileExport.Fields.Sum(f => (int)f.TotalSize);
            return (short)(1 + totalSize);
        }

        static short GetHeaderSize(DataFileExport dataFileExport)
        {
            return (short)(DbfHeaderStructure.StructureSize 
                           + DbfConstants.HeaderTerminatorSize
                           + DbfFieldHeaderStructure.StructureSize * dataFileExport.Fields.Count);
        }

        void IDisposable.Dispose()
        {
            this.BinaryWriter.BaseStream.Dispose();
        }
    }
}