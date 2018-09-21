using System;
using System.Collections.Generic;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface ICollisionManager 
	{
		void Initialize();

		Boolean CheckOne();
		Boolean CheckOne(Vector2 targetPosition, IList<Enemy> enemyTest);

		void ClearCollisionList();
		void AddToBulletCollisionList(Byte bulletIndex);
		void AddToEnemysCollisionList(Byte enemysIndex);

		Boolean EnemyCollideTarget(Vector2 enemysPosition, Vector2 targetPosition);

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

		public Boolean EnemyCollideTarget(Vector2 enemysPosition, Vector2 targetPosition)
		{
			return false;
		}

		// TODO give a better name!
		public Boolean CheckOne()
		{
			Vector2 targetPosition = MyGame.Manager.SpriteManager.LargeTarget.Position;
			IList<Enemy> enemyTest = MyGame.Manager.EnemyManager.EnemyTest;

			return CheckOne(targetPosition, enemyTest);
		}

		public Boolean CheckOne(Vector2 targetPosition, IList<Enemy> EnemyTest)
		{
			// TODO implement collision logic between enemies to test and target.
			return false;
		}

		public void ClearCollisionList()
		{
			BulletCollisionList.Clear();
			EnemysCollisionList.Clear();
		}

		public void AddToBulletCollisionList(Byte bulletIndex)
		{
			BulletCollisionList.Add(bulletIndex);
		}

		public void AddToEnemysCollisionList(Byte enemysIndex)
		{
			EnemysCollisionList.Add(enemysIndex);
		}

		public IList<Byte> BulletCollisionList { get; private set; }
		public IList<Byte> EnemysCollisionList { get; private set; }

	}
}
