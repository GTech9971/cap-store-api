using CapStore.Domain.Categories;

namespace CapStore.ApplicationServices;

public class NotFoundCategoryIdException : Exception
{
    public NotFoundCategoryIdException(CategoryId categoryId) : base($"カテゴリーID:{categoryId}が見つかりません") { }
}
