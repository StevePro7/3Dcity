using System;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IBossManager 
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);
		void Draw();
		void DrawProgress();
	}

	public class BossManager : IBossManager
	{
		private Vector2 progressPosition;
		private String bossProgressText;

		public void Initialize()
		{
			progressPosition = MyGame.Manager.TextManager.GetTextPosition(25, 23);
			bossProgressText = "[100%]";
		}

		public void LoadContent()
		{
		}

		public void Update(GameTime gameTime)
		{
		}

		public void Draw()
		{
		}

		public void DrawProgress()
		{
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, bossProgressText, progressPosition, Color.White);
		}

	}
}
