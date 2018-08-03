using System;
using WindowsGame.Common.Static;
using WindowsGame.Define.Interfaces;
using Microsoft.Xna.Framework;
using WindowsGame.Define;

namespace WindowsGame.Common.Screens
{
	public class TestScreen : BaseScreen, IScreen
	{
		private Vector2 pos1;
		private Vector2 pos2;
		private Vector2 pos3;
		private Vector2 pos4;
		private Vector2 joypadTL;
		private Vector2 joypadMD;
		private Vector2 middle;
		private Vector2 sprite;
		private Vector2 leftPos, rightPos;
		private Microsoft.Xna.Framework.Graphics.Texture2D leftImg, rightImg;
		private Single cx, cy;
		private Single sx, sy;
		private Single tx, ty;
		private Byte li, ri;
		private UInt16 lx, ly, rx, ry;

		public override void Initialize()
		{
			pos1 = new Vector2(0, 0);
			pos2 = new Vector2(0, 80);
			pos3 = new Vector2(0, 240);
			pos4 = new Vector2(0, 480);
			
			UInt16 joypadX = MyGame.Manager.ConfigManager.GlobalConfigData.JoypadX;
			UInt16 joypadY = MyGame.Manager.ConfigManager.GlobalConfigData.JoypadY;
			joypadTL = new Vector2(joypadX, joypadY);
			joypadMD = new Vector2(joypadX + 20, joypadY + 20);

			li = MyGame.Manager.ConfigManager.GlobalConfigData.IconLeftI;
			ri = MyGame.Manager.ConfigManager.GlobalConfigData.IconRightI;
			lx = MyGame.Manager.ConfigManager.GlobalConfigData.IconLeftX;
			ly = MyGame.Manager.ConfigManager.GlobalConfigData.IconLeftY;
			rx = MyGame.Manager.ConfigManager.GlobalConfigData.IconRightX;
			ry = MyGame.Manager.ConfigManager.GlobalConfigData.IconRightY;
			leftPos = new Vector2(lx, ly);
			rightPos = new Vector2(rx, ry);

			cx = 80.0f;
			cy = 360.0f;
			middle = new Vector2(cx, cy);

			sx = (Constants.ScreenWide - 80) / 2.0f;
			sy = (Constants.ScreenHigh - 80) / 2.0f;
			sy += 50;

			sx = 300;
			sy = 200;
			sprite = new Vector2(sx, sy);
			LoadTextData();
		}

		public override void LoadContent()
		{
			leftImg = Assets.PlayTexture;
			if (0 != li)
			{
				leftImg = Assets.PauseTexture;
			}

			rightImg = Assets.SoundOnTexture;
			if (0 != ri)
			{
				rightImg = Assets.SoundOffTexture;
			}

			base.LoadContent();
		}

		public Int32 Update(GameTime gameTime)
		{
			//middle.X = cx;
			//middle.Y = cy;

			Single horizontal = MyGame.Manager.InputManager.Horizontal();
			if (horizontal != 0.0f)
			{
				tx = (horizontal - 100.0f) / 100.0f;
				//MyGame.Manager.Logger.Info(horizontal.ToString());
			}

			Single vertical = MyGame.Manager.InputManager.Vertical();
			//Single vertical = 0.0f;
			if (vertical != 0.0f)
			{
				ty = ((vertical - 280) - 100.0f) / 100.0f;
				//MyGame.Manager.Logger.Info(vertical.ToString());
			}

			//const Single tolerance = 0.0f;
			//if (Math.Abs(horizontal) >= tolerance || Math.Abs(vertical) >= tolerance)
			//{

			const byte offset = 4;
			Single vx = horizontal * offset;
			Single vy = vertical * offset;

			if (0 != horizontal)
			{
				middle.X = horizontal - 20;
				sprite.X += tx * 2;
			}
			if (0 != vertical)
			{
				middle.Y = vertical - 20;
				sprite.Y += ty * 2;
			}
			//middle.X += vx;
			//middle.Y -= vy;
			if (middle.X <= 20)
			{
				middle.X = 20.0f;
			}
			if (middle.X >= 140)
			{
				middle.X = 140.0f;
			}
			if (middle.Y <= 300)
			{
				middle.Y = 300.0f;
			}
			if (middle.Y >= 420)
			{
				middle.Y = 420.0f;
			}

			// joypad
			//sprite.X += vx * 2;
			//sprite.Y -= vy * 2;

			
			
			if (sprite.X <= 0.0f)
			{
				sprite.X = 0.0f;
			}
			if (sprite.X >= 720.0f)
			{
				sprite.X = 720.0f;
			}
			if (sprite.Y <= 78.0f)
			{
				sprite.Y = 78.0f;
			}
			if (sprite.Y >= 400.0f)
			{
				sprite.Y = 400.0f;
			}

			//}

			return (Int32)ScreenType.Test;
		}

