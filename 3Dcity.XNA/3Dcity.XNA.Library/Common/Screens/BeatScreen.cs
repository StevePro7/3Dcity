using System;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class BeatScreen : BaseScreen, IScreen
	{
		public override void Initialize()
		{
			MyGame.Manager.DebugManager.Reset(CurrScreen);
			base.Initialize();
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			MyGame.Manager.EventManager.ClearEvents();
			
			// Move target unconditionally.
			Vector2 pos1 = MyGame.Manager.SpriteManager.LargeTarget.Position;
			Vector2 pos2 = MyGame.Manager.SpriteManager.SmallTarget.Position;

			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();
			MyGame.Manager.SpriteManager.SetMovement(horz, vert);
			MyGame.Manager.SpriteManager.Update(gameTime);

			Vector2 pos3 = MyGame.Manager.SpriteManager.LargeTarget.Position;
			Vector2 pos4 = MyGame.Manager.SpriteManager.SmallTarget.Position;

			if (pos1 != pos3)
			{
				MyGame.Manager.EventManager.AddLargeTargetMoveEvent(pos3);
			}
			if (pos2 != pos4)
			{
				MyGame.Manager.EventManager.AddSmallTargetMoveEvent(pos4);
			}

			//// Events.
			MyGame.Manager.EventManager.ProcessEvents(gameTime);

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			MyGame.Manager.SpriteManager.Draw();
		}

	}
}
