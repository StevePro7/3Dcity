using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface ICommandManager 
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);
		void Draw();
	}

	public class CommandManager : ICommandManager
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
