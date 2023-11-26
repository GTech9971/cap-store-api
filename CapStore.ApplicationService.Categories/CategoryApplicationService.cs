using System;
using CapStore.ApplicationService.Categories.Data;
using CapStore.ApplicationService.Categories.Exceptions;
using CapStore.Domain.Categories;
using CapStore.Domain.Categories.Services;
using CapStore.Domain.Shareds;

namespace CapStore.ApplicationService.Categories
{
	/// <summary>
	/// カテゴリーのアプリケーションサービス
	/// </summary>
	public class CategoryApplicationService
	{

		private readonly ICategoryRepository _repository;
		private readonly CategoryService _service;

		public CategoryApplicationService(ICategoryRepository repository,
											CategoryService service)
		{
			_repository = repository;
			_service = service;
		}

		/// <summary>
		/// カテゴリーを登録する
		/// </summary>
		/// <param name="categoryName">カテゴリー名</param>
		/// <param name="imageUrl">カテゴリー画像</param>
		/// <returns></returns>
		/// <exception cref="CanNotRegistryCategoryException"></exception>
		public async Task<RegistryCategoryDataDto> Registry(CategoryName categoryName,
															ImageUrl imageUrl)
		{
			Category category = new Category(CategoryId.UnDetectId(), categoryName, imageUrl);

			if (await _service.Exists(category.Name))
			{
				throw new CanNotRegistryCategoryException(category, "カテゴリーがすでに存在するため登録できません");
			}

			Category registeredCategory = await _repository.Save(category);
			return new RegistryCategoryDataDto(registeredCategory);
		}
	}
}

