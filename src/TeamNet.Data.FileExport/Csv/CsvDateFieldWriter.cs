// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using System;

namespace TeamNet.Data.FileExport.Csv
{
	class CsvDateFieldWriter : CsvGenericFieldWriter
	{
		public CsvDateFieldWriter(CsvDataFileWriter csvDataFileWriter, IField field)
			: base(csvDataFileWriter, field)
		{ }

		public const string DataFormatPattern = "dd/MM/yy";

		protected override string FormatValue(object value)
		{
			DateTime dateValue = Convert.ToDateTime(value);
			string stringValue = dateValue.ToString(DataFormatPattern);
			string escapedValue = EscapeCsv(stringValue);
			string enclosedValue = Utils.EncloseValue(escapedValue, CsvDataFileWriter.Options.EncloseStringValuesFormat);

			return enclosedValue;			
		}
	}
}
