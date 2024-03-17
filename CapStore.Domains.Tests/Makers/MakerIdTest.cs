using System;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Makers.Test
{
    public class MakerIdTest
    {
        private const string CATEGORY = "メーカーID";

        [Theory]
        [Trait("Category", CATEGORY)]
        [InlineData(-1)]
        public void TestExceptions(int id)
        {
            Assert.Throws<ValidationArgumentException>(() =>
            {
                new MakerId(id);
            });

        }

        [Theory]
        [Trait("Category", CATEGORY)]
        [InlineData(0)]
        [InlineData(101)]
        [InlineData(int.MaxValue)]
        public void TestSuccess(int id)
        {
            var makerId = new MakerId(id);
            Assert.Equal(id, makerId.Value);
        }
    }
}

