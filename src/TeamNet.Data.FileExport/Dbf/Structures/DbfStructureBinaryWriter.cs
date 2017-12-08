// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using System.IO;
using System.Text;

namespace TeamNet.Data.FileExport.Dbf.Structures
{
    static class DbfStructureBinaryWriter
    {
        public static void Write(BinaryWriter binaryWriter, DbfFieldHeaderStructure fieldHeaderStructure)
        {
            string fieldName = Utils.GetFixedLengthString(fieldHeaderStructure.FieldName, 
                                                          DbfConstants.FieldNameLength, 
                                                          DbfConstants.FieldNamePaddingChar, 
                                                          PaddingPositions.Right);
             binaryWriter.Write(Encoding.ASCII.GetBytes(fieldName));
             binaryWriter.Write(fieldHeaderStructure.FieldType);
             binaryWriter.Write(fieldHeaderStructure.OffsetInRecord);
             binaryWriter.Write(fieldHeaderStructure.TotalSize);
             binaryWriter.Write(fieldHeaderStructure.DecimalPlaces);
             binaryWriter.Write(fieldHeaderStructure.Reserved1);
             binaryWriter.Write(fieldHeaderStructure.WorkAreaId);
             binaryWriter.Write(fieldHeaderStructure.MultiUser);
             binaryWriter.Write(fieldHeaderStructure.SetField);
             binaryWriter.Write(fieldHeaderStructure.Reserved21);
             binaryWriter.Write(fieldHeaderStructure.Reserver22);
             binaryWriter.Write(fieldHeaderStructure.Reserved23);
             binaryWriter.Write(fieldHeaderStructure.IncludeInMdx);
        }

        public static void Write(BinaryWriter binaryWriter, DbfHeaderStructure headerStructure)
        {
             binaryWriter.Write(headerStructure.VersionNumber);
             binaryWriter.Write(headerStructure.LastUpdateYear);
             binaryWriter.Write(headerStructure.LastUpdateMonth);
             binaryWriter.Write(headerStructure.LastUpdateDay);
             binaryWriter.Write(headerStructure.NumberOfRecords);
             binaryWriter.Write(headerStructure.HeaderSize);
             binaryWriter.Write(headerStructure.RecordSize);
             binaryWriter.Write(headerStructure.Reserved1);
             binaryWriter.Write(headerStructure.IncompleteTransaction);
             binaryWriter.Write(headerStructure.EncryptionFlag);
             binaryWriter.Write(headerStructure.FreeRecordThreadReserved);
             binaryWriter.Write(headerStructure.ReservedMultiUser1);
             binaryWriter.Write(headerStructure.ReservedMultiUser2);
             binaryWriter.Write(headerStructure.MdxFlag);
             binaryWriter.Write(headerStructure.LanguageDriverId);
             binaryWriter.Write(headerStructure.Reserved2);
        }
    }
}