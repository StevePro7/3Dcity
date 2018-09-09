using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface ICommandManager
	{
		void Initialize();
		void Initialize(String root);
		void LoadContent();

		IDictionary<Byte, IList<Single>> CommandTimeList { get; }
		IDictionary<Byte, IList<String>> CommandTypeList { get; }
		IDictionary<Byte, IList<String>> CommandArgsList { get; }
	}

	public class CommandManager : ICommandManager
	{
		private String commandRoot;

		public void Initialize()
		{
			Initialize(String.Empty);
		}
		public void Initialize(String root)
		{
			commandRoot = root;

			CommandTimeList = new Dictionary<Byte, IList<Single>>();
			CommandTypeList = new Dictionary<Byte, IList<String>>();
			CommandArgsList = new Dictionary<Byte, IList<String>>();
		}

		public void LoadContent()
		{
		}

		public IDictionary<Byte, IList<Single>> CommandTimeList { get; private set; }
		public IDictionary<Byte, IList<String>> CommandTypeList { get; private set; }
		public IDictionary<Byte, IList<String>> CommandArgsList { get; private set; }

	}
}
