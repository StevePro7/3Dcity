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
		public void Initialize()
		{
		}

		public void LoadContent()
		{
		}

		public void LoadLevel(Byte enemies)
		{
			
		}

		public void Update(GameTime gameTime)
		{
		}

		public void Draw()
		{
		}

	}
}
