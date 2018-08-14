using System;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;
using Microsoft.Xna.Framework;
using WindowsGame.Master;

namespace WindowsGame.Common.Screens
{
	public class TestScreen : BaseScreen, IScreen
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
			return (Int32)ScreenType.Test;
		}

		public override void Draw()
		{
			base.Draw();

			MyGame.Manager.TextManager.Draw(TextDataList);
		}

		

	}
}
