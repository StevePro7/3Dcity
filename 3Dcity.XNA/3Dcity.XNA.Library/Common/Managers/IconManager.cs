using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame.Common.Objects;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Managers
{
	public interface IIconManager
	{
		// Methods.
		void Initialize();
		void LoadContent();

		// Properties.
		GameState GameSound { get; }
		GameState GameState { get; }
	}

	public class IconManager : IIconManager
	{
		// Methods.
		public void Initialize()
		{
			GameState = new GameState();
			Vector2 statePosn = new Vector2(5, 4);
			Rectangle stateColl = new Rectangle(0, 0, 100, 100);
			GameState.Initialize(statePosn, stateColl);
		}

		public void LoadContent()
		{
			Texture2D[] theTextures = new Texture2D[2] { Assets.PlayTexture, Assets.PauseTexture };
			GameState.LoadContent(theTextures);
		}

		// Properties.
		public GameState GameSound { get; private set; }
		public GameState GameState { get; private set; }
	}
}
