using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IRenderManager 
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);
		void Draw();
	}

	public class RenderManager : IRenderManager 
	{
		public void Initialize()
		{
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

	}
}
