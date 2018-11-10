using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Define.Interfaces
{
	public interface IScreen
	{
		void Initialize();
		void LoadContent();
		Int32 Update(GameTime gameTime);
		void Draw();
	}
}
