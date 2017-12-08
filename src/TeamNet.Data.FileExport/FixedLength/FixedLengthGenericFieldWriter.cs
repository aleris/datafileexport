// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using System;

namespace TeamNet.Data.FileExport.FixedLength
{
    class FixedLengthGenericFieldWriter : IFieldWriter
    {
        public IField Field { get; private set; }

        public FixedLengthDataFileWriter FixedLengthDataFileWriter { get; private set; }

        public FixedLengthGenericFieldWriter(FixedLengthDataFileWriter fixedLengthDataFileWriter, IField field)
        {
            this.FixedLengthDataFileWriter = fixedLengthDataFileWriter;
            this.Field = field;
        }

		protected virtual string FormatValue(object value)
		{
			string stringValue = Convert.ToString(value, FixedLengthDataFileWriter.Options.FormatProvider);
			string trimmedValue = Utils.GetFixedLengthString(stringValue, Field.TotalSize, ' ', PaddingPositions.Right);
			string enclosedValue = Utils.EncloseValue(trimmedValue, FixedLengthDataFileWriter.Options.EncloseStringValuesFormat);

			return enclosedValue;
		}

        public void WriteValue(object value)
        {
			string stringValue = FormatValue(value);

			this.FixedLengthDataFileWriter.StreamWriter.Write(stringValue);
        }

        public void WriteHeader(int offsetInRecord)
        {
			string trimmedValue = Utils.GetFixedLengthString(this.Field.Name, Field.TotalSize, ' ', PaddingPositions.Right);
			string enclosedName = Utils.EncloseValue(trimmedValue, FixedLengthDataFileWriter.Options.EncloseStringValuesFormat);
			this.FixedLengthDataFileWriter.StreamWriter.Write(enclosedName);
        }		
    }
}