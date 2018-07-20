using System;
using WindowsGame.Common.Static;
using WindowsGame.Define.Interfaces;
using Microsoft.Xna.Framework;
using WindowsGame.Define;

namespace WindowsGame.Common.Screens
{
	public class SplashScreen : BaseScreen, IScreen 
	{
		public override void Initialize()
		{
			base.Initialize();
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public Int32 Update(GameTime gameTime)
		{
			return (Int32)ScreenType.Splash;
		}

		public override void Draw()
		{
			Engine.SpriteBatch.Draw(Assets.SteveProTexture, Vector2.Zero, Color.White);
		}

	}
}
