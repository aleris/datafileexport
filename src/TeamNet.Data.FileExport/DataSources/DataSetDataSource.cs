// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using System;
using System.Data;

namespace TeamNet.Data.FileExport.DataSources
{
    /// <summary>
    /// Implements a <see cref="IDataSource"/> for reading data from a <see cref="DataSet"/>.
    /// </summary>
    public class DataSetDataSource : IDataSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataSetDataSource"/> class.
        /// </summary>
        /// <param name="sourceDataSet">The source data set.</param>
        public DataSetDataSource(DataSet sourceDataSet)
        {
            Guard.Against<ArgumentNullException>(sourceDataSet == null, "sourceDataSet");
            Guard.Against<ArgumentException>(sourceDataSet.Tables.Count == 0, "sourceDataSet has 0 Tables");

            _sourceDataSet = sourceDataSet;
            _cachedRows = GetDataTable().Rows;
        }

        private string _memberName;
        /// <summary>
        /// Gets or sets the DataTable name.
        /// </summary>
        /// <value>The DataTable name.</value>
        public string MemberName
        {
            get { return _memberName; }
            set
            {
                _memberName = value;
                _cachedRows = GetDataTable().Rows;
            }
        }

        /// <summary>
        /// Gets the row count in the data source.
        /// </summary>
        /// <value>The row count.</value>
        public int RowCount
        {
            get { return this.GetDataTable().Rows.Count; }
        }

        private readonly DataSet _sourceDataSet;
        /// <summary>
        /// Gets the source data set.
        /// </summary>
        /// <value>The source data set.</value>
        public DataSet SourceDataSet
        {
            get { return _sourceDataSet; }
        }

        /// <summary>
        /// Gets the data value for the field name at the specified row index. The data is taken from the <see cref="DataTable"/> specified in MemberName or in the first one if MemberName is not specified.
        /// </summary>
        /// <param name="rowIndex">The row index.</param>
        /// <param name="name">The field name.</param>
        /// <returns>
        /// The value for the field name at the specified row index.
        /// </returns>
        public object GetData(int rowIndex, string name)
        {
            DataRow row = _cachedRows[rowIndex];
            return row[name];
        }

        /// <summary>
        /// Gets the <see cref="DataTable"/> from the source <see cref="DataSet"/> specified in MemberName or in the first one if MemberName is not specified..
        /// </summary>
        /// <returns>A <see cref="DataTable"/>.</returns>
        DataTable GetDataTable()
        {
            if (String.IsNullOrEmpty(this.MemberName))
            {
                return this.SourceDataSet.Tables[0];
            }
            else
            {
                DataTable dataTable = this.SourceDataSet.Tables[this.MemberName];
                if (null == dataTable) throw new InvalidOperationException(this.MemberName + " Table does not exists in the SourceDataSet.");
                return dataTable;
            }
        }

        private DataRowCollection _cachedRows;
    }
}
