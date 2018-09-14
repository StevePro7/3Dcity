using System;
using System.Collections;
using System.Collections.Generic;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Sprites
{
	public class Enemy : BaseSprite
	{
		private IList<Byte> blinkFrame;

		public Enemy()
		{
			FrameDelay = new UInt16[Constants.MAX_ENEMYS_FRAME];
			//FrameImage = new Byte[Constants.MAX_ENEMYS_FRAME] { 0, 0, 1, 2, 3, 4, 5, 6, 7, 7, 7, 7, 7 };
			FrameImage = new Byte[Constants.MAX_ENEMYS_FRAME] { 6, 0, 1, 2, 3, 4, 5, 6, 7, 2, 7, 3, 7 };

			blinkFrame = new List<Byte>{ 0, 9, 11 };
		}

		public void Reset(UInt16 frameDelay)
		{
			for (Byte index = 0; index < Constants.MAX_ENEMYS_FRAME; index++)
			{
				FrameDelay[index] = frameDelay;
			}

			EnemyType = EnemyType.Idle;
			//IsActive = false;
			FrameCount = 0;
			FrameIndex = 0;
			FrameTimer = 0;
		}

		public void Spawn()
		{
			// Calculate all frame delays
			EnemyType = EnemyType.Move;
			FrameCount = 0;
			FrameTimer = 0;
			FrameIndex = FrameImage[FrameCount];
		}

		public override void Update(GameTime gameTime)
		{
			if (EnemyType.Move != EnemyType)
			{
				return;
			}

			FrameTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			FrameIndex = FrameImage[FrameCount];
			UInt16 frameDelay = FrameDelay[FrameIndex];
			if (FrameTimer >= frameDelay)
			{
				FrameTimer -= frameDelay;
				FrameCount++;

				// Check for collision after final frame complete!
				if (FrameCount >= MaxFrames)
				{
					EnemyType = EnemyType.Test;
					//IsActive = false;
					//MyGame.Manager.CollisionManager.AddToEnemysCollisionList(ID);		// TODO re-factor!
				}
			}
		}

		public override void Draw()
		{
			if (EnemyType.Move != EnemyType)
			{
				return;
			}

			if (blinkFrame.Contains(FrameCount))
			{
				return;
			}

			base.Draw();
		}

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

		//public Boolean IsActive{ get; private set; }
		public UInt16[] FrameDelay { get; private set; }
		public Byte[] FrameImage { get; private set; }
		public Byte FrameCount { get; private set; }
		public EnemyType EnemyType { get; private set; }
	}
}
