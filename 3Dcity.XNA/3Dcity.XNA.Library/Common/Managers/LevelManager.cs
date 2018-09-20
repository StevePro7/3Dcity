using System;
using System.Collections.Generic;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface ILevelManager 
	{
		void Initialize();
		void Initialize(String root);
		void LoadContent();

		void SetLevelType(LevelType levelType);
		void SetLevelIndex(Byte levelIndex);
		void Draw();

		// Properties.
		Vector2[] LevelTextPositions { get; }
		IList<String> LevelNames { get; }
		Byte MaximLevel { get; }
		LevelType LevelType { get; }
		Byte LevelIndex { get; }
		String LevelDiff { get; }
		String LevelValu { get; }
		String LevelName { get; }
	}

	public class LevelManager : ILevelManager 
	{
		private String levelRoot;

		private const String LEVELS_DIRECTORY = "Levels";
		private const String LEVELS_NAMESFILE = "LevelNames";

		public void Initialize()
		{
			Initialize(String.Empty);
		}

		public void Initialize(String root)
		{
			levelRoot = String.Format("{0}{1}/{2}/{3}", root, Constants.CONTENT_DIRECTORY, Constants.DATA_DIRECTORY, LEVELS_DIRECTORY);
			LevelTextPositions = GetLevelTextPositions();
		}

		public void LoadContent()
		{
			String file = String.Format("{0}/{1}.txt", levelRoot, LEVELS_NAMESFILE);
			LevelNames = MyGame.Manager.FileManager.LoadTxt(file);

			MaximLevel = MyGame.Manager.ConfigManager.GlobalConfigData.MaximLevel;
			if (MaximLevel > LevelNames.Count)
			{
				MaximLevel = (Byte)LevelNames.Count;
			}

			// TODO get from storage...
			SetLevelIndex(0);
			SetLevelType(LevelType.Easy);
		}

		public void SetLevelType(LevelType levelType)
		{
			LevelType = levelType;
			LevelDiff = levelType.ToString().ToUpper();
		}
		
		public void SetLevelIndex(Byte levelIndex)
		{
			LevelIndex = levelIndex;
			LevelValu = String.Format("[{0}]", (levelIndex + 1).ToString().PadLeft(2, '0'));
			LevelName = LevelNames[levelIndex];
		}

		public void Draw()
		{
			MyGame.Manager.TextManager.DrawText(LevelDiff, LevelTextPositions[0]);
			MyGame.Manager.TextManager.DrawText(LevelValu, LevelTextPositions[1]);
			MyGame.Manager.TextManager.DrawText(LevelName, LevelTextPositions[2]);
		}

		private static Vector2[] GetLevelTextPositions()
		{
			Vector2[] positions = new Vector2[3];
			positions[0] = MyGame.Manager.TextManager.GetTextPosition(0, 9);
			positions[1] = MyGame.Manager.TextManager.GetTextPosition(0, 10);
			positions[2] = MyGame.Manager.TextManager.GetTextPosition(0, 11);
			return positions;
		}

		public Vector2[] LevelTextPositions { get; private set; }
		public IList<String> LevelNames { get; private set; }
		public Byte MaximLevel { get; private set; }
		public LevelType LevelType { get; private set; }
		public Byte LevelIndex { get; private set; }
		public String LevelDiff { get; private set; }
		public String LevelValu { get; private set; }
		public String LevelName { get; private set; }
	}
}
