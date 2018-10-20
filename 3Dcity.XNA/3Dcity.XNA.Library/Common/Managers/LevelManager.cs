using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Managers
{
	public interface ILevelManager 
	{
		void Initialize();
		void Initialize(String root);
		void LoadContent();
		void LoadLevelConfigData(LevelType levelType, Byte levelIndex);

		void SetLevelType(LevelType levelType);
		void SetLevelIndex(Byte levelIndex);
		void IncrementLevel();

		//void Draw();							// TODO delete	
		//void DrawLevelRoman();					// TODO delete
		void Draw();
		void DrawTextData();

		// Properties.
		//Vector2[] LevelTextPositions { get; }		// TODO delete
		IList<String> LevelNames { get; }
		//IList<String> LevelRoman { get; }			// TODO delete
		LevelConfigData LevelConfigData { get; }
		Byte MaximLevel { get; }
		LevelType LevelType { get; }
		Byte LevelIndex { get; }
		//String LevelDiff { get; }					// TODO delete
		String LevelValu { get; }
		String LevelName { get; }
	}

	public class LevelManager : ILevelManager 
	{
		private String levelRoot;

		private String levelDiff;
		//private String levelText;				// TODO delete
		private String levelData;
		private Vector2 levelRomanPosition;
		private Vector2 levelNumPosition;
		private Vector2 levelOrbPosition;
		private Rectangle levelOrbPbRectangle;
		//private Rectangle diffOrbRectangle;	// TODO delete

		private const String LEVELS_DIRECTORY = "Levels";
		private const String LEVELS_NAMESFILE = "LevelNames";
		private const String LEVELS_ROMANFILE = "LevelRoman";

		public void Initialize()
		{
			Initialize(String.Empty);
		}

		public void Initialize(String root)
		{
			levelRoot = String.Format("{0}{1}/{2}/{3}", root, Constants.CONTENT_DIRECTORY, Constants.DATA_DIRECTORY, LEVELS_DIRECTORY);
			levelRomanPosition = MyGame.Manager.TextManager.GetTextPosition(0, 11);
			//LevelTextPositions = GetLevelTextPositions();

			const Byte offset = 48;
			levelOrbPosition = new Vector2(Constants.ScreenWide - offset - Constants.GameOffsetX - 4, Constants.ScreenHigh - offset - Constants.GameOffsetY);
			levelNumPosition = MyGame.Manager.TextManager.GetTextPosition(0, 23);
		}

		public void LoadContent()
		{
			String namesFile = String.Format("{0}/{1}.txt", levelRoot, LEVELS_NAMESFILE);
			LevelNames = MyGame.Manager.FileManager.LoadTxt(namesFile);

			// TODO delete
			//String romanFile = String.Format("{0}/{1}.txt", levelRoot, LEVELS_ROMANFILE);
			//LevelRoman = MyGame.Manager.FileManager.LoadTxt(romanFile);
			//LevelType = MyGame.Manager.ConfigManager.GlobalConfigData.LevelType;
			//LevelIndex = MyGame.Manager.ConfigManager.GlobalConfigData.LevelIndex;
			// TODO delete

			MaximLevel = MyGame.Manager.ConfigManager.GlobalConfigData.MaximLevel;
			if (MaximLevel > LevelNames.Count)
			{
				MaximLevel = (Byte)LevelNames.Count;
			}
		}

		public void LoadLevelConfigData(LevelType levelType, Byte levelIndex)
		{
			String levelValue = (levelIndex + 1).ToString().PadLeft(2, '0');
			String file = String.Format("{0}/{1}/{2}-{1}.xml", levelRoot, levelType, levelValue);
			LevelConfigData = MyGame.Manager.FileManager.LoadXml<LevelConfigData>(file);
		}

		public void SetLevelType(LevelType levelType)
		{
			LevelType = levelType;
			levelDiff = levelType.ToString().ToUpper();

			// TODO refactor?  Into 2x methods and put this in second?
			if (null == MyGame.Manager.ImageManager.OrbDiffRectangles)
			{
				return;
			}

			levelOrbPbRectangle = MyGame.Manager.ImageManager.OrbDiffRectangles[(Byte)LevelType];
		}
		
		public void SetLevelIndex(Byte levelIndex)
		{
			LevelIndex = levelIndex;

			levelData = (levelIndex + 1).ToString().PadLeft(2, '0');
			LevelValu = String.Format("[{0}]", levelData);

			if (null == LevelNames)
			{
				return;
			}
			LevelName = LevelNames[levelIndex];
		}

		public void IncrementLevel()
		{
			Byte levelIndex = (Byte) (LevelIndex + 1);
			if (levelIndex >= MaximLevel)
			{
				return;
			}

			SetLevelIndex(levelIndex);
		}

		// TODO delete
		//public void Draw()
		//{
		//    MyGame.Manager.TextManager.DrawText(LevelDiff, LevelTextPositions[0]);
		//    MyGame.Manager.TextManager.DrawText(LevelValu, LevelTextPositions[1]);
		//    MyGame.Manager.TextManager.DrawText(LevelName, LevelTextPositions[2]);
		//}

		// TODO delete
		//public void DrawLevelRoman()
		//{
		//    Engine.SpriteBatch.DrawString(Assets.EmulogicFont, levelText, levelRomanPosition, Color.White);
		//}
		public void Draw()
		{
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, levelOrbPosition, levelOrbPbRectangle, Color.White);
		}

		public void DrawTextData()
		{
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, levelData, levelNumPosition, Color.White);
		}

		// TODO delete
		//private static Vector2[] GetLevelTextPositions()
		//{
		//    Vector2[] positions = new Vector2[3];
		//    positions[0] = MyGame.Manager.TextManager.GetTextPosition(0, 9);
		//    positions[1] = MyGame.Manager.TextManager.GetTextPosition(0, 10);
		//    positions[2] = MyGame.Manager.TextManager.GetTextPosition(0, 11);
		//    return positions;
		//}

		public Vector2[] LevelTextPositions { get; private set; }
		public IList<String> LevelNames { get; private set; }
		//public IList<String> LevelRoman { get; private set; }		// TODO delete
		public LevelConfigData LevelConfigData { get; private set; }
		public Byte MaximLevel { get; private set; }
		public LevelType LevelType { get; private set; }
		public Byte LevelIndex { get; private set; }
		//public String LevelDiff { get; private set; }		// TODO delete
		public String LevelValu { get; private set; }
		public String LevelName { get; private set; }
	}
}
