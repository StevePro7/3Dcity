using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IEnemyManager 
	{
		void Initialize();
		void LoadContent();
		void Reset(LevelType theLevelType, Byte theEnemySpawn, UInt16 minDelay, UInt16 maxDelay, Byte enemyTotal);
		void SpawnAllEnemies();
		void SpawnOneEnemy(Byte index);
		Boolean CheckThisEnemy(Byte index);
		Boolean CheckEnemiesNone();

		//void Spawn(UInt16 frameDelay, Vector2 position);
		void Update(GameTime gameTime);
		void Draw();

		IList<Enemy> EnemyList { get; }
		IList<Enemy> EnemyTest { get; }
		IDictionary<Byte, Enemy> EnemyDict { get; }

		IList<Rectangle> EnemyBounds { get; }
		UInt16[] EnemyOffsetX { get; }
		UInt16[] EnemyOffsetY { get; }
		
		UInt16 MinDelay { get; }
		UInt16 MaxDelay { get; }
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
		private Byte maxEnemySpawn;
		private UInt16 testFrameDelay;

		public void Initialize()
		{
			maxEnemySpawn = Constants.MAX_ENEMYS_SPAWN;

			EnemyList = new List<Enemy>(maxEnemySpawn);
			EnemyTest = new List<Enemy>(maxEnemySpawn);
			EnemyDict = new Dictionary<Byte, Enemy>(maxEnemySpawn);
			EnemyBounds = GetEnemyBounds();

			EnemyOffsetX = new UInt16[maxEnemySpawn];
			EnemyOffsetY = new UInt16[maxEnemySpawn];

			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyOffsetX[index] = (UInt16)(Constants.ENEMY_OFFSET_X[index] + Constants.GameOffsetX);
				EnemyOffsetY[index] = (UInt16)(Constants.ENEMY_OFFSET_Y[index] + Constants.GameOffsetY);

				Enemy enemy = new Enemy();
				enemy.Initialize(Constants.MAX_ENEMYS_FRAME);
				enemy.SetID(index);
				enemy.SetSlotID();
				EnemyList.Add(enemy);
			}
		}

		public void LoadContent()
		{
			for (Byte index = 0; index < Constants.MAX_ENEMYS_SPAWN; index++)
			{
				Enemy enemy = EnemyList[index];
				enemy.LoadContent(MyGame.Manager.ImageManager.EnemyRectangles);
			}

			// TODO enemy frame delay will be injected..
			testFrameDelay = MyGame.Manager.ConfigManager.GlobalConfigData.EnemysDelay;
		}

		public void Reset(LevelType theLevelType, Byte theEnemySpawn, UInt16 minDelay, UInt16 maxDelay, Byte enemyTotal)
		{
			levelType = theLevelType;
			maxEnemySpawn = theEnemySpawn;
			if (maxEnemySpawn > Constants.MAX_ENEMYS_SPAWN)
			{
				maxEnemySpawn = Constants.MAX_ENEMYS_SPAWN;
			}

			// Ensure max total takes precedence over spawn.
			if (maxEnemySpawn > enemyTotal)
			{
				maxEnemySpawn = enemyTotal;
			}

			MinDelay = minDelay;
			MaxDelay = maxDelay;

			// Reset all enemies but not the list as will clear.
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyList[index].Reset();
			}

			EnemyTest.Clear();
			EnemyDict.Clear();

			// TODO delete
			//// Ensure total at least the max.
			//if (enemyTotal < maxEnemySpawn)
			//{
			//    enemyTotal = maxEnemySpawn;
			//}

			EnemyStart = 0;
			EnemySpawn = 0;
			EnemyTotal = enemyTotal;
			EnemyTotalText = EnemyTotal.ToString().PadLeft(2, '0');
		}

		public void SpawnAllEnemies()
		{
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				SpawnOneEnemy(index);
				// TODO tweak configurable numbers

				//EnemyList[index].Start((UInt16)(testFrameDelay + 3* testFrameDelay * index));
				EnemyList[index].Start((UInt16)((index + 1) * testFrameDelay));		// stevepro - remove hard code start
			}
		}

		public void SpawnOneEnemy(Byte index)
		{
			// TODO work out better the frame delay.
			UInt16 frameDelay = testFrameDelay;//Constants.TestFrameDelay;
			//UInt16 frameDelay = MyGame.Manager.RandomManager.Next(MinDelay, MaxDelay);

			SByte slotID = Constants.INVALID_INDEX;
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
			MyGame.Manager.Logger.Info((slotID+1).ToString());

			Enemy enemy = EnemyList[index];

			Vector2 position = enemy.Position;
			Byte randomX = (Byte)MyGame.Manager.RandomManager.Next(Constants.ENEMY_RANDOM_X);
			Byte randomY = (Byte)MyGame.Manager.RandomManager.Next(Constants.ENEMY_RANDOM_Y);
			UInt16 offsetX = EnemyOffsetX[(Byte)slotID];
			UInt16 offsetY = EnemyOffsetY[(Byte)slotID];

			// TODO check this fits within [160,200] = [40,80] = [32,72] => [32 = 160-120-(2*4)
			// [32,72] => [32 = 160-120-(2*4), 72 = 200-120-(2*4)]
			position.X = randomX + offsetX + Constants.BorderSize;
			position.Y = randomY + offsetY + Constants.BorderSize;

			Rectangle bounds = EnemyBounds[(Byte)slotID];
			enemy.Spawn((Byte)slotID, frameDelay, position, bounds);
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
				EnemyStartText = EnemyStart.ToString().PadLeft(2, '0');
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

		public IList<Enemy> EnemyList { get; private set; }
		public IList<Enemy> EnemyTest { get; private set; }
		public IDictionary<Byte, Enemy> EnemyDict { get; private set; }
		public IList<Rectangle> EnemyBounds { get; private set; }
		public UInt16[] EnemyOffsetX { get; private set; }
		public UInt16[] EnemyOffsetY { get; private set; }
		public UInt16 MinDelay { get; private set; }
		public UInt16 MaxDelay { get; private set; }
		public Byte EnemySpawn { get; private set; }
		public Byte EnemyTotal { get; private set; }
		public Byte EnemyStart { get; private set; }
		public Single EnemyPercentage { get; private set; }
		public String EnemyTotalText { get; private set; }
		public String EnemyStartText { get; private set; }
	}
}
