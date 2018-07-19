using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IStorageManager 
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);
		void Draw();
	}

	public class StorageManager : IStorageManager 
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
