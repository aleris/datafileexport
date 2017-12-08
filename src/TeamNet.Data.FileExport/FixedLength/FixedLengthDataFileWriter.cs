// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using System;
using System.IO;

namespace TeamNet.Data.FileExport.FixedLength
{
    /// <summary>
    /// Writes FixedLength streams.
    /// </summary>
    public class FixedLengthDataFileWriter : IDataFileWriter, IDisposable
    {
		/// <summary>
		/// Gets or sets the export options.
		/// </summary>
		public IDataFileExportOptions Options { get; private set; }

        /// <summary>
        /// FixedLength rows separator.
        /// </summary>
        public const string FixedLengthRowsSeparator = "\r\n";

        /// <summary>
        /// Gets the output stream.
        /// </summary>
        /// <value>The output stream.</value>
        public Stream Stream { get; private set; }

        /// <summary>
        /// Gets the stream writer used to write to the output stream.
        /// </summary>
        /// <value>The stream writer.</value>
        public StreamWriter StreamWriter { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedLengthDataFileWriter"/> class.
        /// </summary>
        /// <param name="stream">The output stream to write to.</param>
		/// <param name="options">The data file export options.</param>
        public FixedLengthDataFileWriter(Stream stream, IDataFileExportOptions options)
        {
            this.Stream = stream;
            this.StreamWriter = new StreamWriter(stream);
			this.Options = options;
        }

        /// <summary>
        /// Writes the data file to the output stream.
        /// </summary>
        /// <param name="dataFileExport">The data file export definition.</param>
        public void Write(DataFileExport dataFileExport)
        {
			if(this.Options.IncludeFileHeader)
			{
				this.WriteFileHeader(dataFileExport);
			}
            this.WriteRows(dataFileExport);
            this.StreamWriter.Flush();
        }

        void WriteFileHeader(DataFileExport dataFileExport)
        {
            WriteFieldsHeader(dataFileExport);
        }

        void WriteFieldsHeader(DataFileExport dataFileExport)
        {
            int offset = 1;
            for (int fieldIndex = 0; fieldIndex < dataFileExport.Fields.Count; fieldIndex++)
            {
                IField field = dataFileExport.Fields[fieldIndex];
                field.GetFieldWriter(dataFileExport.ExportFormatFactory).WriteHeader(offset);
                offset += field.TotalSize;
                if (fieldIndex != dataFileExport.Fields.Count - 1)
                {
                    this.StreamWriter.Write(this.Options.Separator);
                }
            }
            this.StreamWriter.Write(FixedLengthRowsSeparator);
        }

        void WriteRows(DataFileExport dataFileExport)
        {
            int rowCount = dataFileExport.DataSource.RowCount;
            if (rowCount > 0 )
            {
                for (int rowIndex = 0; rowIndex != rowCount; rowIndex++)
                {
                    WriteRow(dataFileExport, rowIndex);

                    if (rowIndex != rowCount - 1)
                    {
                        this.StreamWriter.Write(FixedLengthRowsSeparator);
                    }
                }
            }
        }

        void WriteRow(DataFileExport dataFileExport, int rowIndex)
        {
            // values
            for (int fieldIndex = 0; fieldIndex != dataFileExport.Fields.Count; fieldIndex++)
            {
                IField field = dataFileExport.Fields[fieldIndex];
                IFieldWriter fieldWriter = field.GetFieldWriter(dataFileExport.ExportFormatFactory);

                object value = dataFileExport.DataSource.GetData(rowIndex, field.SourceName);
                fieldWriter.WriteValue(value);

                if (fieldIndex != dataFileExport.Fields.Count - 1)
                {
                    this.StreamWriter.Write(this.Options.Separator);
                }
            }
        }

        void IDisposable.Dispose()
        {
            this.StreamWriter.Dispose();
        }
    }
}