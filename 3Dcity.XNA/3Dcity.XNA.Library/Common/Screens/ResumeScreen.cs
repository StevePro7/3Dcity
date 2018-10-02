using System;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class ResumeScreen : BaseScreen, IScreen
	{
		private UInt16 delay;
		private Byte goal;

		public override void Initialize()
		{
			delay = 1000;
			goal = 100;
			base.Initialize();
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			//base.Update(gameTime);
			//if (GamePause)
			//{
			//    return (Int32)CurrScreen;
			//}


			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			//base.Draw();
		}

	}
}
