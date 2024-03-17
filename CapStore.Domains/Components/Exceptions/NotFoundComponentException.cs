namespace CapStore.Domains.Components;

/// <summary>
/// 電子部品が見つからなかった際の例外                                          
/// </summary>
public class NotFoundComponentException : Exception
{
    public NotFoundComponentException(ComponentId componentId)
        : base($"電子部品が見つかりませんでした ID:{componentId.Value}") { }

    public NotFoundComponentException(ComponentName componentName)
    : base($"電子部品が見つかりませんでした 部品名:{componentName.Value}") { }
}
