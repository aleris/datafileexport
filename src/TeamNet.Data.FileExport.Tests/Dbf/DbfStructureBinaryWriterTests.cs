using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using TeamNet.Data.FileExport.Dbf;
using TeamNet.Data.FileExport.Dbf.Structures;

namespace TeamNet.Data.FileExport.Tests.Dbf
{
    [TestFixture]
    public class DbfStructureBinaryWriterTests
    {
        [Test]
        public void FieldStructureIsCorrectlyWritten()
        {
            DbfFieldHeaderStructure fieldHeaderStructure =
                new DbfFieldHeaderStructure
                {
                    FieldName = "A",
                    FieldType = 'B',
                    TotalSize = 1,
                    OffsetInRecord = 2,
                    DecimalPlaces = 3,
                    Reserved1 = 4,
                    WorkAreaId = 5,
                    MultiUser = 6,
                    SetField = 7,
                    Reserved21 = 8,
                    Reserver22 = 9,
                    Reserved23 = 10,
                    IncludeInMdx = 11
                };

            byte[] testData = new byte[32];
            using (MemoryStream ms = new MemoryStream(testData))
            {
                using(BinaryWriter br = new BinaryWriter(ms))
                {
                    DbfStructureBinaryWriter.Write(br, fieldHeaderStructure);
                }
            }
            /*
            Field descriptor array in dbf header (32 bytes for each field)
            ========================================

            Byte Size Contents Description                  Applies for (supported by)
            ----+----+--------+----------------------------+-----------------------------
            0     11   ASCI    field name, 0x00 termin.     all
            */
            Assert.AreEqual(Encoding.ASCII.GetBytes(fieldHeaderStructure.FieldName)[0], testData[0]);
            Assert.AreEqual(0, testData[1]);
            Assert.AreEqual(0, testData[2]);
            Assert.AreEqual(0, testData[3]);
            Assert.AreEqual(0, testData[4]);
            Assert.AreEqual(0, testData[5]);
            Assert.AreEqual(0, testData[6]);
            Assert.AreEqual(0, testData[7]);
            Assert.AreEqual(0, testData[8]);
            Assert.AreEqual(0, testData[9]);
            Assert.AreEqual(0, testData[10]);
            /*
            11     1   ASCI    field type  (see 2b)         all */
            Assert.AreEqual(Encoding.ASCII.GetBytes(new char[] { fieldHeaderStructure.FieldType })[0], testData[11]);
            /*
            12     4   n,n,n,n fld address in memory        D3
                       n,n,0,0 offset from record begin     Fp
                       0,0,0,0 ignored                      FS, D4, D5, Fb, CL*/
            Assert.AreEqual(fieldHeaderStructure.OffsetInRecord, testData[12]);
            Assert.AreEqual(0, testData[13]);
            Assert.AreEqual(0, testData[14]);
            Assert.AreEqual(0, testData[15]);
            /*
            16     1   byte    Field length, bin (see 2b)   all \ FS,CL: for C field type,*/
            Assert.AreEqual(fieldHeaderStructure.TotalSize, testData[16]);
            /*
            17     1   byte    decimal count, bin           all / both used for fld lng*/
            Assert.AreEqual(fieldHeaderStructure.DecimalPlaces, testData[17]);
            /*
            18     2   0,0     reserved                     all*/
            Assert.AreEqual(fieldHeaderStructure.Reserved1, testData[18]);
            Assert.AreEqual(0, testData[19]);
            /*
            20     1   byte    Work area ID                 D4, D5
                       0x00    unused                       FS, D3, Fb, Fp, CL*/
            Assert.AreEqual(fieldHeaderStructure.WorkAreaId, testData[20]);
            /*
            21     2   n,n     multi-user dBase             D3, D4, D5
                       0,0     ignored                      FS, Fb, Fp, CL*/
            Assert.AreEqual(fieldHeaderStructure.MultiUser, testData[21]);
            Assert.AreEqual(0, testData[22]);
            /*
            23     1   0x01    Set Fields                   D3, D4, D5
                       0x00    ignored                      FS, Fb, Fp, CL*/
            Assert.AreEqual(fieldHeaderStructure.SetField, testData[23]);
            /*
            24     7   0..0    reserved                     all*/
            Assert.AreEqual(fieldHeaderStructure.Reserved21, testData[24]);
            Assert.AreEqual(0, testData[25]);
            Assert.AreEqual(0, testData[26]);
            Assert.AreEqual(0, testData[27]);
            Assert.AreEqual(fieldHeaderStructure.Reserver22, testData[28]);
            Assert.AreEqual(0, testData[29]);
            Assert.AreEqual(fieldHeaderStructure.Reserved23, testData[30]);
            /*
            31     1   0x01    Field is in .mdx index       D4, D5
                       0x00    ignored                      FS, D3, Fb, Fp, CL*/
            Assert.AreEqual(fieldHeaderStructure.IncludeInMdx, testData[31]);
        }

