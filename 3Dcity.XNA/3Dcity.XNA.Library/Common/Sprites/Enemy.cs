﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Sprites
{
	public class Enemy : BaseSprite
	{
		private readonly Byte[] frameAngle;
		private IList<Byte> blinkFrame;
		private Vector2[] origins;
		private Single[] rotates;
		private Boolean enemyRotate;

		public Enemy()
		{
			FrameDelay = new UInt16[Constants.MAX_ENEMYS_FRAME];
			FrameImage = new Byte[] { 0, 0, 1, 2, 3, 4, 5, 6, 7, 7, 7, 7, 7 };
			frameAngle = new Byte[] { 0, 1, 1, 2, 2, 3, 3, 0 };
			origins = GetOrigins();
			rotates = GetRotates();
		}

		public void SetBlinkd(Boolean enemyBlink)
		{
			// Facility to disable blink for testing.
			blinkFrame = new List<Byte> { 0 };
			if (!enemyBlink)
			{
				return;
			}

			blinkFrame.Add(9);
			blinkFrame.Add(11);
		}

		public void Reset()
		{
			SlotID = Constants.INVALID_INDEX;
			EnemyType = EnemyType.Idle;
			FrameCount = 0;
			FrameIndex = 0;
			FrameTimer = 0;
			EnemyLaunch = false;
			enemyRotate = false;
		}

		public void SetDeath()
		{
			Position = Vector2.Zero;
			EnemyType = EnemyType.Kill;
			FrameCount = 12;
			FrameIndex = FrameImage[FrameCount];
		}

		public void Spawn(Byte slotID, UInt16 frameDelay, Vector2 position, Rectangle bounds, LevelType levelType)
		{
			SetSlotID(slotID);

			// Calculate all frame delays
			for (Byte index = 0; index < Constants.MAX_ENEMYS_FRAME; index++)
			{
				FrameDelay[index] = frameDelay;
			}

			// TODO maybe only half the blink delay on Hard level type.
			if (LevelType.Hard == levelType)
			{
				for (Byte index = 1; index < blinkFrame.Count; index++)
				{
					Byte value = blinkFrame[index];
					FrameDelay[value] /= 2;
				}
			}

			SetPosition(position);
			SetBounds(bounds);

			EnemyType = EnemyType.Move;
			FrameCount = 0;
			FrameTimer = 0;
			FrameIndex = FrameImage[FrameCount];
			EnemyLaunch = false;
		}

		public void Start(UInt16 startFrameDelay)
		{
			FrameDelay[0] = startFrameDelay;
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

				// Signal when enemy first visible
				if (1 == FrameCount)
				{
					EnemyLaunch = true;
				}

				// Check for collision after final frame complete!
				if (FrameCount >= MaxFrames)
				{
					EnemyType = EnemyType.Test;
				}
			}
		}

		public override void Draw()
		{
			if (EnemyType.Dead == EnemyType)
			{
				return;
			}

			if (blinkFrame.Contains(FrameCount))
			{
				return;
			}

			Byte index = enemyRotate ? frameAngle[FrameIndex] : (Byte) 0;
			base.DrawRotate(rotates[index], origins[index]);
		}

		public void Dead()
		{
			EnemyType = EnemyType.Dead;
		}
		public void None()
		{
			EnemyType = EnemyType.None;
		}

		public void ResetLaunch()
		{
			EnemyLaunch = false;
		}

		public void SetSlotID()
		{
			SlotID = Constants.INVALID_INDEX;
		}
		public void SetSlotID(Byte slotID)
		{
			SlotID = (SByte)slotID;
		}

		private Vector2[] GetOrigins()
		{
			origins = new Vector2[Constants.MAX_ROTATE];
			origins[(Byte)RotateType.None] = new Vector2(0, 0);
			origins[(Byte)RotateType.Rght] = new Vector2(0, Constants.EnemySize);
			origins[(Byte)RotateType.Down] = new Vector2(Constants.EnemySize, Constants.EnemySize);
			origins[(Byte)RotateType.Left] = new Vector2(Constants.EnemySize, 0);
			return origins;
		}
		private Single[] GetRotates()
		{
			rotates = new Single[Constants.MAX_ROTATE];
			rotates[(Byte)RotateType.None] = MathHelper.ToRadians(0);
			rotates[(Byte)RotateType.Rght] = MathHelper.ToRadians(90);
			rotates[(Byte)RotateType.Down] = MathHelper.ToRadians(180);
			rotates[(Byte)RotateType.Left] = MathHelper.ToRadians(270);
			return rotates;
		}

		public SByte SlotID { get; private set; }
		public UInt16[] FrameDelay { get; private set; }
		public Byte[] FrameImage { get; private set; }
		public Byte FrameCount { get; private set; }
		public EnemyType EnemyType { get; private set; }
		public Boolean EnemyLaunch { get; private set; }
	}
}
