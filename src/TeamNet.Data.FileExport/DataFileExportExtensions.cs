// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using TeamNet.Data.FileExport.Fields;

namespace TeamNet.Data.FileExport
{
    /// <summary>
    /// Provides fluent interface extensions for <see cref="DataFileExport" />.
    /// </summary>
    public static class DataFileExportExtensions
    {
        /// <summary>
        /// Adds a new <see cref="TextField"/> to this <see cref="DataFileExport"/> with data source field name the same as the field name.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <param name="totalSize">The field total size.</param>
        /// <returns>The <see cref="DataFileExport"/>.</returns>
        public static DataFileExport AddTextField(this DataFileExport dataFileExport, string name, byte totalSize)
        {
            TextField field = new TextField(name, totalSize);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="TextField"/> to this <see cref="DataFileExport"/> with data source field name the same as the field name and with the default size of 255.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <returns>The <see cref="DataFileExport"/>.</returns>
        public static DataFileExport AddTextField(this DataFileExport dataFileExport, string name)
        {
            TextField field = new TextField(name);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="TextField"/> to the <see cref="DataFileExport"/>.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <param name="totalSize">The field total size.</param>
        /// <param name="sourceName">The data source field name.</param>
        /// <returns>The <see cref="DataFileExport"/>.</returns>
        public static DataFileExport AddTextField(this DataFileExport dataFileExport, string name, byte totalSize, string sourceName)
        {
            TextField field = new TextField(name, totalSize, sourceName);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="TextField"/> to the <see cref="DataFileExport"/> with the default size of 255.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <param name="sourceName">The data source field name.</param>
        /// <returns>The <see cref="DataFileExport"/>.</returns>
        public static DataFileExport AddTextField(this DataFileExport dataFileExport, string name, string sourceName)
        {
            TextField field = new TextField(name, sourceName);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="NumericField"/> to this <see cref="DataFileExport"/> with data source field name the same as the field name.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <param name="totalSize">The field total size.</param>
        /// <param name="decimalPlaces">The decimal places.</param>
        /// <returns>The <see cref="DataFileExport"/>.</returns>
        public static DataFileExport AddNumericField(this DataFileExport dataFileExport, string name, byte totalSize, byte decimalPlaces)
        {
            NumericField field = new NumericField(name, totalSize, decimalPlaces);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="NumericField"/> to this <see cref="DataFileExport"/> with data source field name the same as the field name and with the default total size of 255 and 15 decimal places.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <returns>The <see cref="DataFileExport"/>.</returns>
        public static DataFileExport AddNumericField(this DataFileExport dataFileExport, string name)
        {
            NumericField field = new NumericField(name);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="NumericField"/> to the <see cref="DataFileExport"/>.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <param name="totalSize">The field total size.</param>
        /// <param name="decimalPlaces">The decimal places.</param>
        /// <param name="sourceName">The data source field name.</param>
        /// <returns>The <see cref="DataFileExport"/>.</returns>
        public static DataFileExport AddNumericField(this DataFileExport dataFileExport, string name, byte totalSize, byte decimalPlaces, string sourceName)
        {
            NumericField field = new NumericField(name, totalSize, decimalPlaces, sourceName);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="NumericField"/> to the <see cref="DataFileExport"/> with the default total size of 255 and 15 decimal places.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <param name="sourceName">The data source field name.</param>
        /// <returns>The <see cref="DataFileExport"/>.</returns>
        public static DataFileExport AddNumericField(this DataFileExport dataFileExport, string name, string sourceName)
        {
            NumericField field = new NumericField(name, sourceName);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="DateField"/> to this <see cref="DataFileExport"/> with data source field name the same as the field name.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <returns>The <see cref="DataFileExport"/>.</returns>
        public static DataFileExport AddDateField(this DataFileExport dataFileExport, string name)
        {
            DateField field = new DateField(name);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="DateField"/> to the <see cref="DataFileExport"/>.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <param name="sourceName">The data source field name.</param>
        /// <returns>The <see cref="DataFileExport"/>.</returns>
        /// <remarks>The date field might contain time information but the will be only written if the writter supports it. 
        /// For example DBF date fields contain only the date without time information.</remarks>
        public static DataFileExport AddDateField(this DataFileExport dataFileExport, string name, string sourceName)
        {
            DateField field = new DateField(name, sourceName);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="BooleanField"/> to this <see cref="DataFileExport"/> with data source field name the same as the field name.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <returns>The <see cref="DataFileExport"/>.</returns>
        public static DataFileExport AddBooleanField(this DataFileExport dataFileExport, string name)
        {
            BooleanField field = new BooleanField(name);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="BooleanField"/> to the <see cref="DataFileExport"/>.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The name.</param>
        /// <param name="sourceName">The data source field name.</param>
        /// <returns>The <see cref="DataFileExport"/>.</returns>
        public static DataFileExport AddBooleanField(this DataFileExport dataFileExport, string name, string sourceName)
        {
            BooleanField field = new BooleanField(name, sourceName);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="GenericField"/> to this <see cref="DataFileExport"/> with data source field name the same as the field name.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <returns>The <see cref="DataFileExport"/>.</returns>
        public static DataFileExport AddGenericField(this DataFileExport dataFileExport, string name)
        {
            GenericField field = new GenericField(name);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="GenericField"/> to the <see cref="DataFileExport"/>.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <param name="sourceName">The data source field name.</param>
        /// <returns>The <see cref="DataFileExport"/>.</returns>
        public static DataFileExport AddGenericField(this DataFileExport dataFileExport, string name, string sourceName)
        {
            GenericField field = new GenericField(name, sourceName);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }
    }
}