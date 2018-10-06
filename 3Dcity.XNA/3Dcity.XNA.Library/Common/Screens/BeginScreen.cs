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
		private UInt16 selectDelay;
		private Byte iconIndex;
		private Boolean flag1, flag2;

		public override void Initialize()
		{
			base.Initialize();
			UpdateGrid = false;
			promptPosition = MyGame.Manager.TextManager.GetTextPosition(14, 11);
			promptPosition.X -= 7.5f;
			promptDelay = MyGame.Manager.ConfigManager.GlobalConfigData.BeginDelay;
			selectDelay = MyGame.Manager.ConfigManager.GlobalConfigData.SelectDelay;
		}

		public override void LoadContent()
		{
			MyGame.Manager.DebugManager.Reset();
			iconIndex = 0;

			flag1 = false;
			flag2 = true;

			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			UpdateTimer(gameTime);
			if (flag1)
			{
				if (Timer > selectDelay * 2)
				{
					flag1 = false;
					iconIndex = Convert.ToByte(flag1);
					MyGame.Manager.IconManager.UpdateFireIcon(iconIndex);
					return (Int32)ScreenType.Diff;
				}

				iconIndex = Convert.ToByte(flag1);
				MyGame.Manager.IconManager.UpdateFireIcon(iconIndex);
				return (Int32)CurrScreen;
			}

			
			if (Timer > promptDelay)
			{
				flag2 = !flag2;
				Timer -= promptDelay;
			}

			// Check fire first.
			Boolean fire = MyGame.Manager.InputManager.Fire();
			Boolean midd = MyGame.Manager.InputManager.CenterPos();
			if (fire || midd)
			{
				flag1 = true;
				Timer = 0;
			}

			// TODO check if tap the center or hit the fire button to proceed.
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

			if (flag2)
			{
				Engine.SpriteBatch.DrawString(Assets.EmulogicFont, Globalize.INSERT_COINS, promptPosition, Color.White);	
			}
		}

	}
}
