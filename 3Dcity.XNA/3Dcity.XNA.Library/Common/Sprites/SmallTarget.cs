using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Sprites
{
	public class SmallTarget : BaseSprite
	{
		private const Single RATIO = 0.15f;			// TODO make configurable to tweak "speed" in which small target moves

		public void Update(GameTime gameTime, Single horz, Single vert)
		{
			Vector2 position = Position;

			// TODO - ignore squiggle + check if this snaps back to center on iPad!
			if (0 == horz && 0 == vert)
			{
				position.X = BaseX;
				position.Y = BaseY;

				Position = position;
				return;
			}


			// Tolerance
			if (Math.Abs(horz) < Constants.GeneralTolerance)
			{
				horz = 0.0f;
			}
			if (Math.Abs(vert) < Constants.GeneralTolerance)
			{
				vert = 0.0f;
			}


			Single val1 = horz * 100.0f;
			Single val2 = val1 / 2.0f;
			//Single val3 = val2 + BaseX;
			Single val3 = val2 * RATIO;

			Single val5 = vert * 100.0f;
			Single val6 = val5 / 2.0f;
			//Single val7 = val6 + BaseY;
			Single val7 = val6 * RATIO;


			position.X += val3;
			position.Y += val7;

			if (position.X <= Bounds.Left)
			{
				position.X = Bounds.Left;
			}
			if (position.X >= Bounds.Right)
			{
				position.X = Bounds.Right;
			}
			if (position.Y <= Bounds.Top)
			{
				position.Y = Bounds.Top;
			}
			if (position.Y >= Bounds.Bottom)
			{
				position.Y = Bounds.Bottom;
			}

			Position = position;
		}

	}
}
