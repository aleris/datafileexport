// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace TeamNet.Data.FileExport.DataSources
{
    /// <summary>
    /// Implements a <see cref="IDataSource"/> for reading data from a list of objects. 
    /// </summary>
    /// <remarks>
    /// The objects in the source list should have public properties that are accesed by name to retrieve the values.
    /// </remarks>
    public class PlainObjectsDataSource : IDataSource
    {
        /// <summary>
        /// Gets the data source list of items.
        /// </summary>
        /// <value>A <see cref="IList"/> of objects.</value>
        public IList Items { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlainObjectsDataSource"/> class.
        /// </summary>
        /// <param name="items">The list of items.</param>
        public PlainObjectsDataSource(IList items)
        {
            Guard.Against<ArgumentNullException>(items == null, "items");

            this.Items = items;
            if (items.Count > 0)
            {
                _cachedType = items[0].GetType();
                _cachedProperties = new Dictionary<string, PropertyInfo>();
            }
        }

        /// <summary>
        /// Gets the row count in the data source.
        /// </summary>
        /// <value>The row count.</value>
        public int RowCount
        {
            get { return this.Items.Count; }
        }

        /// <summary>
        /// Gets the data value for the field name at the specified index.
        /// </summary>
        /// <param name="index">The index in list.</param>
        /// <param name="name">The object property name.</param>
        /// <returns>
        /// The value for the field name at the specified row index.
        /// </returns>
        public object GetData(int index, string name)
        {
            object obj = this.Items[index];
            object value = GetCachedProperty(name).GetValue(obj, null);
            return value;
        }

        PropertyInfo GetCachedProperty(string name)
        {
            PropertyInfo property;
            if (!_cachedProperties.TryGetValue(name, out property))
            {
                property = _cachedType.GetProperty(name);
                _cachedProperties.Add(name, property);
            }
            if (null == property)
            {
                throw new InvalidOperationException("Property '" + name + "' not found in '" + _cachedType.FullName + "'.");
            }
            return property;
        }

        private readonly Dictionary<string, PropertyInfo> _cachedProperties;
        private readonly Type _cachedType;
    }
}
