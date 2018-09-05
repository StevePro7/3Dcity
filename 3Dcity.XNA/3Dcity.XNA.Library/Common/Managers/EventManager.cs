using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IEventManager
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);
		void Draw();
	}

	public class EventManager : IEventManager
	{
		private Single timer;

		public void Initialize()
		{
		}

		public void LoadContent()
		{
		}

		public void Update(GameTime gameTime)
		{
			//Single delta = (Single)Math.Round(gameTime.ElapsedGameTime.TotalSeconds, 2);
			Single delta = (Single)gameTime.ElapsedGameTime.TotalSeconds;
			timer += delta;
			timer = (Single)Math.Round(timer, 2);
			//if (0 == eventTypeData.Count)
			//{
			//    return;
			//}

			MyGame.Manager.Logger.Info(timer.ToString());
		}

		public void Draw()
		{
		}

	}
}
