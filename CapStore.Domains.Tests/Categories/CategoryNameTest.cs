using System;
using CapStore.Domains.Categories;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Components.Test
{
    public class CategoryNameTest
    {

        private const string CATEGORY = "カテゴリー名";


        [Fact]
        [Trait("Category", CATEGORY)]
        public void TestNullExceptions()
        {
            Assert.Throws<ValidationArgumentNullException>(() =>
            {
                new CategoryName(null!);
            });
        }

        [Fact]
        [Trait("Category", CATEGORY)]
        public void TestWhiteSpaceExceptions()
        {
            Assert.Throws<ValidationArgumentNullException>(() =>
            {
                new CategoryName("");
            });
        }

        [Theory]
        [Trait("Category", CATEGORY)]
        [InlineData("PIC")]
        [InlineData("半導体")]
        [InlineData("抵抗")]
        [InlineData("LED・抵抗")]
        public void TestSuccess(string name)
        {
            var categoryName = new CategoryName(name);
            Assert.Equal(name, categoryName.Value);
        }
    }
}

