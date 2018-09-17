using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Screens;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Back
{
	public class PlayScreenBAK: BaseScreen, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			MyGame.Manager.SoundManager.PlayMusic();
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();

			Boolean fire = MyGame.Manager.InputManager.Fire();
			if (fire)
			{
				//MyGame.Manager.SoundManager.PlaySoundEffect();

				// get max number of bullets from bullet MGR
				// check if at least one bullet available...
				// OR if fire delay still running then true!
				// otherwise fire = false;

				Vector2 position = MyGame.Manager.SpriteManager.LargeTarget.Position;
				MyGame.Manager.BulletManager.Shoot(1, position);
			}

			Byte index = Convert.ToByte(fire);
			MyGame.Manager.IconManager.UpdateIcon(MyGame.Manager.IconManager.JoyButton, index);

			//MyGame.Manager.BulletManager.Update(gameTime);
			//Boolean isFiring = MyGame.Manager.BulletManager.IsFiring;
			//if (!isFiring)
			//{
			//        Vector2 position = MyGame.Manager.SpriteManager.BigTarget.Position;
			//        MyGame.Manager.BulletManager.Fire(position);
			//}

			MyGame.Manager.BulletManager.Update(gameTime);


			//if (Math.Abs(horz) > 0.4)
			//{
			MyGame.Manager.SpriteManager.SetMovement(horz, vert);
			MyGame.Manager.SpriteManager.Update(gameTime);
			//}

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			MyGame.Manager.TextManager.Draw(TextDataList);

			// Sprite sheet #02.
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.Draw();
		}

	}
}
