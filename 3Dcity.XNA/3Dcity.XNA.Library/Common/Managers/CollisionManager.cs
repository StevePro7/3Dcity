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
		void AddToBulletCollisionList(Byte index);
		void Update(GameTime gameTime);
		void Draw();

		IList<Byte> BulletCollisionList { get; }
	}

	public class CollisionManager : ICollisionManager 
	{
		public void Initialize()
		{
			BulletCollisionList = new List<Byte>(Constants.MAX_BULLET_SHOOT);
		}

		public void ClearBulletCollisionList()
		{
			BulletCollisionList.Clear();
		}

		public void AddToBulletCollisionList(Byte index)
		{
			BulletCollisionList.Add(index);
		}

		public void Update(GameTime gameTime)
		{
		}

		public void Draw()
		{
		}

		public IList<Byte> BulletCollisionList { get; private set; }

	}
}
