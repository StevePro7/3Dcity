using System;
using System.Collections.Generic;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface ICollisionManager 
	{
		void Initialize();
		void ClearBulletCollisionList();
		void AddToBulletCollisionList(Byte bulletIndex);
		void AddToEnemysCollisionList(Byte enemysIndex);
		void Update(GameTime gameTime);
		void Draw();

		IList<Byte> BulletCollisionList { get; }
		IList<Byte> EnemysCollisionList { get; }
	}

	public class CollisionManager : ICollisionManager 
	{
		public void Initialize()
		{
			BulletCollisionList = new List<Byte>(Constants.MAX_BULLET_SHOOT);
			EnemysCollisionList = new List<Byte>(Constants.MAX_ENEMYS_SPAWN);
		}

		public void ClearBulletCollisionList()
		{
			BulletCollisionList.Clear();
		}

		public void AddToBulletCollisionList(Byte bulletIndex)
		{
			BulletCollisionList.Add(bulletIndex);
		}

		public void AddToEnemysCollisionList(Byte enemysIndex)
		{
			
		}

		public void Update(GameTime gameTime)
		{
		}

		public void Draw()
		{
		}

		public IList<Byte> BulletCollisionList { get; private set; }
		public IList<Byte> EnemysCollisionList { get; private set; }

	}
}
