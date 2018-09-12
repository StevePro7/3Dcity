using System;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Sprites
{
	public class Enemy : BaseSprite
	{
		public void SetBounds(Byte index)
		{
			// High + wide max enemy.
			const Byte size = 120;
			const Byte wide = 160;
			const Byte high = 200;
			const Byte uppr = 5;

			if (index < uppr)
			{
				Bounds = new Rectangle((wide * index), 80 + Constants.GameOffsetY, (wide - size), (high - size));
			}
			else
			{
				index -= uppr;
				const Byte offset = 190;
				Bounds = new Rectangle(offset + wide * index, 280 + Constants.GameOffsetY, (wide - size), (high - size));
			}
		}
	}

}
