// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.

using System;
using System.Globalization;

namespace TeamNet.Data.FileExport
{
	/// <summary>
	/// Provides options for exporting text files.
	/// </summary>
	public interface IDataFileExportOptions
	{
		/// <summary>
		/// Provides an object to control formatting. 
		/// </summary>
		IFormatProvider FormatProvider { get; set; }

		/// <summary>
		/// Indicates whether the file should contain the field names as header, or not.
		/// </summary>
		bool IncludeFileHeader { get; set; }

		/// <summary>
		/// The string used for separating values.
		/// </summary>
		string Separator { get; set; }
				
		/// <summary>
		/// The format for enclosing string values. 	
		/// Example: "[@value]" for eclosing in brackets or '"@value"' for enclosing in quotas
		/// </summary>
		string EncloseStringValuesFormat { get; set; }

		/// <summary>
		/// The format for enclosing numeric values. 		
		/// </summary>
		string EncloseNumericValuesFormat { get; set; }
	}
}
