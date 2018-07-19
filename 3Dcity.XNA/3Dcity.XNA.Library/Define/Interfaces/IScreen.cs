using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Define.Interfaces
{
	public interface IScreen
	{
		void Initialize();
		void LoadContent();
		String Update(GameTime gameTime);
		void Draw();
	}
}
