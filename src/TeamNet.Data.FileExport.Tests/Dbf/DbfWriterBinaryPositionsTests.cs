using System.Data;
using System.IO;
using System.Text;
using NUnit.Framework;
using TeamNet.Data.FileExport.DataSources;
using TeamNet.Data.FileExport.Dbf;
using TeamNet.Data.FileExport.Dbf.Structures;
using TeamNet.Data.FileExport.Fields;

namespace TeamNet.Data.FileExport.Tests.Dbf
{
    [TestFixture]
    public class DbfWriterBinaryPositionsTests
    {
        private const int SampleColumnsCount = 1;
        private const string SampleColumnName = "a";
        private const int SampleColumnLenght = 1;
        private const int SampleRowsCount = 1;
        private const string SampleStringData = "b";

        readonly byte[] _testData = new byte[DbfHeaderStructure.StructureSize
                                             + SampleColumnsCount * DbfFieldHeaderStructure.StructureSize
                                             + DbfConstants.HeaderTerminatorSize
                                             + SampleRowsCount * DbfDeletedFlags.StructureSize
                                             + SampleRowsCount * SampleStringData.Length
                                             + DbfConstants.RowsEndTerminatorSize];

        private IDataSource _dataSource;

        [SetUp]
        public void SetUp()
        {
            DataSet source = new DataSet();
            DataTable table = source.Tables.Add();

            for (int i = 0; i < SampleColumnsCount; i++)
            {
                table.Columns.Add(SampleColumnName, typeof(string));    
            }

            for (int i = 0; i < SampleRowsCount; i++)
            {
                table.Rows.Add(SampleStringData);                
            }

            _dataSource = new DataSetDataSource(source);

            DataFileExport dataFileExport = new DataFileExport(_dataSource, new DbfDataFileExportFormatFactory());

            dataFileExport.Fields.Add(new TextField(SampleColumnName, SampleColumnLenght));

            using (MemoryStream ms = new MemoryStream(_testData))
            {
                dataFileExport.Write(ms);
            }
        }

        [Test]
        public void WritesHeaderSeparatorAtCorrectPosition()
        {
            int testPosition = DbfHeaderStructure.StructureSize 
                               + SampleColumnsCount * DbfFieldHeaderStructure.StructureSize;
            
            Assert.AreEqual(DbfConstants.HeaderTerminator,
                            _testData[testPosition]);
        }

        [Test]
        public void WritesDeletedFlagAtCorrectPosition()
        {
            int testPosition = DbfHeaderStructure.StructureSize
                               + SampleColumnsCount * DbfFieldHeaderStructure.StructureSize
                               + DbfConstants.HeaderTerminatorSize;

            Assert.AreEqual(DbfDeletedFlags.Valid,
                            _testData[testPosition]);
        }

        [Test]
        public void WritesColumnValueAtCorrectPosition()
        {
            int testPosition = DbfHeaderStructure.StructureSize
                               + SampleColumnsCount * DbfFieldHeaderStructure.StructureSize
                               + DbfConstants.HeaderTerminatorSize
                               + DbfDeletedFlags.StructureSize;

            byte[] expectedData = Encoding.ASCII.GetBytes(SampleStringData);

            Assert.AreEqual(expectedData[0],
                            _testData[testPosition]);
        }

        [Test]
        public void WritesRowsEndSeparatorAtCorrectPosition()
        {
            int testPosition = DbfHeaderStructure.StructureSize
                               + SampleColumnsCount * DbfFieldHeaderStructure.StructureSize
                               + SampleRowsCount * DbfDeletedFlags.StructureSize
                               + SampleRowsCount * SampleStringData.Length
                               + DbfDeletedFlags.StructureSize;

            Assert.AreEqual(DbfConstants.RowsEndTerminator,
                            _testData[testPosition]);
        }
    }
}