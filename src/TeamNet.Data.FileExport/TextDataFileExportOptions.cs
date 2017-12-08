// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using System;

namespace TeamNet.Data.FileExport
{
	/// <summary>
	/// Provides options for exporting Text files
	/// </summary>
	public class TextDataFileExportOptions : IDataFileExportOptions
	{
		IFormatProvider _formatProvider;
		/// <summary>
		/// Provides an object to control formatting. 
		/// Default is 'CultureInfo.CurrentCulture'.
		/// </summary>		
		public IFormatProvider FormatProvider
		{
			get { return _formatProvider; }
			set { _formatProvider = value; }
		}

		bool _includeFileHeader = true;
		/// <summary>
		/// Indicates whether the file should contain the field names as header, or not.
		/// Default is 'true'.
		/// </summary>
		public bool IncludeFileHeader
		{
			get { return _includeFileHeader; }
			set { _includeFileHeader = value; }
		}

		string _separator = string.Empty;
		/// <summary>
		/// The values separator string.
		/// Default is 'string.Empty'
		/// </summary>
		public string Separator
		{
			get { return _separator; }
			set { _separator = value; }
		}		

		string _encloseStringValuesFormat = "@value";
		/// <summary>
		/// The string format for enclosing string values. By default, values are not enclosed.
		/// </summary>
		public string EncloseStringValuesFormat
		{
			get { return _encloseStringValuesFormat; }
			set { _encloseStringValuesFormat = value; }
		}

		string _encloseNumericValuesFormat = "@value";
		/// <summary>
		/// The string format for enclosing numeric values. By default, values are not enclosed.
		/// </summary>
		public string EncloseNumericValuesFormat
		{
			get { return _encloseNumericValuesFormat; }
			set { _encloseNumericValuesFormat = value; }
		}
	}
}
