namespace Akizuki.ApplicationServices.Registry.Exceptions;

/// <summary>
/// 電子部品IDが登録されていない例外
/// </summary>
public class NotRegisteredComponentIdException : Exception
{
    public NotRegisteredComponentIdException() :
    base()
    { }
}
