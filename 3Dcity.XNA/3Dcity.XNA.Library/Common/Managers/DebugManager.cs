using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IDebugManager 
	{
		void Initialize();
		void Reset(ScreenType screenType);
		void Draw();
	}

	public class DebugManager : IDebugManager
	{
		private Vector2[] boxPositions;

		public void Initialize()
		{
			boxPositions = GetBoxPositions();
		}

		public void Reset(ScreenType screenType)
		{
			if (!MyGame.Manager.ConfigManager.GlobalConfigData.DebugTester)
			{
				return;
			}
			if (MyGame.Manager.ConfigManager.GlobalConfigData.ScreenType != screenType)
			{
				return;
			}

			// Reset scores for testing scenario.
			MyGame.Manager.ScoreManager.ResetAll();

			// Reset levels for testing scenario.
			LevelType LevelType = MyGame.Manager.ConfigManager.GlobalConfigData.LevelType;
			Byte LevelNo = MyGame.Manager.ConfigManager.GlobalConfigData.LevelNo;
			//Byte LevelIndex = (Byte) (LevelNo - 1);		//	MyGame.Manager.ConfigManager.GlobalConfigData.LevelIndex;
			MyGame.Manager.LevelManager.SetLevelType(LevelType);
			//MyGame.Manager.LevelManager.SetLevelIndex(LevelIndex);
			MyGame.Manager.LevelManager.SetLevelNo(LevelNo);


			// Load level configuration data.
			Byte LevelIndex = (Byte) (LevelNo - 1);
			MyGame.Manager.LevelManager.LoadLevelConfigData(LevelType, LevelIndex);
			LevelConfigData LevelConfigData = MyGame.Manager.LevelManager.LevelConfigData;

			// Bullets.
			MyGame.Manager.BulletManager.Reset(LevelConfigData.BulletMaxim, LevelConfigData.BulletFrame, LevelConfigData.BulletShoot);

			// Enemies.
			//MyGame.Manager.EnemyManager.Reset(LevelType, LevelConfigData.EnemySpawn, 1000, 5000, LevelConfigData.EnemyTotal);
			MyGame.Manager.EnemyManager.Reset(LevelType, LevelConfigData);

			// Explosions.
			MyGame.Manager.ExplosionManager.Reset(LevelConfigData.EnemySpawn, LevelConfigData.ExplodeDelay);

			// TODO update this logic for god mode [global] vs. local cheat...!
			MyGame.Manager.StateManager.SetIsGodMode(MyGame.Manager.ConfigManager.GlobalConfigData.IsGodMode);
		}

		public void Draw()
		{
			//TODO delete
			//for (Byte index = 0; index < Constants.MAX_ENEMYS_SPAWN; index++)
			//{
			//    Engine.SpriteBatch.Draw(Assets.ZZindigoTexture, boxPositions[index], Color.Black);
			//}
		}

		private Vector2[] GetBoxPositions()
		{
			const Single hi = 80 + Constants.GameOffsetY;
			const Single lo = 280 + Constants.GameOffsetY;

			boxPositions = new Vector2[Constants.MAX_ENEMYS_SPAWN];
			boxPositions[0] = new Vector2(160 * 0, hi);
			boxPositions[1] = new Vector2(160 * 1, hi);
			boxPositions[2] = new Vector2(160 * 2, hi);
			boxPositions[3] = new Vector2(160 * 3, hi);
			boxPositions[4] = new Vector2(160 * 4, hi);

			const Byte offset = 190;
			boxPositions[5] = new Vector2(160 * 0 + offset, lo);
			boxPositions[6] = new Vector2(160 * 1 + offset, lo);
			boxPositions[7] = new Vector2(160 * 2 + offset, lo);

			return boxPositions;
		}
	}
}
