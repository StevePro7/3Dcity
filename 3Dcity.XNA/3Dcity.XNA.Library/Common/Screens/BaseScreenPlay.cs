using System;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Screens
{
	public abstract class BaseScreenPlay : BaseScreen
	{
		protected LevelType levelType { get; private set; }
		protected Byte levelIndex { get; private set; }
		protected LevelConfigData levelConfigData { get; private set; }
		protected Boolean invincibile { get; private set; }
		protected Boolean checkLevelComplete { get; private set; }

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			base.Draw();
		}

	}
}