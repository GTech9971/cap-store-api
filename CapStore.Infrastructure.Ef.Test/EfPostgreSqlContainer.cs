using System;
using Testcontainers.PostgreSql;

namespace CapStore.Infrastructure.Ef.Test
{
	/// <summary>
	/// efテスト用のpostgreSQLのDBコンテナー
	/// </summary>
	public class EfPostgreSqlContainer
	{

        /// <summary>
        /// テスト用のPostgreSQL Dockerコンテナをコードベースで作成
        /// </summary>
        /// <see cref="https://dotnet.testcontainers.org/"/>
        /// <see cref="https://www.nuget.org/packages/Testcontainers.PostgreSql"/>
        /// <see cref="https://www.azureblue.io/asp-net-core-integration-tests-with-test-containers-and-postgres/"/>
        public readonly PostgreSqlContainer CONTAINER = new PostgreSqlBuilder()
			.WithImage("postgres")
			.WithDatabase("test_db")
			.WithUsername("test")
			.WithPassword("test")
			.WithCleanUp(true)
			.Build();

	}
}

