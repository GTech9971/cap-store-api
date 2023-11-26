using System;
using CapStore.Domain.Makers;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Components.Test
{
	[TestClass]
	public class MakerIdTest
	{

        private const string CATEGORY = "メーカーID";

        [TestMethod]
        [TestCategory(CATEGORY)]
        [ExpectedException(typeof(ValidationArgumentException))]
        [DataRow(-1)]
        public void TestExceptions(int id)
        {
            new MakerId(id);
        }

        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow(0)]
        [DataRow(101)]
        [DataRow(int.MaxValue)]
        public void TestSuccess(int id)
        {
            var makerId = new MakerId(id);
            Assert.AreEqual(id, makerId.Value);
        }
    }
}

