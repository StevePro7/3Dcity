using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Sprites
{
	public class Bullet : BaseSprite
	{
		public override void Initialize(Vector2 position)
		{
			base.Initialize(position);
			BulletType = BulletType.Idle;
		}

		public void Reset(UInt16 frameDelay)
		{
			// TODO remove this as bullet only has 2x states
			BulletType = BulletType.Idle;

			// Constant throughout level.
			FrameDelay = frameDelay;
			//ShootDelay = shootDelay;

			IsFiring = false;
			FrameIndex = 0;
			FrameTimer = 0;
		}

		public void Shoot(Vector2 position)
		{
			IsFiring = true;
			FrameTimer = 0;
			FrameIndex = 0;
			Position = position;
		}

		public override void Update(GameTime gameTime)
		{
			if (!IsFiring)
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
					IsFiring = false;
					// Check collision
				}
			}
		}

		public void SetID(Byte index)
		{
			ID = index;
		}

		public Byte ID { get; private set; }

		public BulletType BulletType { get; private set; }

		public Boolean IsFiring { get; private set; }
		public UInt16 FrameDelay { get; private set; }
		//public UInt16 ShootDelay { get; private set; }
		public Single FrameTimer { get; private set; }
	}

}
