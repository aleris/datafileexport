// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using System;
using System.IO;

namespace TeamNet.Data.FileExport
{
    /// <summary>
    /// Writes tabular data to a data file.
    /// </summary>
    public interface IDataFileWriter : IDisposable
    {
        /// <summary>
        /// Writes the data to the specified stream.
        /// </summary>
        /// <param name="dataFileExport">The data file export definition.</param>
        void Write(DataFileExport dataFileExport);
    }
}