using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IConfigManager 
	{
		void Initialize();
		void Initialize(String root);
		void LoadContent();
	}

	public class ConfigManager : IConfigManager 
	{
		public void Initialize()
		{
			Initialize(String.Empty);
		}
		public void Initialize(String root)
		{
		}

		public void LoadContent()
		{
		}

	}
}
