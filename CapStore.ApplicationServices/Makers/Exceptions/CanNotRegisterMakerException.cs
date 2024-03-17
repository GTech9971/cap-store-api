using System;
using CapStore.Domains.Makers;
namespace CapStore.ApplicationServices.Makers.Exceptions
{
	/// <summary>
	/// メーカーが登録できない例外
	/// </summary>
	public class CanNotRegisterMakerException : Exception
	{
		public CanNotRegisterMakerException(Maker maker, string message)
			: base($"{message}:{maker.ToString()}")
		{
		}
	}
}

