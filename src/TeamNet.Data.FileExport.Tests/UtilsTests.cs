using System;
using NUnit.Framework;

namespace TeamNet.Data.FileExport.Tests
{
    [TestFixture]
    public class UtilsTests
	{
		#region GetFixedLengthString
		[Test]
        public void SmallStringIsPaddedRight()
        {
            const string test = "abc";
            const string expected = "abc___";
            string actual = Utils.GetFixedLengthString(test, expected.Length, '_', PaddingPositions.Right);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SmallStringIsPaddedLeft()
        {
            const string test = "abc";
            const string expected = "___abc";
            string actual = Utils.GetFixedLengthString(test, expected.Length, '_', PaddingPositions.Left);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MatchStringIsUnchangedWhenTryingToPadRight()
        {
            const string test = "abc";
            const string expected = "abc";
            string actual = Utils.GetFixedLengthString(test, expected.Length, '_', PaddingPositions.Right);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MatchStringIsUnchangedWhenTryingToPadLeft()
        {
            const string test = "abc";
            const string expected = "abc";
            string actual = Utils.GetFixedLengthString(test, expected.Length, '_', PaddingPositions.Left);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LongStringIsShrinkedWhenTryingToPadRight()
        {
            const string test = "abc";
            const string expected = "ab";
            string actual = Utils.GetFixedLengthString(test, expected.Length, '_', PaddingPositions.Right);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LongStringIsShrinkedWhenTryingToPadLeft()
        {
            const string test = "abc";
            const string expected = "ab";
            string actual = Utils.GetFixedLengthString(test, expected.Length, '_', PaddingPositions.Left);
            Assert.AreEqual(expected, actual);
        }

        [Test, ExpectedException(typeof(NotImplementedException))]
		public void GetFixedLengthString_WhenInvalidPaddingPositionShouldThrowException()
        {
            const PaddingPositions pos = (PaddingPositions)(-1);
            Utils.GetFixedLengthString("", 1, '_', pos);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
		public void GetFixedLengthString_WhenNullValueShouldThrowArgumentNullException()
        {
            Utils.GetFixedLengthString(null, 1, '_', PaddingPositions.Left);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
		public void GetFixedLengthString_WhenLengthLessThan0ShouldThrowArgumentException()
        {
            Utils.GetFixedLengthString("", -1, '_', PaddingPositions.Left);
		}
		#endregion

		#region GetMaximumLengthString
		[Test]
		public void LongStringIsShrinkedToMaximumLength()
		{
			const string test = "abc";
			const string expected = "ab";
			string actual = Utils.GetMaximumLengthString(test, expected.Length);
			Assert.AreEqual(expected, actual);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void GetMaximumLengthString_WhenNullValueShouldThrowArgumentNullException()
		{
			Utils.GetMaximumLengthString(null, 1);
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void GetMaximumLengthString_WhenLengthLessThan0ShouldThrowArgumentException()
		{
			Utils.GetMaximumLengthString("", -1);
		}
		#endregion
	}
}