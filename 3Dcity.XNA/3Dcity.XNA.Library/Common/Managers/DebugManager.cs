using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Managers
{
	public interface IDebugManager 
	{
		void Initialize();
		void Reset();
		void Draw();
	}

	public class DebugManager : IDebugManager
	{
		private Vector2[] boxPositions;

		public void Initialize()
		{
			boxPositions = GetBoxPositions();
		}

		public void Reset()
		{
			if (!MyGame.Manager.ConfigManager.GlobalConfigData.DebugTester)
			{
				return;
			}

			// Reset scores for testing scenario.
			MyGame.Manager.ScoreManager.Reset();

			// Reset levels for testing scenario.
			MyGame.Manager.LevelManager.SetLevelType(MyGame.Manager.ConfigManager.GlobalConfigData.LevelType);
			MyGame.Manager.LevelManager.SetLevelIndex(MyGame.Manager.ConfigManager.GlobalConfigData.LevelIndex);

			MyGame.Manager.StateManager.SetIsGodMode(MyGame.Manager.ConfigManager.GlobalConfigData.IsGodMode);

			LevelType levelType = MyGame.Manager.ConfigManager.GlobalConfigData.LevelType;
			Byte enemySpawn = MyGame.Manager.ConfigManager.GlobalConfigData.EnemySpawn;
			Byte enemyTotal = MyGame.Manager.ConfigManager.GlobalConfigData.EnemyTotal;
			MyGame.Manager.EnemyManager.Reset(levelType, enemySpawn, 1000, 5000, enemyTotal);
		}

		public void Draw()
		{
			for (Byte index = 0; index < Constants.MAX_ENEMYS_SPAWN; index++)
			{
				Engine.SpriteBatch.Draw(Assets.ZZindigoTexture, boxPositions[index], Color.Black);
			}
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
