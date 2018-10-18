using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class TitleScreen : BaseScreen, IScreen
	{
		//private Vector2[] backedPositions;
		private Vector2 promptPosition;
		private UInt16 promptDelay;
		private UInt16 selectDelay;
		private Byte iconIndex;
		private Boolean flag1, flag2;

		public override void Initialize()
		{
			base.Initialize();

			promptPosition = MyGame.Manager.TextManager.GetTextPosition(14, 11);
			promptPosition.X -= 7.5f;
			promptDelay = MyGame.Manager.ConfigManager.GlobalConfigData.TitleDelay;
			selectDelay = MyGame.Manager.ConfigManager.GlobalConfigData.SelectDelay;

			BackedPositions = MyGame.Manager.StateManager.SetBackedPositions(270, 213, 375, 217);

			NextScreen = ScreenType.Diff;
			PrevScreen = ScreenType.Exit;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
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
				return (Int32) CurrScreen;
			}

			// Check to go back first.
			Boolean back = MyGame.Manager.InputManager.Back();
			if (back)
			{
				return (Int32)PrevScreen;
			}

			// Check for cheat detecction.
			//Boolean mode = MyGame.Manager.InputManager.TitleMode();

			// Check to go forward second.
			Boolean fire = MyGame.Manager.InputManager.Fire();
			Boolean left = MyGame.Manager.InputManager.LeftsSide();
			Boolean rght = MyGame.Manager.InputManager.RightSide();

			if (fire || left || rght)
			{
				MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Right);
				flag1 = true;
				Timer = 0;
			}

			UpdateTimer(gameTime);
			if (flag1)
			{
				if (Timer > selectDelay * 2)
				{
					flag1 = false;
					iconIndex = Convert.ToByte(flag1);
					MyGame.Manager.IconManager.UpdateFireIcon(iconIndex);
					return (Int32) NextScreen;
				}

				iconIndex = Convert.ToByte(flag1);
				MyGame.Manager.IconManager.UpdateFireIcon(iconIndex);
				return (Int32) CurrScreen;
			}

			if (Timer > promptDelay)
			{
				flag2 = !flag2;
				Timer -= promptDelay;
			}

			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();
			MyGame.Manager.RenderManager.DrawTitle();

			// Text data last!
			//MyGame.Manager.TextManager.DrawBuild();
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.TextManager.DrawGameInfo();
			MyGame.Manager.ScoreManager.Draw();

			if (flag2)
			{
				MyGame.Manager.RenderManager.DrawBorderPosition(BackedPositions);
				Engine.SpriteBatch.DrawString(Assets.EmulogicFont, Globalize.INSERT_COINS, promptPosition, Color.White);
			}
		}

	}
}
