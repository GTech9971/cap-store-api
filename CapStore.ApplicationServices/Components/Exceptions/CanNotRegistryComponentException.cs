using System;
using CapStore.Domain.Components;

namespace CapStore.ApplicationServices.Components.Exceptions
{
	/// <summary>
	/// 電子部品登録時の例外
	/// </summary>
	public class CanNotRegistryComponentException : Exception
	{
		public CanNotRegistryComponentException(Component component, string message)
			: base($"{message}:{component}") { }
	}
}

