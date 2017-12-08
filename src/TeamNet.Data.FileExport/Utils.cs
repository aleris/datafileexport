// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using System;

namespace TeamNet.Data.FileExport
{
	enum PaddingPositions
	{
		Left,
		Right
	}

    static class Utils
    {
        /// <summary>
        /// Gets a string with a fixed length, padding or shrinking if necessary.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="length">The desired length.</param>
        /// <param name="paddingChar">The padding char used if the input string value is smaller than the desired length.</param>
        /// <param name="paddingPosition">The padding position specifies where the string will be padded when the input string value is smaller than the desired length.</param>
        /// <returns>A string value with the specified length.</returns>
        public static string GetFixedLengthString(string value, int length, char paddingChar, PaddingPositions paddingPosition)
        {
            Guard.Against<ArgumentNullException>(value == null, "value");
            Guard.Against<ArgumentException>(length < 0, "length < 0");

            string final;

            if (value.Length < length)
            {
                if (paddingPosition == PaddingPositions.Right)
                {
                    final = value.PadRight(length, paddingChar);
                }
                else if (paddingPosition == PaddingPositions.Left)
                {
                    final = value.PadLeft(length, paddingChar);
                }
                else
                {
                    throw new NotImplementedException(paddingPosition.ToString());
                }
            }
            else if (value.Length > length)
            {
                final = value.Substring(0, length);
            }
            else
            {
                final = value;
            }

            return final;
        }

		/// <summary>
		/// Gets a string winth a maximum length, shrinking it if necessary
		/// </summary>
		/// <param name="value">The string value.</param>
		/// <param name="length">The maximum length</param>
		/// <returns>A string with the specified maximum length</returns>
		public static string GetMaximumLengthString(string value, int length)
		{
			Guard.Against<ArgumentNullException>(value == null, "value");
			Guard.Against<ArgumentException>(length < 0, "length < 0");

			string final;

			if(value.Length > length)
			{
				final = value.Substring(0, length);
			}
			else
			{
				final = value;
			}

			return final;
		}
			
		/// <summary>
		/// Applies the enclosing format specified in DataFileExportOptions.EncloseValuesFormat
		/// </summary>
		/// <param name="value">The string value</param>
		/// <param name="encloseFormatString">The string format (e.g. "[@value]")</param>
		/// <returns>A string in the specified format</returns>
		public static string EncloseValue(string value, string encloseFormatString)
		{
			Guard.Against<ArgumentNullException>(value == null, "value");
			Guard.Against<ArgumentNullException>(encloseFormatString == null, "encloseFormatString");

			string format = encloseFormatString.Replace("@value", "{0}");
			return string.Format(format, value);
		}
    }
}
