using System;
using CapStore.Domain.Makers;

namespace CapStore.Infrastructure.Ef.Test
{
	[TestClass]
	public class EfMakerRepositoryTest : BaseEfRepositoryTest
	{

        private const string CATEGORY = "メーカーレポジトリEF";

        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow("Microchip Technology Inc")]
        [DataRow("村田製作所")]
        public async Task SaveSuccessTest(string name)
        {
            MakerName makerName = new MakerName(name);
            Maker maker = new Maker(MakerId.UnDetect(), makerName, null);

            using (var context = CreateContext())
            {
                IMakerRepository repository = new EfMakerRepository(context);
                //新規登録テスト
                Maker registredMaker = await repository.Save(maker);

                Maker? found = await repository.Fetch(makerName);
                Assert.IsNotNull(found);
                Assert.IsFalse(found.Id.IsUnDetect);

                Assert.AreEqual(name, registredMaker.Name.Value);
                Assert.IsFalse(registredMaker.Id.IsUnDetect);

                //更新テスト
                MakerName changeMakerName = new MakerName("change");
                Maker updateNameMaker = new Maker(registredMaker.Id, changeMakerName, null);
                Maker updatedMaker = await repository.Save(updateNameMaker);

                Assert.AreEqual(found.Id.Value, updatedMaker.Id.Value);
                Assert.AreEqual(changeMakerName.Value, updatedMaker.Name.Value);
            }
        }

    }
}

