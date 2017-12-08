// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using System;

namespace TeamNet.Data.FileExport.FixedLength
{
	class FixedLengthNumericFieldWriter : FixedLengthGenericFieldWriter
	{
		public FixedLengthNumericFieldWriter(FixedLengthDataFileWriter fixedLengthDataFileWriter, IField field) :
			base(fixedLengthDataFileWriter, field)
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
					formatString, FixedLengthDataFileWriter.Options.FormatProvider);
			}

			string trimmedValue = Utils.GetFixedLengthString(stringValue, Field.TotalSize, ' ', PaddingPositions.Left);
			string enclosedValue = Utils.EncloseValue(trimmedValue, FixedLengthDataFileWriter.Options.EncloseNumericValuesFormat);

			return enclosedValue;            
		}
	}
}