        [Test]
        public void FileHeaderStructureWithoutFieldsHeaderIsCorrectlyWritten()
        {
            DateTime lastModifiedDate = new DateTime(1234, 12, 21);
            DbfHeaderStructure headerStructure = new DbfHeaderStructure
                                                 {
                                                     VersionNumber = 0x03,
                                                     LastUpdateYear = (byte) (lastModifiedDate.Year % 100),
                                                     LastUpdateMonth = (byte) lastModifiedDate.Month,
                                                     LastUpdateDay = (byte) lastModifiedDate.Day,
                                                     EncryptionFlag = 1,
                                                     HeaderSize = 2,
                                                     LanguageDriverId = 3,
                                                     MdxFlag = 4,
                                                     NumberOfRecords = 5,
                                                     RecordSize = 6,
                                                     Reserved1 = 7,
                                                     IncompleteTransaction = 8,
                                                     FreeRecordThreadReserved = 9,
                                                     ReservedMultiUser1 = 10,
                                                     ReservedMultiUser2 = 11,
                                                     Reserved2 = 12
                                                 };

            byte[] testData = new byte[32];
            using (MemoryStream ms = new MemoryStream(testData))
            {
                using (BinaryWriter br = new BinaryWriter(ms))
                {
                    DbfStructureBinaryWriter.Write(br, headerStructure);
                }
            }

            /*
            Byte Size Contents Description                  Applies for (supported by)
            ----+----+--------+----------------------------+-----------------------------
            00     1   0x03    plain .dbf                   FS, D3, D4, D5, Fb, Fp, CL
                       0x04    plain .dbf                   D4, D5  (FS)
                       0x05    plain .dbf                   D5, Fp  (FS)
                       0x43    with  .dbv memo var size     FS
                       0xB3    with  .dbv and .dbt memo     FS
                       0x83    with  .dbt memo              FS, D3, D4, D5, Fb, Fp, CL
                       0x8B    with  .dbt memo in D4 format D4, D5
                       0x8E    with  SQL table              D4, D5
                       0xF5    with  .fmp memo              Fp*/
            Assert.AreEqual(headerStructure.VersionNumber, testData[0]);

            /*
            01     3  YYMMDD   Last update digits           all*/
            Assert.AreEqual(headerStructure.LastUpdateYear, testData[1]);
            Assert.AreEqual(headerStructure.LastUpdateMonth, testData[2]);
            Assert.AreEqual(headerStructure.LastUpdateDay, testData[3]);

            /*
            04     4  ulong    Number of records in file    all*/
            Assert.AreEqual(headerStructure.NumberOfRecords, testData[4]);
            Assert.AreEqual(0, testData[5]);
            Assert.AreEqual(0, testData[6]);
            Assert.AreEqual(0, testData[7]);

            /*
            08     2  ushort   Header size in bytes         all*/
            Assert.AreEqual(headerStructure.HeaderSize, testData[8]);
            Assert.AreEqual(0, testData[9]);

            /*
            10     2  ushort   Record size in bytes         all*/
            Assert.AreEqual(headerStructure.RecordSize, testData[10]);
            Assert.AreEqual(0, testData[11]);

            /*
            12     2   0,0     Reserved                     all*/
            Assert.AreEqual(headerStructure.Reserved1, testData[12]);
            Assert.AreEqual(0, testData[13]);

            /*
            14     1   0x01    Begin transaction            D4, D5
                       0x00    End Transaction              D4, D5
                       0x00    ignored                      FS, D3, Fb, Fp, CL*/
            Assert.AreEqual(headerStructure.IncompleteTransaction, testData[14]);

            /*
            15     1   0x01    Encryptpted                  D4, D5
                       0x00    normal visible               all*/
            Assert.AreEqual(headerStructure.EncryptionFlag, testData[15]);

            /*
            16    12   0 (1)   multi-user environment use   D4,D5*/
            Assert.AreEqual(headerStructure.FreeRecordThreadReserved, testData[16]);
            Assert.AreEqual(0, testData[17]);
            Assert.AreEqual(0, testData[18]);
            Assert.AreEqual(0, testData[19]);
            Assert.AreEqual(headerStructure.ReservedMultiUser1, testData[20]);
            Assert.AreEqual(0, testData[21]);
            Assert.AreEqual(0, testData[22]);
            Assert.AreEqual(0, testData[23]);
            Assert.AreEqual(headerStructure.ReservedMultiUser2, testData[24]);
            Assert.AreEqual(0, testData[25]);
            Assert.AreEqual(0, testData[26]);
            Assert.AreEqual(0, testData[27]);

            /*
            28     1   0x01    production index exists      Fp, D4, D5
                       0x00    index upon demand            all*/
            Assert.AreEqual(headerStructure.MdxFlag, testData[28]);

            /*
            29     1   n       language driver ID           D4, D5
                       0x01    codepage  437 DOS USA        Fp
                       0x02    codepage  850 DOS Multi ling Fp
                       0x03    codepage 1251 Windows ANSI   Fp
                       0xC8    codepage 1250 Windows EE     Fp
                       0x00    ignored                      FS, D3, Fb, Fp, CL*/
            Assert.AreEqual(headerStructure.LanguageDriverId, testData[29]);

            /*
            30     2   0,0     reserved                     all*/
            Assert.AreEqual(headerStructure.Reserved2, testData[30]);
            Assert.AreEqual(0, testData[31]);

            // the following are writtern with IFieldWriter WriteHeader
            /*
            32    n*32         Field Descriptor, see (2a)   all
            +1     1   0x0D    Header Record Terminator     all
            */
        }
    }
}