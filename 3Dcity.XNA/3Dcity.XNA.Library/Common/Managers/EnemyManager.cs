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
		void Reset(LevelType theLevelType, Byte theEnemySpawn);
		void Spawn(UInt16 frameDelay);
		void Start(UInt16 frameDelay);
		//void Spawn(UInt16 frameDelay, Vector2 position);
		void Update(GameTime gameTime);
		void Draw();

		IList<Enemy> EnemyList { get; }
		//IDictionary<Byte, Enemy> EnemyDict { get; }
	}

	public class EnemyManager : IEnemyManager
	{
		private LevelType levelType;
		private Byte maxEnemySpawn;

		public void Initialize()
		{
			EnemyList = new List<Enemy>(Constants.MAX_ENEMYS_SPAWN);
			//EnemyDict = new Dictionary<Byte, Enemy>(Constants.MAX_ENEMYS_SPAWN);

			for (Byte index = 0; index < Constants.MAX_ENEMYS_SPAWN; index++)
			{
				Enemy enemy = new Enemy();
				enemy.Initialize(Constants.MAX_ENEMYS_FRAME);
				enemy.SetID(index);
				enemy.SetBounds(index);
				EnemyList.Add(enemy);
			}

			maxEnemySpawn = Constants.MAX_ENEMYS_SPAWN;
		}

		public void LoadContent()
		{
			for (Byte index = 0; index < Constants.MAX_ENEMYS_SPAWN; index++)
			{
				Enemy enemy = EnemyList[index];
				enemy.LoadContent(MyGame.Manager.ImageManager.EnemyRectangles);
			}
		}

		public void Reset(LevelType theLevelType, Byte theEnemySpawn)
		{
			levelType = theLevelType;
			maxEnemySpawn = theEnemySpawn;
			if (maxEnemySpawn > Constants.MAX_ENEMYS_SPAWN)
			{
				maxEnemySpawn = Constants.MAX_ENEMYS_SPAWN;
			}

			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyList[index].Reset();
			}
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

		private Vector2 GetPositioni(Byte index)
		{
			Single x = 250;
			if (0 == index%2)
			{
				x = 450;
			}

			return new Vector2(x, 300);
		}

		public IList<Enemy> EnemyList { get; private set; }
		//public IDictionary<Byte, Enemy> EnemyDict { get; private set; }

	}
}