		// This method assumes that horizontal + vertical
		// return Single value between -1 to +1 range
		public Int32 Update_Joypad(GameTime gameTime)
		{
			//middle.X = cx;
			//middle.Y = cy;

			Single horizontal = MyGame.Manager.InputManager.Horizontal();
			if (horizontal != 0.0f)
			{
				//MyGame.Manager.Logger.Info(horizontal.ToString());
			}

			Single vertical = MyGame.Manager.InputManager.Vertical();
			if (vertical != 0.0f)
			{
				//MyGame.Manager.Logger.Info(vertical.ToString());
			}

			//const Single tolerance = 0.0f;
			//if (Math.Abs(horizontal) >= tolerance || Math.Abs(vertical) >= tolerance)
			//{
			const byte offset = 4;
			Single vx = horizontal * offset;
			Single vy = vertical * offset;

			middle.X += vx;
			middle.Y -= vy;
			if (middle.X <= 20)
			{
				middle.X = 20.0f;
			}
			if (middle.X >= 140)
			{
				middle.X = 140.0f;
			}
			if (middle.Y <= 300)
			{
				middle.Y = 300.0f;
			}
			if (middle.Y >= 420)
			{
				middle.Y = 420.0f;
			}

			sprite.X += vx * 2;
			sprite.Y -= vy * 2;
			if (sprite.X <= 0.0f)
			{
				sprite.X = 0.0f;
			}
			if (sprite.X >= 720.0f)
			{
				sprite.X = 720.0f;
			}
			if (sprite.Y <= 78.0f)
			{
				sprite.Y = 78.0f;
			}
			if (sprite.Y >= 400.0f)
			{
				sprite.Y = 400.0f;
			}
			//}

			return (Int32)ScreenType.Test;
		}

		public override void Draw()
		{
			// TODO delegate this to device manager??
			Engine.Game.Window.Title = GetType().Name;

			//Engine.SpriteBatch.Draw(Assets.GameScreen800, Vector2.Zero, Color.White);
			//Engine.SpriteBatch.Draw(Assets.GameScreen960, Vector2.Zero, Color.White);

			Engine.SpriteBatch.Draw(Assets.BackgroundTexture, pos1, Color.White);
			Engine.SpriteBatch.Draw(Assets.Stars01Texture, pos2, Color.White);
			Engine.SpriteBatch.Draw(Assets.Foreground01Texture, pos3, Color.White);
			Engine.SpriteBatch.Draw(Assets.JoypadTexture, pos4, Color.White);

			Draw160();
			//Draw200();	// DON'T USE!

			//Engine.SpriteBatch.Draw(Assets.SteveProTexture200, new Vector2(0, 480 - 200), Color.White);
			//Engine.SpriteBatch.Draw(Assets.JoypadTexture, pos4, Color.White);

			//DrawSquare40(0, 3); DrawSquare40(1, 3); DrawSquare40(2, 3);
			//DrawSquare80(0, 4); DrawSquare80(1, 4); //DrawSquare80(2, 4);
			//DrawSquare80(0, 5); DrawSquare80(1, 5); DrawSquare80(2, 5);

			MyGame.Manager.TextManager.Draw(TextDataList);

			DrawJoypadButton();

			Engine.SpriteBatch.Draw(leftImg, leftPos, Color.White);
			Engine.SpriteBatch.Draw(rightImg, rightPos, Color.White);

			//Engine.SpriteBatch.Draw(Assets.SteveProTexture80, sprite, Color.Yellow);
			//Engine.SpriteBatch.Draw(Assets.Target80Texture, sprite, Color.White);
			

			//Engine.SpriteBatch.Draw(Assets.Enemy64Texture, new Vector2(420, 200), Color.White);
			
			//Engine.SpriteBatch.Draw(Assets.Enemy128Texture, new Vector2(660, 120), Color.White);

			Engine.SpriteBatch.Draw(Assets.Enemy25Texture, new Vector2(50, 250), Color.White);
			Engine.SpriteBatch.Draw(Assets.Enemy32Texture, new Vector2(100, 240), Color.White);
			Engine.SpriteBatch.Draw(Assets.Enemy40Texture, new Vector2(150, 220), Color.White);
			Engine.SpriteBatch.Draw(Assets.Enemy50Texture, new Vector2(200, 200), Color.White);
			Engine.SpriteBatch.Draw(Assets.Enemy64Texture, new Vector2(300, 200), Color.White);
			Engine.SpriteBatch.Draw(Assets.Enemy80Texture, new Vector2(400, 160), Color.White);
			Engine.SpriteBatch.Draw(Assets.Enemy96Texture, new Vector2(500, 140), Color.White);
			Engine.SpriteBatch.Draw(Assets.Enemy120Texture, new Vector2(640, 120), Color.White);

			//Engine.SpriteBatch.Draw(Assets.Target64Texture, new Vector2(320, 180), Color.White);

			//Engine.SpriteBatch.Draw(Assets.Target64Texture, sprite, Color.White);
			Engine.SpriteBatch.Draw(Assets.Target64Texture, sprite, Color.White);
		}

