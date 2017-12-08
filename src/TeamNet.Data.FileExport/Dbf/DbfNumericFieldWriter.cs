// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using System;
using System.Text;
using TeamNet.Data.FileExport.Dbf.Structures;
using System.Globalization;

namespace TeamNet.Data.FileExport.Dbf
{
    class DbfNumericFieldWriter : DbfFieldWriterSkeleton
    {
        public DbfNumericFieldWriter(DbfDataFileWriter dbfDataFileWriter, IField field) 
            : base(dbfDataFileWriter, field)
        {}

        public override char FieldType
        {
            get { return 'N'; }
        }

        public override string FormatValue(object value)
        {
            decimal decimalValue = Convert.ToDecimal(value);
            string decimalPlacesPlaceHolders = String.Empty.PadRight(this.Field.DecimalPlaces, '#');
            string formatString = "#0." + decimalPlacesPlaceHolders;
			string stringValue = decimalValue.ToString(
				formatString, 
				new NumberFormatInfo() 
				{ 
					NumberDecimalSeparator = "." 
				});
            string formatedValue = Utils.GetFixedLengthString(stringValue, this.Field.TotalSize,
                                                              DbfConstants.FieldValuePaddingChar, PaddingPositions.Left);
            return formatedValue;
        }
    }
}