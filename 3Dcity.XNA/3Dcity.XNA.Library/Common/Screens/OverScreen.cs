using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class OverScreen : BaseScreenSelect, IScreen
	{
		private UInt16 bigDelay, medDelay, smlDelay;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			//BackedPositions = new Vector2[2];
			//BackedPositions[0] = new Vector2(290, 215 + Constants.GameOffsetY);
			//BackedPositions[1] = new Vector2(290, 220 + Constants.GameOffsetY);

			CalcTwoBorders(290, 215, 220);

			bigDelay = MyGame.Manager.ConfigManager.GlobalConfigData.OverDelay;
			medDelay = 3000;
			smlDelay = Constants.SLIGHT_PAUSE*4;

			NextScreen = ScreenType.Title;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			base.LoadContent();

			Killspace = MyGame.Manager.StateManager.KillSpace;
			MyGame.Manager.SpriteManager.KillEnemy.SetPosition(Killspace);
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			UpdateTimer(gameTime);

			// Initial pause.
			if (Timer <= smlDelay)
			{
				return (Int32)CurrScreen;
			}

			if (!Flag1)
			{
				// Ensure sound effect once.
				MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Over);
				Flag1 = true;
			}

			if (Timer <= medDelay)
			{
				return (Int32) CurrScreen;
			}

			if (!Flag2)
			{
				// Ensure sound effect once.
				MyGame.Manager.SoundManager.PlayMusic(SongType.GameOver);
				Flag2 = true;
			}

			// Now can check to pro actively goto next screen.
			Boolean status = MyGame.Manager.InputManager.StatusBar();
			//Boolean center = MyGame.Manager.InputManager.CenterPos();
			const Boolean center = false;		// TODO perfect transition...

			// Time expired so advance.
			if (status || center || Timer > bigDelay)
			{
				MyGame.Manager.StateManager.SetKillSpace(Vector2.Zero);
				MyGame.Manager.ScoreManager.ResetMisses();
				MyGame.Manager.SoundManager.StopMusic();
				return (Int32) NextScreen;
			}

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			DrawSheet01();

			// Sprite sheet #02.
			MyGame.Manager.RenderManager.DrawStatusOuter();
			MyGame.Manager.RenderManager.DrawStatusInner(StatusType.Yellow, MyGame.Manager.EnemyManager.EnemyPercentage);

			// Draw dead enemy on instant death only.
			if (Vector2.Zero != Killspace)
			{
				MyGame.Manager.SpriteManager.KillEnemy.Draw();
			}

			DrawSheet02();
			MyGame.Manager.SpriteManager.LargeTarget.Draw();
			DrawBacked();

			// Text data last!
			DrawText();
			MyGame.Manager.TextManager.DrawProgress();
			MyGame.Manager.EnemyManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();
		}

	}
}
