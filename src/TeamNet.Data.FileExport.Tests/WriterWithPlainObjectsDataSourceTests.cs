using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TeamNet.Data.FileExport.DataSources;

namespace TeamNet.Data.FileExport.Tests
{
    [TestFixture]
    public class WriterWithPlainObjectsDataSourceTests
    {
        class TestItem
        {
            public string AString { get; set; }
            public decimal ANumeric { get; set; }
            public DateTime ADate { get; set; }
        }

        [Test]
        public void CanWriteWithPlainObjectsDataSource()
        {
            IList<TestItem> sourceList = new List<TestItem>()
            {
                new TestItem { AString = "a", ANumeric=1, ADate = new DateTime(2000, 10, 11)},
                new TestItem { AString = "b", ANumeric=2, ADate = new DateTime(2001, 11, 12)}
            };

            using (MemoryStream ms = new MemoryStream())
            {
                DataFileExport
                    .CreateDbf((IList)sourceList)
                    .AddTextField("AString", 10)
                    .AddNumericField("OtherName", 10, 0, "ANumeric")
                    .AddDateField("ADate")
                    .Write(ms);

                byte[] testData = ms.GetBuffer();

                Assert.IsTrue(testData.Length > 0);
            }
        }

        [Test]
        public void CanWriteWithEmptyPlainObjectsDataSource()
        {
            IList<TestItem> sourceList = new List<TestItem>();

            using (MemoryStream ms = new MemoryStream())
            {
                DataFileExport
                    .CreateDbf((IList)sourceList)
                    .AddTextField("AString", 10)
                    .AddNumericField("OtherName", 10, 0, "ANumeric")
                    .AddDateField("ADate")
                    .Write(ms);

                byte[] testData = ms.GetBuffer();

                Assert.IsTrue(testData.Length > 0);
            }
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void WhenPropertyNameInvalidShouldThrowException()
        {
            IList<TestItem> sourceList = new List<TestItem>()
            {
                new TestItem { AString = "a", ANumeric=1, ADate = new DateTime(2000, 10, 11)},
            };

            using (MemoryStream ms = new MemoryStream())
            {
                DataFileExport
                    .CreateDbf((IList)sourceList)
                    .AddTextField("AString_DOESNOTEXISTS", 10)
                    .Write(ms);
            }
        }
    }
}
