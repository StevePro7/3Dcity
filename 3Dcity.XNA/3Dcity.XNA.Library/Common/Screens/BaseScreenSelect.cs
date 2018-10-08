using System;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Screens
{
	public class BaseScreenSelect : BaseScreen
	{
		protected Vector2[] CursorPositions { get; set; }
		protected Vector2 SpritePosition { get; private set; }

		protected UInt16 SelectDelay { get; private set; }
		protected Boolean Selected { get; private set; }
		protected Boolean IsMoving { get; set; }
		protected Byte SelectType { get; set; }

		protected Byte IconIndex;
		protected Byte MoveIndex;
		protected Boolean Flag1;
		protected Boolean Flag2;

		private Vector2 spritePosition;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			SelectDelay = MyGame.Manager.ConfigManager.GlobalConfigData.SelectDelay;
			UpdateGrid = false;
		}

		public override void LoadContent()
		{
			base.LoadContent();

			IconIndex = 0;
			MoveIndex = 1;

			spritePosition = SpritePosition;
			spritePosition = MyGame.Manager.SpriteManager.SmallTarget.Position;
			spritePosition.X = Constants.CURSOR_OFFSET_X[MoveIndex];
			SpritePosition = spritePosition;

			Selected = false;
			IsMoving = false;
			Flag1 = Flag2 = false;
		}

		protected void UpdateFlag1(GameTime gameTime)
		{
			if (!Flag1)
			{
				return;
			}
			
			UpdateTimer(gameTime);
			if (Timer > SelectDelay * 2)
			{
				Flag1 = false;
				IconIndex = Convert.ToByte(Flag1);
				MyGame.Manager.IconManager.UpdateFireIcon(IconIndex);
				Selected = true;
				return;
				//ScreenType screenType = contType == 0 ? ScreenType.Play : ScreenType.Over;
				//return (Int32)screenType;
			}

			IconIndex = Convert.ToByte(Flag1);
			MyGame.Manager.IconManager.UpdateFireIcon(IconIndex);
		}

		protected void UpdateFlag2(GameTime gameTime)
		{
			if (!Flag2)
			{
				return;
			}

			IsMoving = true;
			UpdateTimer(gameTime);
			if (Timer <= SelectDelay)
			{
				return;
			}

			MoveIndex = 1;

			spritePosition = SpritePosition;
			spritePosition.X = Constants.CURSOR_OFFSET_X[MoveIndex];
			SpritePosition = spritePosition;
			MyGame.Manager.SpriteManager.SmallTarget.SetPosition(SpritePosition);

			Timer = 0;
			Flag2 = false;
		}

		protected void DetectFire()
		{
			// Check fire first.
			Boolean fire = MyGame.Manager.InputManager.Fire();
			if (fire)
			{
				Flag1 = true;
			}
		}

		protected void DetectMove()
		{
			// Check move second.
			Single horz = MyGame.Manager.InputManager.Horizontal();
			if (0 == horz)
			{
				return;
			}

			if (horz < 0)
			{
				MoveIndex = 0;
			}
			if (horz > 0)
			{
				MoveIndex = 2;
			}

			spritePosition = SpritePosition;
			spritePosition.X = Constants.CURSOR_OFFSET_X[MoveIndex];
			SpritePosition = spritePosition;
			MyGame.Manager.SpriteManager.SmallTarget.SetPosition(SpritePosition);
			SelectType = (Byte)(1 - SelectType);

			Flag2 = true;
		}


		protected void DrawSheet01()
		{
			MyGame.Manager.IconManager.DrawControls();
		}

		protected void DrawSheet02()
		{
			// Sprite sheet #02.
			MyGame.Manager.EnemyManager.Draw();
			MyGame.Manager.ExplosionManager.Draw();
			MyGame.Manager.LevelManager.Draw();
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.DrawCursor();
		}

		protected void DrawText()
		{
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}