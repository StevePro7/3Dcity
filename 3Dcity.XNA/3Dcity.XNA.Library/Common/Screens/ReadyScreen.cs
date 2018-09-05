using System;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;
using Microsoft.Xna.Framework;
using WindowsGame.Master;
using Microsoft.Xna.Framework.Input.Touch;

namespace WindowsGame.Common.Screens
{
	public class ReadyScreen : BaseScreen, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);

			MyGame.Manager.EventManager.ClearEvents();

			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();
			MyGame.Manager.SpriteManager.SetMovement(horz, vert);

			MoveTarget(gameTime);


			//MyGame.Manager.EventManager.Update(gameTime);
			//MyGame.Manager.ExplosionManager.Update(gameTime);
			//MyGame.Manager.BulletManager.Update(gameTime);
			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			//base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			//MyGame.Manager.ExplosionManager.Draw();
			//MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.Draw();

			MyGame.Manager.TextManager.Draw(TextDataList);
		}

		private static void MoveTarget(GameTime gameTime)
		{
			Vector2 largeTargetPosBEF = MyGame.Manager.SpriteManager.BigTarget.Position;
			Vector2 smallTargetPosBEF = MyGame.Manager.SpriteManager.SmallTarget.Position;
			MyGame.Manager.SpriteManager.Update(gameTime);
			Vector2 largeTargetPosAFT = MyGame.Manager.SpriteManager.BigTarget.Position;
			Vector2 smallTargetPosAFT = MyGame.Manager.SpriteManager.SmallTarget.Position;

			if (largeTargetPosBEF != largeTargetPosAFT)
			{
				MyGame.Manager.EventManager.AddLargeTargetMoveEvent(largeTargetPosAFT);
			}
			if (smallTargetPosBEF != smallTargetPosAFT)
			{
				MyGame.Manager.EventManager.AddSmallTargetMoveEvent(smallTargetPosAFT);
			}
		}

	}
}
