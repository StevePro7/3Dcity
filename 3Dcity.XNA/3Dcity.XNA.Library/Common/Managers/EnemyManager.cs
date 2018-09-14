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
		void Reset(Byte theEnemySpawn, UInt16 frameDelay);
		void Spawn();
		void Update(GameTime gameTime);
		void Draw();

		IList<Enemy> EnemyList { get; }
		//IDictionary<Byte, Enemy> EnemyDict { get; }
	}

	public class EnemyManager : IEnemyManager 
	{
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

		public void Reset(Byte theEnemySpawn, UInt16 frameDelay)
		{
			maxEnemySpawn = theEnemySpawn;
			if (maxEnemySpawn > Constants.MAX_ENEMYS_SPAWN)
			{
				maxEnemySpawn = Constants.MAX_ENEMYS_SPAWN;
			}

			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyList[index].Reset(frameDelay);
			}
		}

		public void Spawn()
		{
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyList[index].Spawn();
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

		public IList<Enemy> EnemyList { get; private set; }
		//public IDictionary<Byte, Enemy> EnemyDict { get; private set; }

	}
}
