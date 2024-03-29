﻿using System;
using System.Text.RegularExpressions;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Akizukies.Catalogs
{
	/// <summary>
	/// カタログID(通販コード)
	/// </summary>
	public class CatalogId
	{
		public const int LENGTH = 6;
		private const string PATTERN = "^1\\d{5}$";

		private readonly string _catalogId;

		public CatalogId(string catalogId)
		{
			if (string.IsNullOrWhiteSpace(catalogId))
			{
				throw new ValidationArgumentNullException("カタログIDは必須です");
			}

			if (catalogId.Length != LENGTH)
			{
				throw new ValidationArgumentException($"カタログIDの桁数が{LENGTH}桁ではありません");
			}

			if (Regex.IsMatch(catalogId, PATTERN) == false)
			{
				throw new ValidationArgumentException("カタログIDのフォーマットが不正です");
			}

			_catalogId = catalogId;
		}

		/// <summary>
		/// カタログID(通販コード)
		/// </summary>
		public string Value => _catalogId;

	}
}

