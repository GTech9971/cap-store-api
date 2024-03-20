using System.Reflection;
using System.Linq.Dynamic.Core;

namespace CapStore.Domains;

public class FilterSortService<T>
{
    public IQueryable<T> filter(IQueryable<T> source, string filterColumn, string filterQuery)
    {
        return IsValidProperty(filterColumn)
            ? source.Where($"{filterColumn}.ToString().StartsWith(@0)", filterQuery)
            : source;
    }

    public IQueryable<T> sort(IQueryable<T> source, string sortColumn, string sortOrder)
    {
        sortOrder = string.IsNullOrWhiteSpace(sortOrder) == false && sortOrder.ToUpper() == "ASC"
                        ? "ASC"
                        : "DESC";
        return IsValidProperty(sortColumn)
                ? source.OrderBy(string.Format("{0} {1}", sortColumn, sortOrder))
                : source;
    }

    /// <summary>
    /// Checks if the given property name exists
    /// to protect against SQL injection attacks
    /// </summary>
    private bool IsValidProperty(string propertyName,
                                bool throwExceptionIfNotFound = false)
    {
        var prop = typeof(T).GetProperty(
            propertyName,
            BindingFlags.IgnoreCase |
            BindingFlags.Public |
            BindingFlags.Static |
            BindingFlags.Instance);

        if (prop == null && throwExceptionIfNotFound)
        {
            throw new NotSupportedException($"ERROR: Property '{propertyName}' does not exist.");
        }

        return prop != null;
    }
}
