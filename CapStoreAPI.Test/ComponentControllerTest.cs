﻿using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Text;
using System.Text.Json;
using System.Net;

namespace CapStoreAPI.Test
{
    /// <summary>
    /// 登録テスト
    /// </summary>
    public sealed class RegistryTest : IClassFixture<PostgreSqlTest>, IDisposable
    {
        private readonly WebApplicationFactory<Program> _webApplicationFactory;

        private readonly HttpClient _httpClient;

        public RegistryTest(PostgreSqlTest fixture)
        {
            var clientOptions = new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            };
            _webApplicationFactory = new CustomWebApplicationFactory(fixture);
            _httpClient = _webApplicationFactory.CreateClient(clientOptions);
        }

        public void Dispose()
        {
            _webApplicationFactory.Dispose();
        }


        [Fact(DisplayName = "電子部品登録")]
        [Trait("Controller", "Component")]
        public async Task RegistryNotFoundTest()
        {
            using StringContent jsonContent = new(
                               JsonSerializer.Serialize(new
                               {
                                   name = "PIC16F1827",
                                   modelName = "PIC16f1827",
                                   description = "PICの説明",
                                   categoryId = 1,
                                   makerId = 1
                               }),
                               Encoding.UTF8,
                               "application/json");

            using HttpResponseMessage response = await _httpClient.PostAsync(
                "/api/v1/components/",
                jsonContent);



            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "電子部品取得API")]
        [Trait("Controller", "Component")]
        public async Task FetchComponentsEmptyTest()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/components/");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }


        [Fact(DisplayName = "電子部品取得API 1件")]
        [Trait("Controller", "Component")]
        public async Task FetchComponentsOneSuccessTest()
        {
            //まずは1件登録
            string json = JsonSerializer.Serialize(new
            {
                name = "PIC16F1827",
                modelName = "PIC16f1827",
                description = "PICの説明",
                categoryId = 1,
                makerId = 1
            });

            using StringContent jsonContent = new(json, Encoding.UTF8, "application/json");
            using HttpResponseMessage registryResponse = await _httpClient.PostAsync("/api/v1/components/", jsonContent);


            //取得
            using HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/components/");

            // Assert
            Assert.Equal(HttpStatusCode.OK, registryResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }

}