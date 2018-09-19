using System;
using System.Collections.Generic;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface ILevelManager 
	{
		void Initialize();
		void Initialize(String root);
		void LoadContent();
		void SetLevelType(LevelType levelType);

		IList<String> LevelNames { get; }
		LevelType LevelType { get; }
		Byte LevelIndex { get; }
		String LevelName { get; }
	}

	public class LevelManager : ILevelManager 
	{
		private String levelRoot;

		private const String LEVELS_DIRECTORY = "Levels";
		private const String LEVELS_NAMESFILE = "LevelNames";

		public void Initialize()
		{
			// TODO get from storage...
			LevelType = LevelType.Easy;
			Initialize(String.Empty);
		}

		public void Initialize(String root)
		{
			levelRoot = String.Format("{0}{1}/{2}/{3}", root, Constants.CONTENT_DIRECTORY, Constants.DATA_DIRECTORY, LEVELS_DIRECTORY);
		}

		public void LoadContent()
		{
			String file = String.Format("{0}/{1}.txt", levelRoot, LEVELS_NAMESFILE);
			LevelNames = MyGame.Manager.FileManager.LoadTxt(file);
		}

		public void SetLevelType(LevelType levelType)
		{
			LevelType = levelType;
		}

		
		public IList<String> LevelNames { get; private set; }
		public LevelType LevelType { get; private set; }
		public Byte LevelIndex { get; private set; }
		public String LevelName { get; private set; }
	}
}
