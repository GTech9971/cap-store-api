using CapStore.Domain.Makers;

namespace CapStore.ApplicationServices;

public class NotFoundMakerIdException : Exception
{
    public NotFoundMakerIdException(MakerId makerId) : base($"メーカーID:{makerId}が見つかりません") { }
}
