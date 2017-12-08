// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using TeamNet.Data.FileExport.DataSources;
using TeamNet.Data.FileExport.Csv;
using TeamNet.Data.FileExport.Dbf;
using TeamNet.Data.FileExport.OfficeXml;
using TeamNet.Data.FileExport.FixedLength;

namespace TeamNet.Data.FileExport
{
    /// <summary>
    /// Provides a entry point for exporting tabular data to a data file.
    /// </summary>
    public class DataFileExport
    {
        /// <summary>
        /// Gets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public IDataSource DataSource { get; set; }

        private readonly List<IField> _fields = new List<IField>();
        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <value>The fields.</value>
        public IList<IField> Fields { get { return _fields; } }

        /// <summary>
        /// Gets the data file format.
        /// </summary>
        /// <value>The data file format.</value>
        public IDataFileExportFormatFactory ExportFormatFactory { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataFileExport"/> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="exportFormatFactory">The data file format.</param>
        public DataFileExport(IDataSource dataSource, IDataFileExportFormatFactory exportFormatFactory)
        {
            DataSource = dataSource;
            ExportFormatFactory = exportFormatFactory;
        }

        /// <summary>
        /// Creates a <see cref="DataFileExport" /> that writes the specified data source with the specified format.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="dataFileExportFormatFactory">The data file format.</param>
        /// <returns>A new data file export definition.</returns>
        public static DataFileExport Create(IDataSource dataSource, IDataFileExportFormatFactory dataFileExportFormatFactory)
        {
            Guard.Against<ArgumentNullException>(null == dataSource, "dataSource");
            Guard.Against<ArgumentNullException>(null == dataFileExportFormatFactory, "dataFileExportFormatFactory");

            DataFileExport dataFileExport = new DataFileExport(dataSource, dataFileExportFormatFactory);

            return dataFileExport;
        }

        #region Create Overloads

        /// <summary>
        /// Creates a <see cref="DataFileExport" /> that writes data from the source data with DBF file format.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <returns>A new data file export definition.</returns>
        public static DataFileExport CreateDbf(IDataSource dataSource)
        {
            return Create(dataSource, new DbfDataFileExportFormatFactory());
        }

        /// <summary>
        /// Creates a <see cref="DataFileExport" /> that writes data from the source DataSet with DBF file format.
        /// </summary>
        /// <param name="sourceDataSet">The source data set.</param>
        /// <returns>A new data file export definition.</returns>
        public static DataFileExport CreateDbf(DataSet sourceDataSet)
        {
            return CreateDbf(new DataSetDataSource(sourceDataSet));
        }

        /// <summary>
        /// Creates a <see cref="DataFileExport" /> that writes data from the source item list with DBF file format.
        /// </summary>
        /// <param name="sourceItems">The source items.</param>
        /// <returns>A new data file export definition.</returns>
        public static DataFileExport CreateDbf(IList sourceItems)
        {
            return CreateDbf(new PlainObjectsDataSource(sourceItems));
        }

        /// <summary>
        /// Creates a <see cref="DataFileExport" /> that writes data from the source data with CSV file format.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <returns>A new data file export definition.</returns>
        public static DataFileExport CreateCsv(IDataSource dataSource)
        {
            return Create(dataSource, new CsvDataFileExportFormatFactory());
        }

        /// <summary>
        /// Creates a <see cref="DataFileExport" /> that writes data from the source DataSet with CSV file format.
        /// </summary>
        /// <param name="sourceDataSet">The source data set.</param>
        /// <returns>A new data file export definition.</returns>
        public static DataFileExport CreateCsv(DataSet sourceDataSet)
        {
            return CreateCsv(new DataSetDataSource(sourceDataSet));
        }

        /// <summary>
        /// Creates a <see cref="DataFileExport" /> that writes data from the source item list with CSV file format.
        /// </summary>
        /// <param name="sourceItems">The source items.</param>
        /// <returns>A new data file export definition.</returns>
        public static DataFileExport CreateCsv(IList sourceItems)
        {
            return CreateCsv(new PlainObjectsDataSource(sourceItems));
        }

        /// <summary>
        /// Creates a <see cref="DataFileExport" /> that writes data from the source data with OfficeXml file format.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <returns>A new data file export definition.</returns>
        public static DataFileExport CreateOfficeXml(IDataSource dataSource)
        {
            return Create(dataSource, new OfficeXmlDataFileExportFormatFactory());
        }

        /// <summary>
        /// Creates a <see cref="DataFileExport" /> that writes data from the source DataSet with OfficeXml file format.
        /// </summary>
        /// <param name="sourceDataSet">The source data set.</param>
        /// <returns>A new data file export definition.</returns>
        public static DataFileExport CreateOfficeXml(DataSet sourceDataSet)
        {
            return CreateOfficeXml(new DataSetDataSource(sourceDataSet));
        }

        /// <summary>
        /// Creates a <see cref="DataFileExport" /> that writes data from the source item list with OfficeXml file format.
        /// </summary>
        /// <param name="sourceItems">The source items.</param>
        /// <returns>A new data file export definition.</returns>
        public static DataFileExport CreateOfficeXml(IList sourceItems)
        {
            return CreateOfficeXml(new PlainObjectsDataSource(sourceItems));
        }

		/// <summary>
		/// Creates a <see cref="DataFileExport" /> that writes data from the source data with FixedLength file format.
		/// </summary>
		/// <param name="dataSource">The data source.</param>
		/// <returns>A new data file export definition.</returns>
		public static DataFileExport CreateFixedLength(IDataSource dataSource)
		{
			return Create(dataSource, new FixedLengthDataFileExportFormatFactory());
		}

		/// <summary>
		/// Creates a <see cref="DataFileExport" /> that writes data from the source DataSet with FixedLength file format.
		/// </summary>
		/// <param name="sourceDataSet">The source data set.</param>
		/// <returns>A new data file export definition.</returns>
		public static DataFileExport CreateFixedLength(DataSet sourceDataSet)
		{
			return CreateFixedLength(new DataSetDataSource(sourceDataSet));
		}

		/// <summary>
		/// Creates a <see cref="DataFileExport" /> that writes data from the source item list with FixedLength file format.
		/// </summary>
		/// <param name="sourceItems">The source items.</param>
		/// <returns>A new data file export definition.</returns>
		public static DataFileExport CreateFixedLength(IList sourceItems)
		{
			return CreateFixedLength(new PlainObjectsDataSource(sourceItems));
		}
        #endregion

        /// <summary>
        /// Changes the file format to CSV.
        /// </summary>
        /// <returns>The data file export definition.</returns>
        public DataFileExport AsCsv()
        {
            this.ExportFormatFactory = new CsvDataFileExportFormatFactory();
            return this;
        }

        /// <summary>
        /// Changes the file format to DBF.
        /// </summary>
        /// <returns>The data file export definition.</returns>
        public DataFileExport AsDbf()
        {
            this.ExportFormatFactory = new DbfDataFileExportFormatFactory();
            return this;
        }

		/// <summary>
		/// Changes the file format to FixedLength.
		/// </summary>
		/// <returns>The data file export definition.</returns>
		public DataFileExport AsFixedLength()
		{
			this.ExportFormatFactory = new FixedLengthDataFileExportFormatFactory();
			return this;
		}

        /// <summary>
        /// Writes data from the data source to the specified stream using the current file format.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <returns>The data file export definition.</returns>
        public DataFileExport Write(Stream stream)
        {
            Guard.Against<ArgumentNullException>(null == stream, "stream");

            using (IDataFileWriter writer = this.ExportFormatFactory.CreateWriter(stream))
            {
                writer.Write(this);
            }
            return this;
        }

        /// <summary>
        /// Writes data from the data source to the specified file using the current file format.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The data file export definition.</returns>
        public DataFileExport Write(string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                Write(stream);
            }
            return this;
        }
    }
}
