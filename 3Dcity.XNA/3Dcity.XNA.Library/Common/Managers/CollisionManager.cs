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

		Boolean BoxesCollision(Vector2 enemysPosition, Vector2 targetPosition);
		Boolean BoxesCollision(Byte radius, Vector2 enemysPosition, Vector2 targetPosition);
		Boolean ColorCollision(Vector2 enemysPosition, Vector2 targetPosition);
		Boolean CheckOne();
		Boolean CheckOne(Vector2 targetPosition, IList<Enemy> enemyTest);

		// TODO delete
		//void ClearCollisionList();
		//void AddToBulletCollisionList(Byte bulletIndex);
		//void AddToEnemysCollisionList(Byte enemysIndex);

		Boolean EnemyCollideTarget(Vector2 enemysPosition, Vector2 targetPosition);
		SByte DetermineEnemySlot(Vector2 position);

		// TODO delete
		//IList<Byte> BulletCollisionList { get; }
		//IList<Byte> EnemysCollisionList { get; }

		IList<UInt16> EnemysList { get; }
		IList<UInt16> TargetList { get; }
	}

	public class CollisionManager : ICollisionManager
	{
		//private const Byte borderSize = 4;		// TODO delete
		private const Byte bulletSize = 28;

		private String collisionRoot;
		private Byte borderSize;
		private Byte enemysSize;
		private Byte targetSize;
		private Byte offsetSize;

		private const String SPRITE_DIRECTORY = "Sprite";

		public void Initialize()
		{
			Initialize(String.Empty);
		}

		public void Initialize(String root)
		{
			//BulletCollisionList = new List<Byte>(Constants.MAX_BULLET_SHOOT);
			//EnemysCollisionList = new List<Byte>(Constants.MAX_ENEMYS_SPAWN);

			collisionRoot = String.Format("{0}{1}/{2}/{3}", root, Constants.CONTENT_DIRECTORY, Constants.DATA_DIRECTORY, SPRITE_DIRECTORY);
			borderSize = Constants.BorderSize;
			enemysSize = Constants.EnemySize;
			targetSize = Constants.TargetSize;
			offsetSize = (Byte)(targetSize - 2 * borderSize);
		}

		public void LoadContent()
		{
			LoadContentEnemys();
			LoadContentTarget();

			enemysSize = Constants.EnemySize;
			targetSize = Constants.TargetSize;
			offsetSize = (Byte)(targetSize - 2 * borderSize);
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

		public Boolean BoxesCollision(Vector2 enemysPosition, Vector2 targetPosition)
		{
			return BoxesCollision(targetSize, enemysPosition, targetPosition);
		}
		public Boolean BoxesCollision(Byte radius, Vector2 enemysPosition, Vector2 targetPosition)
		{
			UInt16 enemysX = (UInt16) enemysPosition.X;
			UInt16 enemysY = (UInt16) enemysPosition.Y;
			UInt16 targetX = (UInt16) targetPosition.X;
			UInt16 targetY = (UInt16) targetPosition.Y;

			if (targetX + targetSize < enemysX)
			{
				return false;
			}
			if (targetY + targetSize < enemysY)
			{
				return false;
			}
			if (targetX > enemysX + enemysSize)
			{
				return false;
			}
			if (targetY > enemysY + enemysSize)
			{
				return false;
			}

			// An intersection found.
			return true;
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
			// Position injected is the target position [top-left]
			// Bullet is at the same position BUT must offset 28px
			// Reason: 8x8 bullet is middle of target move 28x28px
			Vector2 topLeftPos = position;
			topLeftPos.X += bulletSize;
			topLeftPos.Y += bulletSize;
			position = topLeftPos;

			// Bullet exactly in middle thus cannot kill any ships.
			UInt16 halfWayOffset = (UInt16)(Constants.HALFWAY_DOWN - borderSize);
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

				for (Byte index = 0; index < Constants.BOTTOM_SECTOR; index++)
				{
					if (position.X < ((index + 1) * Constants.DbleSize) - borderSize)
					{
						return (SByte)index;
					}
				}
			}


			// Bottom section [below middle] - slotID more involved.
			if (position.Y > halfWayOffset)
			{
				// Bottom left is invalid.
				if (position.X <= Constants.BOTTOM_OFFSET - borderSize)
				{
					return Constants.INVALID_INDEX;
				}
				// Bottom right is invalid.  [Includes 666]
				if (position.X >= (3 * Constants.DbleSize) + Constants.BOTTOM_OFFSET - borderSize)
				{
					return Constants.INVALID_INDEX;
				}

				// Test 346 or 506 - 2x bottom spots.
				for (Byte index = 0; index < 2; index++)
				{
					UInt16 spot = (UInt16) (((index + 1) * Constants.DbleSize) + Constants.BOTTOM_OFFSET - borderSize); 
					if (spot == (UInt16)position.X)
					{
						return Constants.INVALID_INDEX;
					}
				}

				// Lookup index.
				for (Byte index = Constants.BOTTOM_SECTOR; index < Constants.MAX_ENEMYS_SPAWN; index++)
				{
					Byte value = (Byte)(index - Constants.BOTTOM_SECTOR);
					if (position.X < ((value + 1) * Constants.DbleSize) + Constants.BOTTOM_OFFSET - borderSize)
					{
						return (SByte)index;
					}
				}

				return Constants.INVALID_INDEX;
			}

			return Constants.INVALID_INDEX;
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

		//public void ClearCollisionList()
		//{
		//    BulletCollisionList.Clear();
		//    EnemysCollisionList.Clear();
		//}
		//public void AddToBulletCollisionList(Byte bulletIndex)
		//{
		//    BulletCollisionList.Add(bulletIndex);
		//}
		//public void AddToEnemysCollisionList(Byte enemysIndex)
		//{
		//    EnemysCollisionList.Add(enemysIndex);
		//}

		//public IList<Byte> BulletCollisionList { get; private set; }
		//public IList<Byte> EnemysCollisionList { get; private set; }

		public IList<UInt16> EnemysList { get; private set; }
		public IList<UInt16> TargetList { get; private set; }
	}
}

