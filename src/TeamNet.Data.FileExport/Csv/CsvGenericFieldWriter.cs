// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using System;

namespace TeamNet.Data.FileExport.Csv
{
    class CsvGenericFieldWriter : IFieldWriter
    {
        public IField Field { get; private set; }

        public CsvDataFileWriter CsvDataFileWriter { get; private set; }

        public CsvGenericFieldWriter(CsvDataFileWriter csvDataFileWriter, IField field)
        {
            this.CsvDataFileWriter = csvDataFileWriter;
            this.Field = field;
        }

		protected virtual string FormatValue(object value)
		{
			string stringValue = Convert.ToString(value, CsvDataFileWriter.Options.FormatProvider);
			string trimmedValue = Utils.GetMaximumLengthString(stringValue, Field.TotalSize);
			string escapedValue = EscapeCsv(trimmedValue);
			string enclosedValue = Utils.EncloseValue(escapedValue, CsvDataFileWriter.Options.EncloseStringValuesFormat);

			return enclosedValue;
		}

        public void WriteValue(object value)
        {
			string stringValue = FormatValue(value);
			this.CsvDataFileWriter.StreamWriter.Write(stringValue);
        }

        public void WriteHeader(int offsetInRecord)
        {
            string escapedName = EscapeCsv(this.Field.Name);
			string enclosedName = Utils.EncloseValue(escapedName, CsvDataFileWriter.Options.EncloseStringValuesFormat);
            this.CsvDataFileWriter.StreamWriter.Write(enclosedName);
        }

        protected string EscapeCsv(string value)
        {
            // Double all quote characters
            string escaped = value.Replace("\"", "\"\"");

            // If it contains a comma, a quote char or rows separator, qualify it with quotes.
            if (escaped.IndexOf('"') > -1 || escaped.IndexOf(CsvDataFileWriter.Options.Separator) > -1 || escaped.IndexOf(Csv.CsvDataFileWriter.CsvRowsSeparator) > -1)
            {
                escaped = "\"" + escaped + "\"";
            }

            return escaped;
        }
    }
}