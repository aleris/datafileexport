using System;
using System.Linq;
using TeamNet.Data.FileExport.Dbf.Structures;

namespace TeamNet.Data.FileExport.Tests
{
    class TestHelpers
    {
        public static int GetFieldValuePosition(DataFileExport dataFileExport, int rowIndex, int fieldIndex)
        {
            int position = DbfHeaderStructure.StructureSize
                           + dataFileExport.Fields.Count * DbfFieldHeaderStructure.StructureSize
                           + DbfConstants.HeaderTerminatorSize
                           + (rowIndex + 1) * DbfDeletedFlags.StructureSize;

            int fieldPosition = dataFileExport.Fields.Sum(f => f.TotalSize) * rowIndex + dataFileExport.Fields.Take(fieldIndex).Sum(f => f.TotalSize);

            return position + fieldPosition;
        }

        public static int GetFieldValueEndPosition(DataFileExport dataFileExport, int rowIndex, int fieldIndex)
        {
            int position = DbfHeaderStructure.StructureSize
                           + dataFileExport.Fields.Count * DbfFieldHeaderStructure.StructureSize
                           + DbfConstants.HeaderTerminatorSize
                           + (rowIndex + 1) * DbfDeletedFlags.StructureSize;

            int fieldPosition = dataFileExport.Fields.Sum(f => f.TotalSize) * rowIndex + dataFileExport.Fields.Take(fieldIndex).Sum(f => f.TotalSize);

            return position + fieldPosition + dataFileExport.Fields[fieldIndex].TotalSize;
        }

        public static bool AreEqualByteArrays(byte[] first, byte[] second)
        {
            if (first.Length != second.Length)
            {
                return false;
            }

            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool AreEqualByteArrays(byte[] first, byte[] second, int startInSecond)
        {
            byte[] secondPart = new byte[first.Length];
            Array.Copy(second, startInSecond, secondPart, 0, first.Length);
            return AreEqualByteArrays(first, secondPart);
        }
    }

}
