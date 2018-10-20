using System;
using WindowsGame.Common.Data;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class FinishScreen : BaseScreenSelect, IScreen
	{
		private Vector2 completePosition;
		private Vector2 hitRatioPosition;
		private String hitRatioText;
		private UInt16 timer1, timer2;
		private UInt16 promptDelay;
		private Vector2 homeSpot;
		private Single deltaX, deltaY;
		private Boolean flag;

		private const Single offset = 1.0f;
		private const Single multiplier = 0.6f;

		public override void Initialize()
		{
			base.Initialize();
			UpdateGrid = MyGame.Manager.ConfigManager.GlobalConfigData.UpdateGrid;

			promptDelay = MyGame.Manager.ConfigManager.GlobalConfigData.FlashDelay;
			homeSpot = MyGame.Manager.SpriteManager.LargeTarget.HomeSpot;

			BackedPositions = MyGame.Manager.StateManager.SetBackedPositions(250, 195, 370, 217);
			completePosition = MyGame.Manager.TextManager.GetTextPosition(13, 10);
			hitRatioPosition = MyGame.Manager.TextManager.GetTextPosition(23, 11);
			NextScreen = ScreenType.Load;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			base.LoadContent();

			MyGame.Manager.IconManager.UpdateFireIcon(0);
			MyGame.Manager.SpriteManager.SmallTarget.SetHomeSpot();

			Byte scoreKills = MyGame.Manager.ScoreManager.ScoreKills;
			Byte enemyTotal = MyGame.Manager.EnemyManager.EnemyTotal;
			Single hitRatio = scoreKills / (Single) enemyTotal;
			hitRatioText = hitRatio.ToString().PadLeft(3, '0');
			hitRatioText += Globalize.PERCENTAGE;

			deltaX = homeSpot.X - MyGame.Manager.SpriteManager.LargeTarget.Position.X;
			deltaY = homeSpot.Y - MyGame.Manager.SpriteManager.LargeTarget.Position.Y;

			timer1 = 0;
			timer2 = 0;
			Flag2 = true;
			flag = false;
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32) CurrScreen;
			}

			// Update bullets to finish off..
			MyGame.Manager.BulletManager.Update(gameTime);

			timer2 += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			if (timer2 > promptDelay)
			{
				Flag2 = !Flag2;
				timer2 -= promptDelay;
			}



			UpdateFlag1(gameTime);
			if (Selected)
			{
				MyGame.Manager.LevelManager.IncrementLevel();
				if (MyGame.Manager.LevelManager.LevelIndex >= MyGame.Manager.LevelManager.MaximLevel)
				{
					NextScreen = ScreenType.Beat;
				}
				//TODO delete as already level already set below...
				//MyGame.Manager.LevelManager.SetLevelType((LevelType)SelectType);
				return (Int32)NextScreen;
			}

			//DetectFire();
			Boolean fire = MyGame.Manager.InputManager.Fire();
			Boolean left = MyGame.Manager.InputManager.LeftsSide();
			Boolean rght = MyGame.Manager.InputManager.RightSide();
			if (fire || left || rght)
			{
				Flag1 = true;
			}
			if (Flag1)
			{
				PlaySoundEffect();
				return (Int32)CurrScreen;
			}


			if (flag)
			{
				return (Int32)CurrScreen;
			}


			Single deltaT = (Single)gameTime.ElapsedGameTime.TotalSeconds;
			Vector2 targetPosition = MyGame.Manager.SpriteManager.LargeTarget.Position;

			if (Math.Abs(homeSpot.X - targetPosition.X) > offset)
			{
				targetPosition.X += deltaX * deltaT * multiplier;
			}
			if (Math.Abs(homeSpot.Y - targetPosition.Y) > offset)
			{
				targetPosition.Y += deltaY * deltaT * multiplier;
			}

			MyGame.Manager.SpriteManager.LargeTarget.SetPosition(targetPosition);
			if (Math.Abs(homeSpot.X - targetPosition.X) <= offset && Math.Abs(homeSpot.Y - targetPosition.Y) <= offset)
			{
				MyGame.Manager.SoundManager.StopMusic();
				MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Cheat);
				UpdateGrid = false;
				flag = true;
			}


			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			DrawSheet01();

			// Sprite sheet #02.
			MyGame.Manager.RenderManager.DrawStatusOuter();
			MyGame.Manager.RenderManager.DrawStatusInner(StatusType.Yellow, MyGame.Manager.EnemyManager.EnemyPercentage);

			MyGame.Manager.EnemyManager.Draw();
			MyGame.Manager.LevelManager.Draw();
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.Draw();
			DrawBacked();

			// Text data last!
			DrawText();
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawProgress();
			MyGame.Manager.EnemyManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();
			
			MyGame.Manager.TextManager.DrawText(hitRatioText, hitRatioPosition);

			if (Flag1 || Flag2)
			{
				MyGame.Manager.TextManager.DrawText(Globalize.FINISH_TEXT1, completePosition);
			}
		}

	}
}
