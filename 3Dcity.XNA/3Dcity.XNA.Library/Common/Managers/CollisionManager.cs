using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface ICollisionManager 
	{
		void Initialize();
		void Initialize(String root);
		void LoadContent();
		void LoadContentEnemys();
		void LoadContentTarget();

		Boolean ColorCollision(Vector2 enemysPosition, Vector2 targetPosition);
		Boolean CheckOne();
		Boolean CheckOne(Vector2 targetPosition, IList<Enemy> enemyTest);

		void ClearCollisionList();
		void AddToBulletCollisionList(Byte bulletIndex);
		void AddToEnemysCollisionList(Byte enemysIndex);

		Boolean EnemyCollideTarget(Vector2 enemysPosition, Vector2 targetPosition);
		SByte DetermineEnemySlot(Vector2 position);

		IList<Byte> BulletCollisionList { get; }
		IList<Byte> EnemysCollisionList { get; }

		IList<UInt16> EnemysList { get; }
		IList<UInt16> TargetList { get; }
	}

	public class CollisionManager : ICollisionManager
	{
		private const Byte borderSize = 4;
		private const Byte bulletSize = 28;
		private String collisionRoot;
		private Byte enemysSize;
		private Byte offsetSize;

		private const String SPRITE_DIRECTORY = "Sprite";

		public void Initialize()
		{
			Initialize(String.Empty);
		}

		public void Initialize(String root)
		{
			BulletCollisionList = new List<Byte>(Constants.MAX_BULLET_SHOOT);
			EnemysCollisionList = new List<Byte>(Constants.MAX_ENEMYS_SPAWN);

			collisionRoot = String.Format("{0}{1}/{2}/{3}", root, Constants.CONTENT_DIRECTORY, Constants.DATA_DIRECTORY, SPRITE_DIRECTORY);
		}

		public void LoadContent()
		{
			LoadContentEnemys();
			LoadContentTarget();

			enemysSize = Constants.EnemySize;
			offsetSize = Constants.TargetSize - 2 * borderSize;
		}

		public void LoadContentEnemys()
		{
			EnemysList = LoadContentData("Enemys");
		}
		public void LoadContentTarget()
		{
			TargetList = LoadContentData("Target");
		}
		private IList<UInt16> LoadContentData(String file)
		{
			file = String.Format("{0}/{1}.txt", collisionRoot, file);
			return MyGame.Manager.FileManager.LoadTxt<UInt16>(file);
		}


		public Boolean ColorCollision(Vector2 enemysPosition, Vector2 targetPosition)
		{
			// Use 56 x 56 as target collision thus border offset is 4px on all sides.
			Vector2 offsetPosition = targetPosition;
			offsetPosition.X += borderSize;
			offsetPosition.Y += borderSize;

			Int16 enemysPosX = (Int16) enemysPosition.X;
			Int16 enemysPosY = (Int16) enemysPosition.Y;
			Int16 offsetPosX = (Int16) offsetPosition.X;
			Int16 offsetPosY = (Int16) offsetPosition.Y;

			Int16 lft = Math.Max(enemysPosX, offsetPosX);
			Int16 top = Math.Max(enemysPosY, offsetPosY);
			Int16 rgt = Math.Min((Int16)(enemysPosX + enemysSize), (Int16)(offsetPosX + offsetSize));
			Int16 bot = Math.Min((Int16)(enemysPosY + enemysSize), (Int16)(offsetPosY + offsetSize));

			// Check every point within the intersection bounds.
			for (Int16 y = top; y < bot; y++)
			{
				for (Int16 x = lft; x < rgt; x++)
				{
					UInt16 targetIndex = (UInt16)((x - offsetPosX) + (y - offsetPosY) * offsetSize);
					if (!TargetList.Contains(targetIndex))
					{
						continue;
					}
					UInt16 enemysIndex = (UInt16)((x - enemysPosX) + (y - enemysPosY) * enemysSize);
					if (!EnemysList.Contains(enemysIndex))
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

		public SByte DetermineEnemySlot(Vector2 position)
		{
			//Vector2 topLeftPos = position;
			//topLeftPos.X -= bulletSize;
			//topLeftPos.Y -= bulletSize;
			//position = topLeftPos

			// Bullet exactly in middle thus cannot kill any ships.
			const UInt16 halfWayOffset = Constants.HALFWAY_DOWN - borderSize;
			if (halfWayOffset == (UInt16)position.Y)
			{
				return Constants.INVALID_INDEX;
			}

			// Top section [above middle] - check slotID is simple.
			if (position.Y < halfWayOffset)
			{
				Byte modulo = (Byte)(position.X / Constants.DbleSize);
				UInt16 data = (UInt16)((modulo + 1) * Constants.DbleSize);

				var bob = (Int16)(data - position.X);
				if (borderSize == (Int16)(data - position.X))
				{
					return Constants.INVALID_INDEX;
				}

				// TODO test this!
				//for (Byte index = 0; index < Constants.BOTTOM_SECTOR; index++)
				//{
				//    if (position.X < ((index + 1) * Constants.DbleSize) - borderSize)
				//    {
				//        return (SByte)index;
				//    }
				//}

				if (position.X < (1 * 160) - 4)
				{
					return 0;
				}
				if (position.X < (2 * 160) - 4)
				{
					return 1;
				}
				if (position.X < (3 * 160) - 4)
				{
					return 2;
				}
				if (position.X < (4 * 160) - 4)
				{
					return 3;
				}
				if (position.X < (5 * 160) - 4)
				{
					return 4;
				}
			}

			// Bottom section [below middle] - slotID more involved.
			if (position.Y > halfWayOffset)
			{
				if (position.X < Constants.BOTTOM_OFFSET)
				{
					return Constants.INVALID_INDEX;
				}
				if (position.X > (3 * Constants.BOTTOM_OFFSET))
				{
					return Constants.INVALID_INDEX;
				}


				// TODO test this!
				Byte modulo = (Byte)((position.X + Constants.BOTTOM_OFFSET) / Constants.DbleSize);
				UInt16 data = (UInt16)((modulo + 1) * Constants.DbleSize);

				var bob = (Int16)(data - (position.X + Constants.BOTTOM_OFFSET));
				if (borderSize == (Int16)(data - (position.X + Constants.BOTTOM_OFFSET)))
				{
					return Constants.INVALID_INDEX;
				}

				// TODO test this!
				//for (Byte index = Constants.BOTTOM_SECTOR; index < Constants.MAX_ENEMYS_SPAWN; index++)
				//{
				//    if ((position.X + Constants.BOTTOM_OFFSET) < ((index + 1) * Constants.DbleSize) + Constants.BOTTOM_OFFSET - borderSize)
				//    {
				//        return (SByte)index;
				//    }
				//}

				if (position.X < (1 * 160) + Constants.BOTTOM_OFFSET - 4)
				{
					return 5;
				}
				if (position.X < (2 * 160) + Constants.BOTTOM_OFFSET - 4)
				{
					return 6;
				}
				if (position.X < (3 * 160) + Constants.BOTTOM_OFFSET - 4)
				{
					return 7;
				}
			}

			return SByte.MinValue;
			//return Constants.INVALID_INDEX;
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

		public IList<UInt16> EnemysList { get; private set; }
		public IList<UInt16> TargetList { get; private set; }
	}
}

