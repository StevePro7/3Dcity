using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Interfaces
{
	public interface IInputFactory
	{
		void Initialize();
		void Update(GameTime gameTime);

		Single Horizontal();
		Single Vertical();
		Boolean Escape();
	}
}