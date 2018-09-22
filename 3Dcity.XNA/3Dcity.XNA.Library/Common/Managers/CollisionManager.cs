using System;
using System.Collections.Generic;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Managers
{
	public interface ICollisionManager 
	{
		void Initialize();
		void LoadContent();

		Boolean ColorCollision(Vector2 enemysPosition, Vector2 targetPosition);
		Boolean CheckOne();
		Boolean CheckOne(Vector2 targetPosition, IList<Enemy> enemyTest);

		void ClearCollisionList();
		void AddToBulletCollisionList(Byte bulletIndex);
		void AddToEnemysCollisionList(Byte enemysIndex);

		Boolean EnemyCollideTarget(Vector2 enemysPosition, Vector2 targetPosition);

		IList<Byte> BulletCollisionList { get; }
		IList<Byte> EnemysCollisionList { get; }

		Color[] EnemysColor { get; }
		Color[] TargetColor { get; }
	}

	public class CollisionManager : ICollisionManager
	{
		private Byte enemysSize;
		private Byte targetSize;

		public void Initialize()
		{
			BulletCollisionList = new List<Byte>(Constants.MAX_BULLET_SHOOT);
			EnemysCollisionList = new List<Byte>(Constants.MAX_ENEMYS_SPAWN);
		}

		public void LoadContent()
		{
			enemysSize = (Byte)(Assets.Enemy120.Width);
			targetSize = (Byte)(Assets.Target56.Width);

			EnemysColor = new Color[Assets.Enemy120.Width * Assets.Enemy120.Height];
			Assets.Enemy120.GetData(EnemysColor);

			TargetColor = new Color[Assets.Target56.Width * Assets.Target56.Height];
			Assets.Target56.GetData(TargetColor);
		}

		public Boolean ColorCollision(Vector2 enemysPosition, Vector2 targetPosition)
		{
			Int16 enemysPosX = (Int16) enemysPosition.X;
			Int16 enemysPosY = (Int16) enemysPosition.Y;
			Int16 targetPosX = (Int16) targetPosition.X;
			Int16 targetPosY = (Int16) targetPosition.Y;

			Int16 lft = Math.Max(enemysPosX, targetPosX);
			Int16 top = Math.Max(enemysPosY, targetPosY);
			Int16 rgt = Math.Min((Int16)(enemysPosX + enemysSize), (Int16)(targetPosX + targetSize));
			Int16 bot = Math.Min((Int16)(enemysPosY + enemysSize), (Int16)(targetPosY + targetSize));


			// Check every point within the intersection bounds.
			for (Int16 y = top; y < bot; y++)
			{
				for (Int16 x = lft; x < rgt; x++)
				{
					UInt16 targetIndex = (UInt16)((x - targetPosX) + (y - targetPosY) * targetSize);
					Color targetColor = TargetColor[targetIndex];
					if (0 == targetColor.A)
					{
						continue;
					}

					UInt16 enemysIndex = (UInt16)((x - enemysPosX) + (y - enemysPosY) * enemysSize);
					Color enemysColor = EnemysColor[enemysIndex];
					if (0 == enemysColor.A)
					{
						continue;
					}

					// An intersection has been found.
					return true;
				}
			}

			// No intersection found.
			return false;
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

		public Color[] EnemysColor { get; private set; }
		public Color[] TargetColor { get; private set; }
	}
}
