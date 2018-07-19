using System;
using System.Collections.Generic;
using WindowsGame.Define.Objects;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface ITextManager 
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);
		void Draw();

		IList<TextData> LoadTextData(String screen);
	}

	public class TextManager : ITextManager 
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

		public IList<TextData> LoadTextData(String screen)
		{
			//return LoadTextData(screen, Constants.TextsSize, Constants.GameOffsetX, Constants.FontOffsetX, Constants.FontOffsetY);
			return null;
		}

	}
}
