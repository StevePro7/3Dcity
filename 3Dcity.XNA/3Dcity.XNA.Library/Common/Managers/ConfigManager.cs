using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Common.Data;

namespace WindowsGame.Common.Managers
{
	public interface IConfigManager 
	{
		void Initialize();
		void Initialize(String contentRoot);
		void LoadContent();

		GlobalConfigData GlobalConfigData { get; }
	}

	public class ConfigManager : IConfigManager 
	{
		private String configRoot;

		private const String CONFIG_DIRECTORY = "Config";
		private const String GLOBAL_CONFIG_FILENAME = "GlobalConfig.xml";

		public void Initialize()
		{
			String contentRoot = MyGame.Manager.ContentManager.ContentRoot;
			Initialize(contentRoot);
		}
		public void Initialize(String contentRoot)
		{
			configRoot = String.Format("{0}/{1}/{2}", contentRoot, Constants.DATA_DIRECTORY, CONFIG_DIRECTORY);
		}

		public void LoadContent()
		{
			String file = String.Format("{0}/{1}", configRoot, GLOBAL_CONFIG_FILENAME);
			GlobalConfigData = MyGame.Manager.FileManager.LoadXml<GlobalConfigData>(file);
		}

		public GlobalConfigData GlobalConfigData { get; private set; }

	}
}
