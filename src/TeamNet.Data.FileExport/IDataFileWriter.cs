// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
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