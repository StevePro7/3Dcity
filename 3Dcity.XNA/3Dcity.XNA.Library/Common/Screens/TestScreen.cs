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
			base.Draw();
		}

	}
}
