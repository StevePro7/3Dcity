using System;

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

		public Boolean IsExploding { get; private set; }
		public UInt16 FrameDelay { get; private set; }

	}
}
