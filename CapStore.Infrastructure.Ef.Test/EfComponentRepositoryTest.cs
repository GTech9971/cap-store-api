using System;
using CapStore.Domain.Categories;
using CapStore.Domain.Components;
using CapStore.Domain.Makers;
using CapStore.Domain.Shareds;
using CapStore.Infrastructure.Ef.Categories.Data;
using CapStore.Infrastructure.Ef.Components;
using CapStore.Infrastructure.Ef.Makers.Data;

namespace CapStore.Infrastructure.Ef.Test
{
	[TestClass]
	public class EfComponentRepositoryTest:BaseEfRepositoryTest
	{

		private const string CATEGORY = "電子部品レポジトリEF";

		[TestInitialize]
		public override async Task Setup()
		{
			await base.Setup();
			using (var context = CreateContext())
			{
				CategoryData categoryData = new CategoryData();
				categoryData.Name = "マイコン";

				await context.CategoryDatas.AddAsync(categoryData);

				MakerData makerData = new MakerData();
				makerData.Name = "microchip Inc";

				await context.MakerDatas.AddAsync(makerData);

				await context.SaveChangesAsync();
			}
		}

		[TestMethod]
		[TestCategory(CATEGORY)]
		[DataRow("TestName", "TEST-NAME", "説明文", "https://akizukidenshi.com/img/goods/L/I-18237.jpg")]
		public async Task SaveSuccessTest(string name,
										  string modelName,
										  string description,
										  string imageUrl)
		{
			using (var context = CreateContext())
			{
                ComponentName componentName = new ComponentName(name);
                ComponentModelName componentModelName = new ComponentModelName(modelName);
                ComponentDescription componentDescription = new ComponentDescription(description);
				//category
				CategoryData? categoryData = context.CategoryDatas.FirstOrDefault();
				Assert.IsNotNull(categoryData);
				Category category = categoryData.ToModel();

				//maker
				MakerData? makerData = context.MakerDatas.FirstOrDefault();
				Assert.IsNotNull(makerData);
				Maker maker = makerData.ToModel();
				//images
				ComponentImage componentImage = new ComponentImage(
					ComponentImageId.UnDetectId(),
					ComponentId.UnDetectId(),
					new ImageUrl(imageUrl));
				ComponentImageList imageList = new ComponentImageList(componentImage);

				//component
				Component component = new Component(
					ComponentId.UnDetectId(),
					componentName,
					componentModelName,
					componentDescription,
					category,
					maker,
					imageList);


                IComponentRepository repository = new EfComponentRepository(context);
				//登録テスト
				var registeredComponent = await repository.Save(component);

                //保存後の返却値テスト
                Assert.AreEqual(name, registeredComponent.Name.Value);
                Assert.IsFalse(registeredComponent.Id.IsUnDetect);

				//取得テスト
                Component? found = await repository.Fetch(component.Name);
				//登録ずみデータの確認
				Assert.IsNotNull(found);
				Assert.IsFalse(found.Id.IsUnDetect);
				Assert.AreEqual(name, found.Name.Value);
				Assert.AreEqual(modelName, found.ModelName.Value);
				Assert.AreEqual(description, found.Description.Value);
				//category
				Assert.AreEqual(category.Id.Value, found.Category.Id.Value);
				Assert.AreEqual(category.Name.Value, found.Category.Name.Value);
				Assert.AreEqual(category.Image?.Value, found.Category.Image?.Value);
				//maker
				Assert.AreEqual(maker.Id.Value, found.Maker.Id.Value);
				Assert.AreEqual(maker.Name.Value, found.Maker.Name.Value);
				Assert.AreEqual(maker.Image?.Value, found.Maker.Image?.Value);
				//images
				Assert.IsTrue(found.Images.Any());
				Assert.AreEqual(componentImage.Image.Value, found.Images.AsList().FirstOrDefault()!.Image.Value);

				//更新テスト
				ComponentName changeComponentName = new ComponentName("change");
				Component updateNameComponent = new Component(registeredComponent.Id,
															changeComponentName,
															registeredComponent.ModelName,
															registeredComponent.Description,
															registeredComponent.Category,
															registeredComponent.Maker,
															registeredComponent.Images);

				Component updatedComponent = await repository.Save(updateNameComponent);
				Assert.AreEqual(found.Id.Value, updatedComponent.Id.Value);
				Assert.AreEqual(changeComponentName.Value, updatedComponent.Name.Value);
			}
		}
	}
}

