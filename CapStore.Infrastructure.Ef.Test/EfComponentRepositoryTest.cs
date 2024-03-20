using System;
using CapStore.Domains;
using CapStore.Domains.Categories;
using CapStore.Domains.Components;
using CapStore.Domains.Makers;
using CapStore.Domains.Shareds;
using CapStore.Infrastructure.Ef.Categories.Data;
using CapStore.Infrastructure.Ef.Components;
using CapStore.Infrastructure.Ef.Components.Data;
using CapStore.Infrastructure.Ef.Makers.Data;
using Xunit;

namespace CapStore.Infrastructure.Ef.Test
{
	[Collection("Ef Component")]
	public class EfComponentRepositoryTest : IClassFixture<BaseEfRepositoryTest>
	{

		private readonly CapStoreDbContext _context;

		public EfComponentRepositoryTest(BaseEfRepositoryTest fixture) : base()
		{
			_context = fixture._context;
		}

		private const string CATEGORY = "電子部品レポジトリEF";

		[Theory(DisplayName = "電子部品登録成功")]
		[Trait("Category", CATEGORY)]
		[InlineData("TestName", "TEST-NAME", "説明文", "https://akizukidenshi.com/img/goods/L/I-18237.jpg")]
		public async Task SaveSuccessTest(string name,
										  string modelName,
										  string description,
										  string imageUrl)
		{

			ComponentName componentName = new ComponentName(name);
			ComponentModelName componentModelName = new ComponentModelName(modelName);
			ComponentDescription componentDescription = new ComponentDescription(description);
			//category
			CategoryData? categoryData = _context.CategoryDatas.FirstOrDefault();
			Assert.NotNull(categoryData);
			Category category = categoryData.ToModel();

			//maker
			MakerData? makerData = _context.MakerDatas.FirstOrDefault();
			Assert.NotNull(makerData);
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


			FilterSortService<ComponentData> filterSortService = new FilterSortService<ComponentData>();

			IComponentRepository repository = new EfComponentRepository(_context, filterSortService);
			//登録テスト
			var registeredComponent = await repository.Save(component);

			//保存後の返却値テスト
			Assert.Equal(name, registeredComponent.Name.Value);
			Assert.False(registeredComponent.Id.IsUnDetect);

			//取得テスト
			Component? found = await repository.Fetch(component.Name);
			//登録ずみデータの確認
			Assert.NotNull(found);
			Assert.False(found.Id.IsUnDetect);
			Assert.Equal(name, found.Name.Value);
			Assert.Equal(modelName, found.ModelName.Value);
			Assert.Equal(description, found.Description.Value);
			//category
			Assert.Equal(category.Id.Value, found.Category.Id.Value);
			Assert.Equal(category.Name.Value, found.Category.Name.Value);
			Assert.Equal(category.Image?.Value, found.Category.Image?.Value);
			//maker
			Assert.Equal(maker.Id.Value, found.Maker.Id.Value);
			Assert.Equal(maker.Name.Value, found.Maker.Name.Value);
			Assert.Equal(maker.Image?.Value, found.Maker.Image?.Value);
			//images
			Assert.True(found.Images.Any());
			Assert.Equal(componentImage.Image.Value, found.Images.AsList().FirstOrDefault()!.Image.Value);

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
			Assert.Equal(found.Id.Value, updatedComponent.Id.Value);
			Assert.Equal(changeComponentName.Value, updatedComponent.Name.Value);
		}
	}
}