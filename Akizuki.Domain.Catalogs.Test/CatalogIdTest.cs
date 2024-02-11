using System;
using CapStore.Domain.Shareds.Exceptions;

namespace Akizuki.Domain.Catalogs.Test
{
    [TestClass]
    public class CatalogIdTest
    {

        private const string CATEGORY = "カタログID";

        [TestMethod]
        [TestCategory(CATEGORY)]
        [ExpectedException(typeof(ValidationArgumentNullException))]
        public void TestNullExceptions()
        {
            new CatalogId(null!);
        }

        [TestMethod]
        [TestCategory(CATEGORY)]
        [ExpectedException(typeof(ValidationArgumentNullException))]
        public void TestWhiteSpaceExceptions()
        {
            new CatalogId("");
        }

        [TestMethod]
        [TestCategory(CATEGORY)]
        [ExpectedException(typeof(ValidationArgumentException))]
        [DataRow("aa")]
        [DataRow("ああ")]
        [DataRow("I-18167A")]
        [DataRow("gI-1816")]
        public void TestExceptions(string catalogId)
        {
            new CatalogId(catalogId);
        }

        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow("118167")]
        [DataRow("108173")]
        [DataRow("106437")]
        [DataRow("105294")]
        public void TestSuccess(string catalogId)
        {
            var catalog = new CatalogId(catalogId);
            Assert.AreEqual(catalogId, catalog.Value);
        }
    }
}

