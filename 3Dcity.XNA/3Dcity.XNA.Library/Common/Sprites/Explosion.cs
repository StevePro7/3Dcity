using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Sprites
{
	public class Explosion : BaseSprite
	{
		public void Reset(UInt16 frameDelay)
		{
			// Constant throughout level.
			FrameDelay = frameDelay;
			IsExploding = false;
			FrameIndex = 0;
			FrameTimer = 0;
		}

		public void Explode()
		{
			IsExploding = true;
			FrameIndex = 0;
			FrameTimer = 0;
		}

		public override void Update(GameTime gameTime)
		{
			if (!IsExploding)
			{
				return;
			}

			FrameTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			if (FrameTimer >= FrameDelay)
			{
				FrameTimer -= FrameDelay;
				FrameIndex++;

				if (FrameIndex >= MaxFrames)
				{
					IsExploding = false;
				}
			}
		}

		public Boolean IsExploding { get; private set; }
		public UInt16 FrameDelay { get; private set; }

	}
}
