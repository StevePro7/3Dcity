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

		IList<Direction>[] DirectionList { get; }
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
		public Vector2[] UnitVelocityList { get; private set; }
		public Vector2[] MoveVelocityList { get; private set; }
	}
}
