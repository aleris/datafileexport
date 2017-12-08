// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using System;

namespace TeamNet.Data.FileExport.FixedLength
{
	class FixedLengthDateFieldWriter : FixedLengthGenericFieldWriter
	{
		public FixedLengthDateFieldWriter(FixedLengthDataFileWriter fixedLengthDataFileWriter, IField field) :
			base(fixedLengthDataFileWriter, field)
		{ }

		public const string DataFormatPattern = "dd/MM/yy";

		protected override string FormatValue(object value)
		{
			DateTime dateValue = Convert.ToDateTime(value);
			string stringValue = dateValue.ToString(DataFormatPattern);
			
			string trimmedValue = Utils.GetFixedLengthString(stringValue, Field.TotalSize, ' ', PaddingPositions.Left);
			string enclosedValue = Utils.EncloseValue(trimmedValue, FixedLengthDataFileWriter.Options.EncloseStringValuesFormat);

			return enclosedValue;
		}
	}
}
