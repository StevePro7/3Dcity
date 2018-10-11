using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Managers
{
	public interface IEnemyManager 
	{
		void Initialize();
		void LoadContent();
		void Reset(LevelType theLevelType, LevelConfigData theLevelConfigData);
		void SpawnAllEnemies();
		void SpawnOneEnemy(Byte index);
		Boolean CheckThisEnemy(Byte index);
		Boolean CheckEnemiesNone();
		void Update(GameTime gameTime);
		void Draw();
		void DrawProgress();

		IList<Enemy> EnemyList { get; }
		IList<Enemy> EnemyTest { get; }
		IDictionary<Byte, Enemy> EnemyDict { get; }

		IList<Rectangle> EnemyBounds { get; }
		UInt16[] EnemyOffsetX { get; }
		UInt16[] EnemyOffsetY { get; }

		Byte EnemySpawn { get; }
		Byte EnemyTotal { get; }
		Byte EnemyStart { get; }
		Single EnemyPercentage { get; }
		String EnemyTotalText { get; }
		String EnemyStartText { get; }
	}

	public class EnemyManager : IEnemyManager
	{
		private LevelType levelType;
		private LevelConfigData levelConfigData;
		private IDictionary<Byte, UInt16> enemyDelays;
		private Byte maxEnemySpawn;
		private Vector2[] progressPosition;

		public void Initialize()
		{
			maxEnemySpawn = Constants.MAX_ENEMYS_SPAWN;

			EnemyList = new List<Enemy>(maxEnemySpawn);
			EnemyTest = new List<Enemy>(maxEnemySpawn);
			EnemyDict = new Dictionary<Byte, Enemy>(maxEnemySpawn);
			EnemyBounds = GetEnemyBounds();

			EnemyOffsetX = new UInt16[maxEnemySpawn];
			EnemyOffsetY = new UInt16[maxEnemySpawn];

			Boolean enemyBlink = MyGame.Manager.ConfigManager.GlobalConfigData.EnemyBlink;
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyOffsetX[index] = (UInt16)(Constants.ENEMY_OFFSET_X[index] + Constants.GameOffsetX);
				EnemyOffsetY[index] = (UInt16)(Constants.ENEMY_OFFSET_Y[index] + Constants.GameOffsetY);

				Enemy enemy = new Enemy();
				enemy.Initialize(Constants.MAX_ENEMYS_FRAME);
				enemy.SetID(index);
				enemy.SetSlotID();
				enemy.SetBlinkd(enemyBlink);
				EnemyList.Add(enemy);
			}

			enemyDelays = new Dictionary<Byte, UInt16>(Constants.MAX_ENEMYS_TOTAL);
			progressPosition = new Vector2[3];
			progressPosition[0] = MyGame.Manager.TextManager.GetTextPosition(25, 23);
			progressPosition[1] = MyGame.Manager.TextManager.GetTextPosition(28, 23);
			progressPosition[2] = MyGame.Manager.TextManager.GetTextPosition(29, 23);
		}

		public void LoadContent()
		{
			for (Byte index = 0; index < Constants.MAX_ENEMYS_SPAWN; index++)
			{
				Enemy enemy = EnemyList[index];
				enemy.LoadContent(MyGame.Manager.ImageManager.EnemyRectangles);
			}
		}

		public void Reset(LevelType theLevelType, LevelConfigData theLevelConfigData)
		{
			levelType = theLevelType;
			levelConfigData = theLevelConfigData;
			maxEnemySpawn = levelConfigData.EnemySpawn;
			if (maxEnemySpawn > Constants.MAX_ENEMYS_SPAWN)
			{
				maxEnemySpawn = Constants.MAX_ENEMYS_SPAWN;
			}

			// Ensure max total takes precedence over spawn.
			if (maxEnemySpawn > levelConfigData.EnemyTotal)
			{
				maxEnemySpawn = levelConfigData.EnemyTotal;
			}

			// Reset all enemies but not the list as will clear.
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyList[index].Reset();
			}

			EnemyTest.Clear();
			EnemyDict.Clear();
			enemyDelays.Clear();

			EnemyStart = 0;
			EnemySpawn = 0;
			EnemyTotal = levelConfigData.EnemyTotal;
			EnemyStartText = EnemyStart.ToString().PadLeft(3, '0');
			EnemyTotalText = EnemyTotal.ToString().PadLeft(3, '0');

			// Validate enemy frame proportions add to 100%
			if (100 == levelConfigData.EnemySpeedNone + levelConfigData.EnemySpeedWave + levelConfigData.EnemySpeedFast)
			{
				return;
			}

			//TODO Maybe have a better validation algorithm if totals > 100%
			if (levelConfigData.EnemySpeedWave > 100)
			{
				levelConfigData.EnemySpeedWave = 50;
			}
			if (levelConfigData.EnemySpeedFast > 100)
			{
				levelConfigData.EnemySpeedFast /= 50;
			}
			// Otherwise halve the values and subtract from 100%.
			levelConfigData.EnemySpeedNone = (Byte)(100 - (levelConfigData.EnemySpeedWave + levelConfigData.EnemySpeedFast));
		}

		public void SpawnAllEnemies()
		{
			// Calculate frame delays for all enemy ships.
			MyGame.Manager.DelayManager.ResetEnemyDelays(enemyDelays, levelConfigData, EnemyTotal);
			MyGame.Manager.DelayManager.CalcdEnemyDelays(enemyDelays, levelConfigData, EnemyTotal);

			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				SpawnOneEnemy(index);
			}

			const Byte first = 0;
			UInt16 startDelay = MyGame.Manager.DelayManager.GetStartDelay(first, levelConfigData.EnemyStartDelay, levelConfigData.EnemyStartDelta);
			UInt16 totalDelay = MyGame.Manager.DelayManager.GetTotalDelay(EnemyList[first].FrameDelay);
			UInt16 parseDelay = (UInt16) (totalDelay / maxEnemySpawn);

			// Displace evenly spawned ships from start delay...
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyList[index].Start((UInt16)(index * parseDelay + startDelay));
			}
		}

		public void SpawnOneEnemy(Byte index)
		{
			UInt16 frameDelay = enemyDelays[EnemySpawn];
			SByte slotID;
			while (true)
			{
				slotID = (SByte)MyGame.Manager.RandomManager.Next(Constants.MAX_ENEMYS_SPAWN);
				if (!EnemyDict.ContainsKey((Byte)slotID))
				{
					break;
				}
			}

			// TODO delete
			//slotID = 0;		// hard code slotID to test.
			//MyGame.Manager.Logger.Info((slotID+1).ToString());

			Enemy enemy = EnemyList[index];

			Vector2 position = enemy.Position;
			Byte randomX = (Byte)MyGame.Manager.RandomManager.Next(Constants.ENEMY_RANDOM_X);
			Byte randomY = (Byte)MyGame.Manager.RandomManager.Next(Constants.ENEMY_RANDOM_Y);
			UInt16 offsetX = EnemyOffsetX[(Byte)slotID];
			UInt16 offsetY = EnemyOffsetY[(Byte)slotID];

			// TODO check this fits within [160,200] = [40,80] = [32,72] => [32 = 160-120-(2*4)]
			// [32,72] => [32 = 160-120-(2*4), 72 = 200-120-(2*4)]
			position.X = randomX + offsetX + Constants.BorderSize;
			position.Y = randomY + offsetY + Constants.BorderSize;

			Rectangle bounds = EnemyBounds[(Byte)slotID];
			enemy.Spawn((Byte)slotID, frameDelay, position, bounds, levelType);
			EnemyDict.Add((Byte)slotID, enemy);

			EnemySpawn++;
		}

		public Boolean CheckThisEnemy(Byte index)
		{
			Boolean check = false;

			Enemy enemy = EnemyList[index];
			if (EnemyType.Idle == enemy.EnemyType)
			{
				return false;
			}

			SByte slotID = enemy.SlotID;
			if (EnemyDict.ContainsKey((Byte)slotID))
			{
				EnemyDict.Remove((Byte)slotID);
			}

			enemy.Reset();

			// Check this is last enemy!!
			if (EnemySpawn >= EnemyTotal)
			{
				enemy.None();
				check = true;
			}

			return check;
		}

		public Boolean CheckEnemiesNone()
		{
			// Assume no more to spawn.
			Boolean test = true;
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				Enemy enemy = EnemyList[index];
				if (EnemyType.None != enemy.EnemyType)
				{
					test = false;
					break;
				}
			}

			return test;
		}

		public void Update(GameTime gameTime)
		{
			Boolean launchCheck = false;
			EnemyTest.Clear();
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				Enemy enemy = EnemyList[index];
				enemy.Update(gameTime);

				if (enemy.EnemyLaunch)
				{
					launchCheck = true;
					EnemyStart++;
					enemy.ResetLaunch();
				}
				if (EnemyType.Test == enemy.EnemyType)
				{
					EnemyTest.Add(enemy);
				}
			}

			if (launchCheck)
			{
				EnemyStartText = EnemyStart.ToString().PadLeft(3, '0');
				EnemyPercentage = ((Single)EnemyStart / (Single)EnemyTotal) * 100.0f;
			}
		}

		public void Draw()
		{
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyList[index].Draw();
			}
		}

		public void DrawProgress()
		{
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, EnemyStartText, progressPosition[0], Color.White);
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, Globalize.SEPARATOR, progressPosition[1], Color.White);
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, EnemyTotalText, progressPosition[2], Color.White);
		}

		private static IList<Rectangle> GetEnemyBounds()
		{
			IList<Rectangle> enemyBounds = new List<Rectangle>(Constants.MAX_ENEMYS_SPAWN);
			for (Byte index = 0; index < Constants.MAX_ENEMYS_SPAWN; index++)
			{
				Rectangle bounds = GetEnemyBound(index);
				enemyBounds.Add(bounds);
			}

			return enemyBounds;
		}
		private static Rectangle GetEnemyBound(Byte index)
		{
			// High + wide max enemy.
			const Byte size = 120;
			const Byte wide = 160;
			const Byte high = 200;

			const Byte inflate = 4;
			const Byte deflate = 8;

			UInt16 offsetY = 80;
			offsetY += Constants.GameOffsetY;

			const Byte uppr = Constants.BOTTOM_SECTOR;
			if (index < uppr)
			{
				return new Rectangle((wide * index) + inflate, offsetY + inflate, wide - size - deflate, high - size - deflate);
			}
			else
			{
				index -= uppr;
				offsetY = Constants.HALFWAY_DOWN;
				offsetY += Constants.GameOffsetY;
				const Byte offsetX = Constants.BOTTOM_OFFSET;
				return new Rectangle(offsetX + (wide * index) + inflate, offsetY + inflate, wide - size - deflate, high - size - deflate);
			}
		}


		//private static UInt16 GetStartFrameDelay(Byte index, UInt16 enemyStartDelay, UInt16 enemyStartDelta)
		//{
		//    UInt16 delay = (UInt16)((index + 1) * enemyStartDelay);
		//    UInt16 delta = (UInt16)MyGame.Manager.RandomManager.Next(enemyStartDelta);

		//    return (UInt16)(delay + delta);
		//}


		public IList<Enemy> EnemyList { get; private set; }
		public IList<Enemy> EnemyTest { get; private set; }
		public IDictionary<Byte, Enemy> EnemyDict { get; private set; }
		public IList<Rectangle> EnemyBounds { get; private set; }
		public UInt16[] EnemyOffsetX { get; private set; }
		public UInt16[] EnemyOffsetY { get; private set; }

		public Byte EnemySpawn { get; private set; }
		public Byte EnemyTotal { get; private set; }
		public Byte EnemyStart { get; private set; }
		public Single EnemyPercentage { get; private set; }
		public String EnemyTotalText { get; private set; }
		public String EnemyStartText { get; private set; }
	}
}
