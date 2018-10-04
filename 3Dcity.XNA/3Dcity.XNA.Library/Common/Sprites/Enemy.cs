using System;
using System.Collections.Generic;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Sprites
{
	public class Enemy : BaseSprite
	{
		private readonly IList<Byte> blinkFrame;

		public Enemy()
		{
			FrameDelay = new UInt16[Constants.MAX_ENEMYS_FRAME];
			FrameImage = new Byte[] { 0, 0, 1, 2, 3, 4, 5, 6, 7, 7, 7, 7, 7 };
			blinkFrame = new List<Byte>{ 0, 9, 11 };
		}

		public void Reset()
		{
			SlotID = Constants.INVALID_INDEX;
			EnemyType = EnemyType.Idle;
			FrameCount = 0;
			FrameIndex = 0;
			FrameTimer = 0;

			// TODO uncomment for testing collision offsets on Resume screen.
			//FrameCount = 1;
			//FrameIndex = MyGame.Manager.ConfigManager.GlobalConfigData.EnemyFrame;
		}

		public void Spawn(Byte slotID, UInt16 frameDelay, Vector2 position, Rectangle bounds)
		{
			SetSlotID(slotID);

			// Calculate all frame delays
			for (Byte index = 0; index < Constants.MAX_ENEMYS_FRAME; index++)
			{
				FrameDelay[index] = frameDelay;
				if (blinkFrame.Contains(index))
				{
					FrameDelay[index] /= 2;
				}
			}

			SetPosition(position);
			SetBounds(bounds);

			EnemyType = EnemyType.Move;
			FrameCount = 0;
			FrameTimer = 0;
			FrameIndex = FrameImage[FrameCount];
		}

		public void Start(UInt16 frameDelay)
		{
			// Longer delay for initial move.
			FrameDelay[0] = frameDelay;
		}

		public override void Update(GameTime gameTime)
		{
			if (EnemyType.Move != EnemyType)
			{
				return;
			}

			FrameTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			FrameIndex = FrameImage[FrameCount];
			UInt16 frameDelay = FrameDelay[FrameCount];
			if (FrameTimer >= frameDelay)
			{
				FrameTimer -= frameDelay;
				FrameCount++;

				// Check for collision after final frame complete!
				if (FrameCount >= MaxFrames)
				{
					EnemyType = EnemyType.Test;
				}
			}
		}

		public override void Draw()
		{
			//if (EnemyType.Move != EnemyType)
			//{
			//    return;
			//}

			if (EnemyType.Dead == EnemyType)
			{
				return;
			}


			if (blinkFrame.Contains(FrameCount))
			{
				return;
			}

			base.Draw();
		}

		public void Dead()
		{
			EnemyType = EnemyType.Dead;
		}
		public void None()
		{
			EnemyType = EnemyType.None;
		}

		public void SetSlotID()
		{
			SlotID = Constants.INVALID_INDEX;
		}
		public void SetSlotID(Byte slotID)
		{
			SlotID = (SByte)slotID;
		}

		public SByte SlotID { get; private set; }
		public UInt16[] FrameDelay { get; private set; }
		public Byte[] FrameImage { get; private set; }
		public Byte FrameCount { get; private set; }
		public EnemyType EnemyType { get; private set; }
	}
}
