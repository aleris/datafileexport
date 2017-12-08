using System;
using System.Data;
using NUnit.Framework;
using TeamNet.Data.FileExport.DataSources;

namespace TeamNet.Data.FileExport.Tests
{
    [TestFixture]
    public class DataSetDataSourceTests
    {
        private DataSet _sourceDataSet;

        [SetUp]
        public void SetUp()
        {
            _sourceDataSet = new DataSet();
            
            DataTable t1 = _sourceDataSet.Tables.Add("T1");
            t1.Columns.Add("C1");
            t1.Rows.Add("V1_1");
            t1.Rows.Add("V1_2");
            
            DataTable t2 = _sourceDataSet.Tables.Add("T2");
            t2.Columns.Add("C2");
            t2.Rows.Add("V2");
        }

        [Test]
        public void RowsCountAreCorrect()
        {
            DataSetDataSource dataSource = new DataSetDataSource(_sourceDataSet);
            Assert.AreEqual(_sourceDataSet.Tables["T1"].Rows.Count, dataSource.RowCount);
        }

        [Test]
        public void DataValueForImplicitDataTableIsCorrect()
        {   
            DataSetDataSource dataSource = new DataSetDataSource(_sourceDataSet);
            Assert.AreEqual(_sourceDataSet.Tables["T1"].Rows[0]["C1"], dataSource.GetData(0, "C1"));
            Assert.AreEqual(_sourceDataSet.Tables["T1"].Rows[1]["C1"], dataSource.GetData(1, "C1"));
        }

        [Test]
        public void DataValueForNamedDataTableIsCorrect()
        {
            DataSetDataSource dataSource = new DataSetDataSource(_sourceDataSet);
            dataSource.MemberName = "T2";
            Assert.AreEqual(_sourceDataSet.Tables["T2"].Rows[0]["C2"], dataSource.GetData(0, "C2"));
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void WhenMemberNameIsNotValidShoudThrowInvalidOperationException()
        {
            DataSetDataSource dataSource = new DataSetDataSource(_sourceDataSet);
            dataSource.MemberName = "INVALID";
            dataSource.GetData(0, "C1");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void WhenDataSetIsNullShoudThrowArgumentNullException()
        {
            DataSetDataSource dataSource = new DataSetDataSource(null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WhenDataSetDoesNotContainTablesIsNullShoudThrowArgumentException()
        {
            DataSetDataSource dataSource = new DataSetDataSource(new DataSet());
        }
    }
}
