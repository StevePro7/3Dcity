using System;
using WindowsGame.Common.Static;
using WindowsGame.Define.Interfaces;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Screens
{
	public class TitleScreen : BaseScreen, IScreen 
	{
		public override void Initialize()
		{
			base.Initialize();
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public Int32 Update(GameTime gameTime)
		{
			return (Int32)ScreenType.Title;
		}

		public override void Draw()
		{
			base.Draw();
		}

	}
}