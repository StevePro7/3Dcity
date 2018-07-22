using System;

namespace WindowsGame.Common.Data
{
	public abstract class BaseData
	{
		public static void Initialize()
		{
			BaseRoot = String.Empty;
		}
		public static void Initialize(String root)
		{
			BaseRoot = root;
		}

		public static String BaseRoot { get; private set; }

	}
}