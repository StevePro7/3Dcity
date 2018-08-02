using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Objects
{
	public class SmallTarget : BaseSprite
	{
		private Vector2 oldPosition;

		public SmallTarget()
		{
			oldPosition = Vector2.Zero;
		}

		//private const Single ratio = 0.05f;
		private const Single ratio = 0.15f;			// TODO make configurable to tweak "speed" in which small target moves

		public override void Update(GameTime gameTime, Single horz, Single vert)
		{
			Vector2 position = Position;

			


			// TODO regression test on desktop and further test on mobiles
			//Boolean snap = false;
			//#if WINDOWS
			//            snap = Math.Abs(horz) < Single.Epsilon && Math.Abs(vert) < Single.Epsilon;
			//#elif
			//            snap = 0 == horz && 0 == vert;
			//#endif
			// TODO regression test on desktop and further test on mobiles


			//if (Math.Abs(horz) < Single.Epsilon && Math.Abs(vert) < Single.Epsilon)
			// SQUIGGLE

			// no squiggle
			//int aa = (int)horz;
			//int bb = (int)vert;
			//if (0 == aa && 0 == bb)
			//if (0 == (Int32)horz && 0 == (Int32)vert)
			//if (Math.Abs(horz) < Single.Epsilon && Math.Abs(vert) < Single.Epsilon)

			// TODO - ignore squiggle + check if this snaps back to center on iPad!
			if (0 == horz && 0 == vert)
			{
				position.X = BaseX;
				position.Y = BaseY;
				// return;
			}

			//if (Math.Abs(vert) < 0.001f)
			//{
			//    position.Y = BaseY;
			//}


			// Tolerance
			if (Math.Abs(horz) < Constants.Tolerance)
			{
				horz = 0.0f;
			}
			if (Math.Abs(vert) < Constants.Tolerance)
			{
				vert = 0.0f;
			}


			Single val1 = horz * 100.0f;
			Single val2 = val1 / 2.0f;
			//Single val3 = val2 + BaseX;
			Single val3 = val2 * ratio;

			Single val5 = vert * 100.0f;
			Single val6 = val5 / 2.0f;
			//Single val7 = val6 + BaseY;
			Single val7 = val6 * ratio;


			//TODO check / log / delete
			//if (position.X + val3 <= Bounds.Left || position.X + val3 >= Bounds.Right)
			//{
			//    if (position == oldPosition)
			//    {
			//        val7 = 0.0f;
			//    }
			//}
			//if (position.Y + val7 <= Bounds.Top || position.Y + val7 >= Bounds.Bottom)
			//{
			//    if (position == oldPosition)
			//    {
			//        val3 = 0.0f;
			//    }
			//}
			//TODO check / log / delete


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
			oldPosition = Position;
		}

	}
}
