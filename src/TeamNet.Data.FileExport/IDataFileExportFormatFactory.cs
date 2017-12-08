// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using System.IO;

namespace TeamNet.Data.FileExport
{
    /// <summary>
    /// Defines methods for creating writers for specific data file export format.
    /// </summary>
    public interface IDataFileExportFormatFactory
    {
        /// <summary>
        /// Creates a data file export writer.
        /// </summary>
        /// <param name="stream">The output stream to which the writer will write to.</param>
        /// <returns>A data file writer.</returns>
        IDataFileWriter CreateWriter(Stream stream);

        /// <summary>
        /// Creates a text field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A text field writer</returns>
        IFieldWriter CreateTextFieldWriter(IField field);

        /// <summary>
        /// Creates a numeric field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A numeric field writer</returns>
        IFieldWriter CreateNumericFieldWriter(IField field);

        /// <summary>
        /// Creates a date field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A date field writer.</returns>
        IFieldWriter CreateDateFieldWriter(IField field);

        /// <summary>
        /// Creates a boolean field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A boolean field writer.</returns>
        IFieldWriter CreateBooleanFieldWriter(IField field);

        /// <summary>
        /// Creates a generic field writer.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>A generic field writer.</returns>
        IFieldWriter CreateGenericFieldWriter(IField field);
    }
}