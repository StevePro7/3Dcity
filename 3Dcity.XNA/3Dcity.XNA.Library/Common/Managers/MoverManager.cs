using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IMoverManager
	{
		void Initialize();
		void ResetEnemyMoves(IList<MoveType> enemyMoves, Byte percentage, Byte enemyTotal, MoveType moveType);
		Boolean ShouldEnemyMove(Byte item, LevelType levelType);
		Boolean UpdateVelocity(Byte frameIndex, MoveType moveType, LevelType levelType);
		Vector2 GetEnemyVelocity(MoveType moveType);

		IList<Direction>[] DirectionList { get; }
		IList<Byte>[] MoveFrameList { get; }
		Vector2[] UnitVelocityList { get; }
		Vector2[] MoveVelocityList { get; }
	}

	public class MoverManager : IMoverManager
	{
		public void Initialize()
		{
			// Initialize directions.
			DirectionList = new IList<Direction>[Constants.MAX_MOVES];
			DirectionList[(Byte)MoveType.None] = new List<Direction> { Direction.None };
			DirectionList[(Byte)MoveType.Horz] = GetHorzDirection();
			DirectionList[(Byte)MoveType.Vert] = GetVertDirection();
			DirectionList[(Byte)MoveType.Both] = GetBothDirection();

			// Initialize move frames.
			MoveFrameList = new IList<Byte>[2];
			MoveFrameList[(Byte) LevelType.Easy] = new List<Byte> { 1, 2, 3, 4};
			MoveFrameList[(Byte) LevelType.Hard] = new List<Byte> {1, 2, 3, 4, 5};

			// Initialize velocities.
			UnitVelocityList = new Vector2[Constants.MAX_MOVES + 1];
			UnitVelocityList[(Byte)Direction.None] = Vector2.Zero;
			UnitVelocityList[(Byte)Direction.Left] = new Vector2(-1, 0);
			UnitVelocityList[(Byte)Direction.Right] = new Vector2(1, 0);
			UnitVelocityList[(Byte)Direction.Up] = new Vector2(0, -1);
			UnitVelocityList[(Byte)Direction.Down] = new Vector2(0, 1);
		}

		public void ResetEnemyMoves(IList<MoveType> enemyMoves, Byte percentage, Byte enemyTotal, MoveType moveType)
		{
			const Byte first = 1;
			Byte iterations = (Byte)(percentage / 100.0f * enemyTotal);
			for (Byte index = 0; index < iterations; index++)
			{
				while (true)
				{
					// Always want first [0th] enemy to be None so random starts >= first.
					Byte key = (Byte)MyGame.Manager.RandomManager.Next(first, enemyTotal);
					if (MoveType.None == enemyMoves[key])
					{
						enemyMoves[key] = moveType;
						break;
					}
				}
			}
		}

		public Boolean ShouldEnemyMove(Byte item, LevelType levelType)
		{
			IList<Byte> moveFrames = MoveFrameList[(Byte) levelType];
			return moveFrames.Contains(item);
		}

		public Boolean UpdateVelocity(Byte frameIndex, MoveType moveType, LevelType levelType)
		{
			if (MoveType.Both == moveType)
			{
				return true;
			}

			IList<Byte> moveFrames = MoveFrameList[(Byte)levelType];
			Byte moveFrame = moveFrames[0];
			if (frameIndex == moveFrame)
			{
				return true;
			}

			return false;
		}

		public Vector2 GetEnemyVelocity(MoveType moveType)
		{
			IList<Direction> directionList = DirectionList[(Byte) moveType];
			Byte max = (Byte) directionList.Count;
			Byte index = (Byte)MyGame.Manager.RandomManager.Next(max);
			Direction direction = directionList[index];
			Vector2 unitVelocity = UnitVelocityList[(Byte) direction];
			return unitVelocity;
		}

		private static IList<Direction> GetHorzDirection()
		{
			return new List<Direction>
			{
				Direction.Left,
				Direction.Right,
			};
		}
		private static IList<Direction> GetVertDirection()
		{
			return new List<Direction>
			{
				Direction.Up,
				Direction.Down,
			};
		}
		private static IList<Direction> GetBothDirection()
		{
			return new List<Direction>
			{
				Direction.Left,
				Direction.Right,
				Direction.Up,
				Direction.Down,
			};
		}

		public IList<Direction>[] DirectionList { get; private set; }
		public IList<Byte>[] MoveFrameList { get; private set; }
		public Vector2[] UnitVelocityList { get; private set; }
		public Vector2[] MoveVelocityList { get; private set; }
	}
}
