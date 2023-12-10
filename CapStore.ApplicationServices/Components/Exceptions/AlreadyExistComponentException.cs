using System;
using CapStore.Domain.Components;

namespace CapStore.ApplicationServices.Components.Exceptions
{
	/// <summary>
	/// 電子部品がすでに存在する例外
	/// </summary>
	public class AlreadyExistComponentException : Exception
	{
		public AlreadyExistComponentException(Component component, string message)
			:base($"{component}:{message}")
		{
		}
	}
}

