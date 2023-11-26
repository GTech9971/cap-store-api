using System;
using CapStore.Domain.Categories;
using CapStore.Infrastructure.Categories.Ef;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace CapStore.Infrastructures.CategoriesEf.Test
{
	[TestClass]
	public class EfCategoryRepositoryTest
	{
        /// <summary>
        /// テスト用のPostgreSQL Dockerコンテナをコードベースで作成
        /// </summary>
		/// <see cref="https://dotnet.testcontainers.org/"/>
        /// <see cref="https://www.nuget.org/packages/Testcontainers.PostgreSql"/>
		/// <see cref="https://www.azureblue.io/asp-net-core-integration-tests-with-test-containers-and-postgres/"/>
        private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
			.WithImage("postgres")
			.WithDatabase("test_db")
			.WithUsername("test")
			.WithPassword("test")
			.WithCleanUp(true)
			.Build();
			

		private const string CATEGORY = "カテゴリーレポジトリEF";

		/// <summary>
		/// コンテナ起動と、DBの再作成
		/// </summary>
		/// <returns></returns>
		[TestInitialize]
		public async Task Setup()
		{
			//コンテナ起動
			await _container.StartAsync();
			//テーブル再作成
			using(var context =  CreateContext())
			{
				await context.Database.EnsureDeletedAsync();
				await context.Database.EnsureCreatedAsync();
			}
		}

        /// <summary>
        /// コンテナ終了
        /// </summary>
        /// <returns></returns>
        [TestCleanup]
        public async Task CleanUp()
        {
            await _container.DisposeAsync();
        }

        /// <summary>
        /// DbContext作成
        /// </summary>
        /// <returns></returns>
        private CategoryDbContext CreateContext()
        {
            return new CategoryDbContext(
            new DbContextOptionsBuilder<CategoryDbContext>()
                .UseNpgsql(_container.GetConnectionString())
                .Options);
        }

		

		[TestMethod]
		[TestCategory(CATEGORY)]
		[DataRow("マイコン")]
		[DataRow("抵抗")]
		public async Task SaveSuccessTest(string name)
		{
			CategoryName categoryName = new CategoryName(name);
			Category category = new Category(CategoryId.UnDetectId(), categoryName, null);

			using (var context = CreateContext())
			{
				ICategoryRepository repository = new EfCategoryRepository(context);
				Category registredCategory = await repository.Save(category);

				Category? found = await repository.Fetch(categoryName);
				Assert.IsNotNull(found);
				Assert.IsFalse(found.Id.IsUnDetect);

				Assert.AreEqual(name, registredCategory.Name.Value);
				Assert.IsFalse(registredCategory.Id.IsUnDetect);
			}
		}


	}
}

