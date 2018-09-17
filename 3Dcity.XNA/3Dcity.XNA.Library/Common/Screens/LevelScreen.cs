using System;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class LevelScreen : BaseScreen, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();
			//LoadTextData();
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			base.Draw();
		}

	}
}
