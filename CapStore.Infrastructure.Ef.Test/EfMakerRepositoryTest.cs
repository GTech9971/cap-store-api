using System;
using CapStore.Domain.Makers;
using Xunit;

namespace CapStore.Infrastructure.Ef.Test
{
    public class EfMakerRepositoryTest : IClassFixture<BaseEfRepositoryTest>
    {
        private readonly CapStoreDbContext _context;

        public EfMakerRepositoryTest(BaseEfRepositoryTest fixture) : base()
        {
            _context = fixture._context;
        }

        private const string CATEGORY = "メーカーレポジトリEF";

        [Theory(DisplayName = "メーカー登録成功テスト")]
        [Trait("Category", CATEGORY)]
        [InlineData("Microchip Technology Inc")]
        [InlineData("村田製作所")]
        public async Task SaveSuccessTest(string name)
        {
            MakerName makerName = new MakerName(name);
            Maker maker = new Maker(MakerId.UnDetect(), makerName, null);

            IMakerRepository repository = new EfMakerRepository(_context);
            //新規登録テスト
            Maker registredMaker = await repository.Save(maker);

            Maker? found = await repository.Fetch(makerName);
            Assert.NotNull(found);
            Assert.False(found.Id.IsUnDetect);

            Assert.Equal(name, registredMaker.Name.Value);
            Assert.False(registredMaker.Id.IsUnDetect);

            //更新テスト
            MakerName changeMakerName = new MakerName("change");
            Maker updateNameMaker = new Maker(registredMaker.Id, changeMakerName, null);
            Maker updatedMaker = await repository.Save(updateNameMaker);

            Assert.Equal(found.Id.Value, updatedMaker.Id.Value);
            Assert.Equal(changeMakerName.Value, updatedMaker.Name.Value);
        }

    }
}