		private void DrawJoypadButton()
		{
			// Move
			Engine.SpriteBatch.Draw(Assets.Target40Texture, middle, Color.White);
			//Engine.SpriteBatch.Draw(Assets.SteveProTexture40, middle, Color.White);

			// Top left = (80, 360)
			//Engine.SpriteBatch.Draw(Assets.SteveProTexture40, joypadTL, Color.White);

			// Middle
			//Vector2 middle = new Vector2(80, 360);
			//Engine.SpriteBatch.Draw(Assets.SteveProTexture40, middle, Color.White);

			// Middle pixel = (100, 380)
			//Vector2 pixel = new Vector2(100, 380);
			//Engine.SpriteBatch.Draw(Assets.SteveProTexture40, pixel, Color.White);

			// Middle = (60, 340) with 20,20 offset
			//Engine.SpriteBatch.Draw(Assets.SteveProTexture40, joypadMD, Color.White);
		}

		private void Draw160()
		{	
			//Engine.SpriteBatch.Draw(Assets.SteveProTexture160, new Vector2(0 + 20, 480 - 160 - 20), Color.White);
			Engine.SpriteBatch.Draw(Assets.JoypadTexture, new Vector2(0 + 20, 480 - 160 - 20), Color.White);

			//Engine.SpriteBatch.Draw(Assets.SteveProTexture160, new Vector2(800 - 160 - 20, 480 - 160 - 20), Color.White);
			//Engine.SpriteBatch.Draw(Assets.SteveProTexture80, new Vector2(800 - 80 - 20 - 20, 480 - 80 - 20 - 20), Color.White);
			//Engine.SpriteBatch.Draw(Assets.ButtonTexture, new Vector2(800 - 80 - 20 - 20, 480 - 80 - 20 - 20), Color.White);
			Engine.SpriteBatch.Draw(Assets.ButtonOnTexture, new Vector2(800 - 80 - 20 - 20, 480 - 80 - 20), Color.White);
		}

		private void Draw160org()
		{
			Engine.SpriteBatch.Draw(Assets.SteveProTexture160, new Vector2(0, 480 - 160), Color.White);
			Engine.SpriteBatch.Draw(Assets.SteveProTexture160, new Vector2(800 - 160, 480 - 160), Color.White);
		}

		private void Draw200()
		{
			Engine.SpriteBatch.Draw(Assets.SteveProTexture200, new Vector2(0, 480 - 200), Color.White);
			Engine.SpriteBatch.Draw(Assets.SteveProTexture200, new Vector2(800 - 200, 480 - 200), Color.White);
		}

		private void DrawSquare40(Byte x, Byte y)
		{
			const Byte size = 80;
			UInt16 tx = (UInt16)(size * x);
			UInt16 ty = (UInt16)(size * y + 40);
			Vector2 pos = new Vector2(tx, ty);
			Engine.SpriteBatch.Draw(Assets.SteveProTexture40, pos, Color.Black);
		}

		private void DrawSquare80(Byte x, Byte y)
		{
			const Byte size = 80;
			UInt16 tx = (UInt16)(size * x);
			UInt16 ty = (UInt16)(size * y);
			Vector2 pos = new Vector2(tx, ty);
			Engine.SpriteBatch.Draw(Assets.SteveProTexture80, pos, Color.Black);
		}

	}
}
