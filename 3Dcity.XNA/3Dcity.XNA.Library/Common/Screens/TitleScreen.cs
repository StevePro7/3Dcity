using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class TitleScreen : BaseScreen, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			// Not bad settings for default
			MyGame.Manager.BulletManager.Reset(5, 200, 100);
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			MyGame.Manager.CollisionManager.ClearBulletCollisionList();

			// Move target unconditionally.
			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();
			MyGame.Manager.SpriteManager.SetMovement(horz, vert);
			MyGame.Manager.SpriteManager.Update(gameTime);

			Boolean fire = MyGame.Manager.InputManager.Fire();
			if (fire)
			{
				SByte bulletIndex = MyGame.Manager.BulletManager.CheckBullets();
				if (Constants.INVALID_INDEX != bulletIndex)
				{
					Vector2 position = MyGame.Manager.SpriteManager.LargeTarget.Position;
					MyGame.Manager.BulletManager.Shoot((Byte)bulletIndex, position);
				}
				//    Vector2 position = MyGame.Manager.SpriteManager.LargeTarget.Position;
				//    MyGame.Manager.BulletManager.Fire(position);
			}

			//Byte myIndex = Convert.ToByte(fire);
			//MyGame.Manager.IconManager.UpdateIcon(MyGame.Manager.IconManager.JoyButton, myIndex);

			// Then bullet and target second.
			MyGame.Manager.BulletManager.Update(gameTime);
			if (MyGame.Manager.CollisionManager.BulletCollisionList.Count > 0)
			{
				// Check collisions here.
			}

			// Update fire icon.
			Byte fireIcon = Convert.ToByte(!MyGame.Manager.BulletManager.CanShoot);
			MyGame.Manager.IconManager.UpdateIcon(MyGame.Manager.IconManager.JoyButton, fireIcon);

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			//MyGame.Manager.TextManager.Draw(TextDataList);

			// Sprite sheet #02.


			// Then bullet and target second.
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.Draw();
		}

	}
}
