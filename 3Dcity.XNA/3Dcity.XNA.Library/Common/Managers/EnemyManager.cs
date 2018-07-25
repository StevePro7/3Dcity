using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IEnemyManager 
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);
		void Draw();
	}

	public class EnemyManager : IEnemyManager 
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
