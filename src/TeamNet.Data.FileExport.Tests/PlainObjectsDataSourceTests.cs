using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TeamNet.Data.FileExport.DataSources;

namespace TeamNet.Data.FileExport.Tests
{
    [TestFixture]
    public class PlainObjectsDataSourceTests
    {
        class TestItem
        {
            public string C1 { get; set; }
            public string C2 { get; set; }
        }

        private IList<TestItem> _sourceList;

        [SetUp]
        public void SetUp()
        {
            _sourceList = new List<TestItem>();
            _sourceList.Add(new TestItem { C1 = "a", C2 = "b" });
            _sourceList.Add(new TestItem { C1 = "c", C2 = "d" });
        }

        [Test]
        public void RowsCountAreCorrect()
        {
            PlainObjectsDataSource dataSource = new PlainObjectsDataSource((IList)_sourceList);
            Assert.AreEqual(_sourceList.Count, dataSource.RowCount);
        }

        [Test]
        public void DataValueIsCorrect()
        {
            PlainObjectsDataSource dataSource = new PlainObjectsDataSource((IList)_sourceList);
            Assert.AreEqual(_sourceList[0].C1, dataSource.GetData(0, "C1"));
            Assert.AreEqual(_sourceList[0].C2, dataSource.GetData(0, "C2"));
        }
    }
}
