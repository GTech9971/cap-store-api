using CapStore.Domains.Makers;

namespace CapStore.ApplicationServices.Makers.Exceptions;

public class NotFoundMakerIdException : Exception
{
    public NotFoundMakerIdException(MakerId makerId) : base($"メーカーID:{makerId}が見つかりません") { }
}
