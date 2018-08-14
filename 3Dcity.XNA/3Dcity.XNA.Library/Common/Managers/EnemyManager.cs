using System;
using System.Collections.Generic;
using WindowsGame.Common.Sprites;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IEnemyManager 
	{
		void Initialize();
		void LoadContent();
		void LoadLevel(Byte enemies);
		void Update(GameTime gameTime);
		void Draw();

		IList<Enemy> EnemyList { get; }
		IDictionary<Byte, Enemy> EnemyDict { get; }
	}

	public class EnemyManager : IEnemyManager 
	{
		private const Byte MAX_ENEMYS = 8;

		public void Initialize()
		{
			EnemyList = new List<Enemy>(MAX_ENEMYS);
			EnemyDict = new Dictionary<Byte, Enemy>();
			for (Byte index = 0; index < MAX_ENEMYS; index++)
			{
				Enemy enemy = new Enemy();
				EnemyList.Add(enemy);

				EnemyDict[index] = null;
			}
		}

		public void LoadContent()
		{
		}

		public void LoadLevel(Byte enemies)
		{
			
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
