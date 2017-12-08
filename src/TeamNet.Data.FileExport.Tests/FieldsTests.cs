using System;
using NUnit.Framework;
using TeamNet.Data.FileExport.Dbf;
using TeamNet.Data.FileExport.Dbf.Structures;
using TeamNet.Data.FileExport.Fields;

namespace TeamNet.Data.FileExport.Tests
{
    [TestFixture]
    public class FieldsTests
    {
        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void WhenFieldNameIsNullShouldThrowArgumentNullException()
        {
            NumericField field = new NumericField(null, 2, 1, "");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void WhenFieldSourceNameIsNullShouldThrowArgumentNullException()
        {
            NumericField field = new NumericField("", 2, 1, null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WhenTotalSizeIsLessThanDecimalPlacesShouldThrowArgumentException()
        {
            NumericField field = new NumericField("", 1, 2, "");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WhenDecimalPlacesIsLessThanTotalSizeShouldThrowArgumentException()
        {
            NumericField field = new NumericField("", 1, 2, "");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WhenDecimalPlacesIsTooBigShouldThrowArgumentException()
        {
            NumericField field = new NumericField("", 255, (byte)(DbfConstants.FieldMaximumDecimals + 1), "");
        }

        [Test]
        public void NumericFieldShouldReturnNotNullFieldWriter()
        {
            NumericField field = new NumericField("", 2, 1, "");
            IFieldWriter writer = field.GetFieldWriter(new DbfDataFileExportFormatFactory());

            Assert.IsNotNull(writer);
        }

        [Test]
        public void CharacterFieldShouldReturnNotNullFieldWriter()
        {
            TextField field = new TextField("", 1);
            IFieldWriter writer = field.GetFieldWriter(new DbfDataFileExportFormatFactory());

            Assert.IsNotNull(writer);
        }

        [Test]
        public void DateFieldShouldReturnNotNullFieldWriter()
        {
            DateField field = new DateField("", "");
            IFieldWriter writer = field.GetFieldWriter(new DbfDataFileExportFormatFactory());

            Assert.IsNotNull(writer);
        }

        [Test]
        public void LogicalFieldShouldReturnNotNullFieldWriter()
        {
            BooleanField field = new BooleanField("", "");
            IFieldWriter writer = field.GetFieldWriter(new DbfDataFileExportFormatFactory());

            Assert.IsNotNull(writer);
        }

        [Test]
        public void FieldShouldReturnNotNullFieldWriter()
        {
            GenericField field = new GenericField("", "");
            IFieldWriter writer = field.GetFieldWriter(new DbfDataFileExportFormatFactory());

            Assert.IsNotNull(writer);
        }
    }
}
