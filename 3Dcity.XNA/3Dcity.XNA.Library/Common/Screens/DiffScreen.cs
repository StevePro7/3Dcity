﻿using System;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class DiffScreen : BaseScreen, IScreen
	{
		private Vector2[] cursorPositions;
		private Vector2 spritePosition;
		private Vector2 titlePosition;

		private UInt16 delay;
		private Byte levelType;
		private Byte iconIndex, moveIndex;
		private Boolean flag1, flag2;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			iconIndex = 0;
			moveIndex = 1;

			cursorPositions = GetCursorPositions();
			spritePosition = MyGame.Manager.SpriteManager.SmallTarget.Position;
			spritePosition.X = Constants.CURSOR_OFFSET_X[moveIndex];

			titlePosition = new Vector2((Constants.ScreenWide - 240) / 2.0f, (Constants.ScreenHigh - 160) / 2.0f + Constants.GameOffsetY + 94);

			delay = MyGame.Manager.ConfigManager.GlobalConfigData.SelectDelay;

			levelType = (Byte)MyGame.Manager.LevelManager.LevelType;
			flag1 = flag2 = false;

			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			if (flag1)
			{
				UpdateTimer(gameTime);
				if (Timer > delay * 2)
				{
					flag1 = false;
					iconIndex = Convert.ToByte(flag1);
					MyGame.Manager.IconManager.UpdateIcon(MyGame.Manager.IconManager.JoyButton, iconIndex);
					MyGame.Manager.LevelManager.SetLevelType((LevelType)levelType);
					return (Int32) ScreenType.Over;			// TODO goto level select screen
				}

				iconIndex = Convert.ToByte(flag1);
				MyGame.Manager.IconManager.UpdateIcon(MyGame.Manager.IconManager.JoyButton, iconIndex);
				return (Int32)CurrScreen;
			}

			if (flag2)
			{
				UpdateTimer(gameTime);
				if (Timer > delay)
				{
					moveIndex = 1;
					spritePosition.X = Constants.CURSOR_OFFSET_X[moveIndex];
					MyGame.Manager.SpriteManager.SmallTarget.SetPosition(spritePosition);

					Timer = 0;
					flag2 = false;
				}

				return (Int32)CurrScreen;
			}

			// Check fire first.
			Boolean fire = MyGame.Manager.InputManager.Fire();
			if (fire)
			{
				flag1 = true;
				return (Int32)CurrScreen;
			}

			// Check move second.
			Single horz = MyGame.Manager.InputManager.Horizontal();
			if (0 == horz)
			{
				return (Int32)CurrScreen;
			}

			if (horz < 0)
			{
				moveIndex = 0;
			}
			if (horz > 0)
			{
				moveIndex = 2;
			}

			spritePosition.X = Constants.CURSOR_OFFSET_X[moveIndex];
			MyGame.Manager.SpriteManager.SmallTarget.SetPosition(spritePosition);

			levelType = (Byte) (1 - levelType);
			flag2 = true;
			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			MyGame.Manager.SpriteManager.DrawCursor();

			Engine.SpriteBatch.Draw(Assets.title00, titlePosition, Color.White);

			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawCursor(cursorPositions[levelType]);
		}

		private static Vector2[] GetCursorPositions()
		{
			Vector2[] positions = new Vector2[2];
			positions[0] = MyGame.Manager.TextManager.GetTextPosition(12, 11);
			positions[1] = MyGame.Manager.TextManager.GetTextPosition(23, 11);
			return positions;
		}

	}
}
