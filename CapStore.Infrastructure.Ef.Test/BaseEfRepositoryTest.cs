using System;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace CapStore.Infrastructure.Ef.Test
{
	public abstract class BaseEfRepositoryTest : DbContext
	{
        protected readonly PostgreSqlContainer _container;

        public BaseEfRepositoryTest()
		{
            _container = new EfPostgreSqlContainer().CONTAINER;
		}

        /// <summary>
        /// コンテナ起動と、DBの再作成
        /// </summary>
        /// <returns></returns>
        [TestInitialize]
        public virtual async Task Setup()
        {
            //コンテナ起動
            await _container.StartAsync();
            //テーブル再作成
            using (var context = CreateContext())
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
        protected CapStoreDbContext CreateContext()
        {
            return new CapStoreDbContext(
            new DbContextOptionsBuilder<CapStoreDbContext>()
                .UseNpgsql(_container.GetConnectionString())
                .Options);
        }
    }
}

