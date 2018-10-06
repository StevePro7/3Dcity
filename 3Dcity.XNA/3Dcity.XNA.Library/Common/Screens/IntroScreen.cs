using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class IntroScreen : BaseScreen, IScreen
	{
		private Vector2 startPosition;
		private Vector2 titlePosition;
		private Vector2 moverPosition;
		//private Vector2 buildPosition;
		private Single startY;
		private Single titleY;
		private Single deltaY;
		private Boolean coolMusic;
		//private String buildVersion;

		public override void Initialize()
		{
			base.Initialize();
			UpdateGrid = false;

			titlePosition = new Vector2((Constants.ScreenWide - 240) / 2.0f, (Constants.ScreenHigh - Constants.DbleSize) / 2.0f + 94);
			startPosition = new Vector2(titlePosition.X, Constants.ScreenHigh - Constants.GameOffsetY + 10);

			startY = startPosition.Y;
			titleY = titlePosition.Y;
		}

		public override void LoadContent()
		{
			//buildPosition = MyGame.Manager.TextManager.GetTextPosition(35, 23);
			UInt16 introDelay = MyGame.Manager.ConfigManager.GlobalConfigData.IntroDelay;
			deltaY = startY - titleY;
			deltaY = introDelay / deltaY;
			moverPosition = startPosition;
			coolMusic = MyGame.Manager.StateManager.CoolMusic;
			SongType song = coolMusic ? SongType.CoolMusic : SongType.GameTitle;
			MyGame.Manager.SoundManager.PlayMusic(song, false);
			//buildVersion = MyGame.Manager.DeviceManager.BuildVersion;
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			Timer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;

			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			if (startY > titleY)
			{
				Single delta = (Single) gameTime.ElapsedGameTime.TotalSeconds;
				//startY -= delta * deltaY * 24;
				startY -= delta * deltaY * 8;			// TODO make configurable??
				moverPosition.Y = startY;
			}

			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.RenderManager.DrawTitle(moverPosition);
			MyGame.Manager.RenderManager.DrawBottom();

			// Text data last!
			MyGame.Manager.TextManager.DrawBuild();
			MyGame.Manager.TextManager.DrawTitle();
			//MyGame.Manager.ScoreManager.Draw();		// TODO - leave out for now...
			//Engine.SpriteBatch.DrawString(Assets.EmulogicFont, buildVersion, buildPosition, Color.White);
		}

	}
}
