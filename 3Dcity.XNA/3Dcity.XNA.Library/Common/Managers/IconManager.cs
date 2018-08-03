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
		void Update(GameTime gameTime);
		void Draw();

		// Properties.
		GameSound GameSound { get; }
		GameState GameState { get; }
	}

	public class IconManager : IIconManager
	{
		// Methods.
		public void Initialize()
		{
			const Byte offset = 100;

			GameState = new GameState();
			Vector2 statePosn = new Vector2(5, 4);
			Rectangle stateColl = new Rectangle(0, 0, offset, offset);
			GameState.Initialize(statePosn, stateColl);

			GameSound = new GameSound();
			Vector2 soundPosn = new Vector2(725, 4);
			Rectangle soundColl = new Rectangle(Constants.ScreenWide - offset, 0, offset, offset);
			GameSound.Initialize(soundPosn, soundColl);
		}

		public void LoadContent()
		{
			Texture2D[] theTextures = null;

			theTextures = new Texture2D[2] { Assets.PlayTexture, Assets.PauseTexture };
			GameState.LoadContent(theTextures);

			theTextures = new Texture2D[2] { Assets.SoundOnTexture, Assets.SoundOffTexture };
			GameSound.LoadContent(theTextures);
		}

		public void Update(GameTime gameTime)
		{
		}
		
		public void Draw()
		{
			GameState.Draw();
			GameSound.Draw();
		}

		// Properties.
		public GameSound GameSound { get; private set; }
		public GameState GameState { get; private set; }
	}
}
