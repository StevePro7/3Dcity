using System;
using WindowsGame.Master;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class BeginScreen : BaseScreen, IScreen
	{
		private Vector2 promptPosition;
		private UInt16 promptDelay;
		private UInt16 promptTimer;
		private Boolean flag;

		public override void Initialize()
		{
			base.Initialize();
			UpdateGrid = false;
			promptPosition = MyGame.Manager.TextManager.GetTextPosition(14, 11);
			promptPosition.X -= 7.5f;
			promptDelay = MyGame.Manager.ConfigManager.GlobalConfigData.BeginDelay;
		}

		public override void LoadContent()
		{
			MyGame.Manager.DebugManager.Reset();
			base.LoadContent();

			promptTimer = 0;
			flag = true;
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			promptTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			if (promptTimer > promptDelay)
			{
				promptTimer -= promptDelay;
				flag = !flag;
			}

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();
			MyGame.Manager.RenderManager.DrawTitle();

			// Text data last!
			MyGame.Manager.TextManager.DrawBuild();
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.ScoreManager.Draw();

			if (flag)
			{
				Engine.SpriteBatch.DrawString(Assets.EmulogicFont, Globalize.INSERT_COINS, promptPosition, Color.White);	
			}
		}

	}
}
