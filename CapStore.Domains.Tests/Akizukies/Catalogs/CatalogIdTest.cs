using System;
using CapStore.Domains.Akizukies.Catalogs;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Tests.Akizukies.Catalogs
{
    public class CatalogIdTest
    {

        private const string CATEGORY = "カタログID";

        [Fact]
        [Trait("Category", CATEGORY)]
        public void TestNullExceptions()
        {
            Assert.Throws<ValidationArgumentNullException>(() =>
            {
                new CatalogId(null!);
            });
        }

        [Fact]
        [Trait("Category", CATEGORY)]
        public void TestWhiteSpaceExceptions()
        {
            Assert.Throws<ValidationArgumentNullException>(() =>
            {
                new CatalogId("");
            });
        }

        [Theory]
        [Trait("Category", CATEGORY)]
        [InlineData("aa")]
        [InlineData("ああ")]
        [InlineData("I-18167A")]
        [InlineData("gI-1816")]
        public void TestExceptions(string catalogId)
        {
            Assert.Throws<ValidationArgumentException>(() =>
            {
                new CatalogId(catalogId);
            });
        }

        [Theory]
        [Trait("Category", CATEGORY)]
        [InlineData("118167")]
        [InlineData("108173")]
        [InlineData("106437")]
        [InlineData("105294")]
        public void TestSuccess(string catalogId)
        {
            var catalog = new CatalogId(catalogId);
            Assert.Equal(catalogId, catalog.Value);
        }
    }
}

