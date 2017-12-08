// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using System;

namespace TeamNet.Data.FileExport.Csv
{
	class CsvNumericFieldWriter : CsvGenericFieldWriter
	{
		public CsvNumericFieldWriter(CsvDataFileWriter csvDataFileWriter, IField field)
			: base(csvDataFileWriter, field)
		{ }

		protected override string FormatValue(object value)
		{
			string stringValue;
			if(value is DBNull)
			{
				stringValue = string.Empty;
			}
			else
			{

				decimal decimalValue = Convert.ToDecimal(value);
				string decimalPlacesPlaceHolders = String.Empty.PadRight(this.Field.DecimalPlaces, '0');
				string formatString = "#0." + decimalPlacesPlaceHolders;
				stringValue = decimalValue.ToString(
					formatString, CsvDataFileWriter.Options.FormatProvider);
			}
			string trimmedValue = Utils.GetMaximumLengthString(stringValue, Field.TotalSize);
			string escapedValue = base.EscapeCsv(trimmedValue);
			string enclosedValue = Utils.EncloseValue(escapedValue, CsvDataFileWriter.Options.EncloseNumericValuesFormat);

			return enclosedValue;
		}
	}
}
