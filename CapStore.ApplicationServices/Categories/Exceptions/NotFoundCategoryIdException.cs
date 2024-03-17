using CapStore.Domains.Categories;

namespace CapStore.ApplicationServices.Categories.Exceptions;

public class NotFoundCategoryIdException : Exception
{
    public NotFoundCategoryIdException(CategoryId categoryId) : base($"カテゴリーID:{categoryId}が見つかりません") { }
}
