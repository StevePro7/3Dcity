using System;
using System.Collections.Generic;
using WindowsGame.Common.Sprites;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IEnemyManager 
	{
		void Initialize();
		void LoadContent();
		void Reset(Byte theEnemySpawn);
		void Update(GameTime gameTime);
		void Draw();

		IList<Enemy> EnemyList { get; }
		IDictionary<Byte, Enemy> EnemyDict { get; }
	}

	public class EnemyManager : IEnemyManager 
	{
		private Byte maxEnemySpawn;

		public void Initialize()
		{
			EnemyList = new List<Enemy>(Constants.MAX_ENEMY_SPAWN);
			EnemyDict = new Dictionary<Byte, Enemy>(Constants.MAX_ENEMY_SPAWN);

			for (Byte index = 0; index < Constants.MAX_ENEMY_SPAWN; index++)
			{
				Enemy enemy = new Enemy();
				enemy.Initialize(Constants.MAX_ENEMY_FRAME);
				enemy.SetID(index);
				enemy.SetBounds(index);
				EnemyList.Add(enemy);

				EnemyDict[index] = null;
			}

			maxEnemySpawn = Constants.MAX_ENEMY_SPAWN;
		}

		public void LoadContent()
		{
			for (Byte index = 0; index < Constants.MAX_ENEMY_SPAWN; index++)
			{
				Enemy enemy = EnemyList[index];
				enemy.LoadContent(MyGame.Manager.ImageManager.EnemyRectangles);
			}
		}

		public void Reset(Byte theEnemySpawn)
		{
			maxEnemySpawn = theEnemySpawn;
			if (maxEnemySpawn > Constants.MAX_ENEMY_SPAWN)
			{
				maxEnemySpawn = Constants.MAX_ENEMY_SPAWN;
			}
		}

		public void Update(GameTime gameTime)
		{
		}

		public void Draw()
		{
		}

		public IList<Enemy> EnemyList { get; private set; }
		public IDictionary<Byte, Enemy> EnemyDict { get; private set; }

	}
}
