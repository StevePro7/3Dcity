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

		public override void Initialize()
		{
			pos1 = new Vector2(0, 0);
			pos2 = new Vector2(0, 80);
			pos3 = new Vector2(0, 240);
			pos4 = new Vector2(0, 480);

			base.Initialize();
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public Int32 Update(GameTime gameTime)
		{
			return (Int32)ScreenType.Test;
		}

		public override void Draw()
		{
			// TODO delegate this to device manager??
			Engine.Game.Window.Title = GetType().Name;

			Engine.SpriteBatch.Draw(Assets.BackgroundTexture, pos1, Color.White);
			Engine.SpriteBatch.Draw(Assets.StarsTexture, pos2, Color.White);
			Engine.SpriteBatch.Draw(Assets.ForegroundTexture, pos3, Color.White);
			Engine.SpriteBatch.Draw(Assets.JoypadTexture, pos4, Color.White);

			Draw160();
			//Draw200();
			
			//Engine.SpriteBatch.Draw(Assets.SteveProTexture200, new Vector2(0, 480 - 200), Color.White);
			//Engine.SpriteBatch.Draw(Assets.JoypadTexture, pos4, Color.White);

			//DrawSquare40(0, 3); DrawSquare40(1, 3); DrawSquare40(2, 3);
			//DrawSquare80(0, 4); DrawSquare80(1, 4); //DrawSquare80(2, 4);
			//DrawSquare80(0, 5); DrawSquare80(1, 5); DrawSquare80(2, 5);
			base.Draw();
		}

		private void Draw160()
		{
			//Engine.SpriteBatch.Draw(Assets.SteveProTexture160, new Vector2(0 + 20, 480 - 160 - 20), Color.White);
			Engine.SpriteBatch.Draw(Assets.JoypadTexture, new Vector2(0 + 20, 480 - 160 - 20), Color.White);

			//Engine.SpriteBatch.Draw(Assets.SteveProTexture160, new Vector2(800 - 160 - 20, 480 - 160 - 20), Color.White);
			Engine.SpriteBatch.Draw(Assets.SteveProTexture80, new Vector2(800 - 80 - 20 - 20, 480 - 80 - 20 - 20), Color.White);
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
