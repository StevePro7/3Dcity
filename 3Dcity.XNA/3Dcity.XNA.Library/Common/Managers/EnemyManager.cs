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
		void Reset(LevelType theLevelType, Byte theEnemySpawn, UInt16 minDelay, UInt16 maxDelay);
		void SpawnAllEnemies();
		void SpawnOneEnemy(Byte index);
		void Spawn(UInt16 frameDelay);
		void Start(UInt16 frameDelay);
		//void Spawn(UInt16 frameDelay, Vector2 position);
		void Update(GameTime gameTime);
		void Draw();

		IList<Enemy> EnemyList { get; }
		IDictionary<Byte, Enemy> EnemyDict { get; }
		IList<Rectangle> EnemyBounds { get; }
		UInt16[] EnemyOffsetX { get; }
		UInt16[] EnemyOffsetY { get; }
		UInt16 MinDelay { get; }
		UInt16 MaxDelay { get; }
	}

	public class EnemyManager : IEnemyManager
	{
		private LevelType levelType;
		private Byte maxEnemySpawn;

		public void Initialize()
		{
			maxEnemySpawn = Constants.MAX_ENEMYS_SPAWN;

			EnemyList = new List<Enemy>(maxEnemySpawn);
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
				//enemy.SetBounds(index);		//TODO delete
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
		}

		public void Reset(LevelType theLevelType, Byte theEnemySpawn, UInt16 minDelay, UInt16 maxDelay)
		{
			levelType = theLevelType;
			maxEnemySpawn = theEnemySpawn;

			MinDelay = minDelay;
			MaxDelay = maxDelay;
			if (maxEnemySpawn > Constants.MAX_ENEMYS_SPAWN)
			{
				maxEnemySpawn = Constants.MAX_ENEMYS_SPAWN;
			}

			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyList[index].Reset();
			}

			EnemyDict.Clear();
		}

		public void SpawnAllEnemies()
		{
		}

		public void SpawnOneEnemy(Byte index)
		{
			SByte slotID = Constants.INVALID_INDEX;
			while (true)
			{
				slotID = (SByte)MyGame.Manager.RandomManager.Next(maxEnemySpawn);
				if (!EnemyDict.ContainsKey((Byte)slotID))
				{
					break;
				}
			}

			Enemy enemy = EnemyList[index];
			Vector2 position = enemy.Position;
			Byte randomX = (Byte)MyGame.Manager.RandomManager.Next(40);
			Byte randomY = (Byte)MyGame.Manager.RandomManager.Next(80);
			UInt16 offsetX = EnemyOffsetX[(Byte)slotID];
			UInt16 offsetY = EnemyOffsetY[(Byte)slotID];

			position.X = randomX + offsetX;
			position.Y = randomY + offsetY;
			enemy.Spawn(0, position);
			EnemyDict.Add((Byte)slotID, enemy);
		}

		//public void Spawn(UInt16 frameDelay, Vector2 position)
		public void Spawn(UInt16 frameDelay)
		{
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				Vector2 position = GetPositioni(index);
				EnemyList[index].Spawn(frameDelay, position);
			}
		}

		public void Start(UInt16 frameDelay)
		{
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyList[index].Start(frameDelay);
			}
		}

		public void Update(GameTime gameTime)
		{
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyList[index].Update(gameTime);
			}
		}

		public void Draw()
		{
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyList[index].Draw();
			}
		}

		// TODO remove
		private Vector2 GetPositioni(Byte index)
		{
			Single x = 250;
			if (0 == index%2)
			{
				x = 450;
			}

			return new Vector2(x, 300);
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
			const Byte uppr = 5;

			if (index < uppr)
			{
				return new Rectangle((wide * index), 80 + Constants.GameOffsetY, (wide - size), (high - size));
			}
			else
			{
				index -= uppr;
				const Byte offset = 190;
				return new Rectangle(offset + wide * index, 280 + Constants.GameOffsetY, (wide - size), (high - size));
			}
		}

		public IList<Enemy> EnemyList { get; private set; }
		public IDictionary<Byte, Enemy> EnemyDict { get; private set; }
		public IList<Rectangle> EnemyBounds { get; private set; }
		public UInt16[] EnemyOffsetX { get; private set; }
		public UInt16[] EnemyOffsetY { get; private set; }
		public UInt16 MinDelay { get; private set; }
		public UInt16 MaxDelay { get; private set; }
	}
}
