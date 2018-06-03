using System;
using System.Collections.Generic;
using TectC.Contracts;

namespace TestC.Services
{
	public class DataProvider: IDataProvider
	{
		private readonly List<string> _names = new List<string> { "Name1", "Name2", "Name3",
			"Name4", "Name5", "Name6", "Name7", "Name8", "Name9", "Name10" };

		private readonly List<string> _descriptions = new List<string> { "Description1",
			"Description2", "Description3", "Description4", "Description5",
			"Description6", "Description7", "Description8", "Description9",
			"Description10" };

		private readonly Random _random = new Random();

		public string GetName()
		{
			return _names[_random.Next(_names.Count)];
		}

		public string GetDescription()
		{
			return _descriptions[_random.Next(_descriptions.Count)];
		}
	}
}
