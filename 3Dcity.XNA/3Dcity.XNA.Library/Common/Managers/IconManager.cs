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
		JoyButton JoyButton { get; }
	}

	public class IconManager : IIconManager
	{
		// Methods.
		public void Initialize()
		{
			const Byte gameOffset = 100;
			const Byte fireOffset = 200;

			GameState = new GameState();
			Vector2 statePosn = new Vector2(5, 4);
			Rectangle stateColl = new Rectangle(0, 0, gameOffset, gameOffset);
			GameState.Initialize(statePosn, stateColl);

			GameSound = new GameSound();
			Vector2 soundPosn = new Vector2(725, 4);
			Rectangle soundColl = new Rectangle(Constants.ScreenWide - gameOffset, 0, gameOffset, gameOffset);
			GameSound.Initialize(soundPosn, soundColl);


			JoyButton = new JoyButton();
			Vector2 firePosn = new Vector2(Constants.ScreenWide - 80 - (2 * 20), Constants.ScreenHigh - 80 - (1 * 20));
			Rectangle fireColl = new Rectangle(Constants.ScreenWide - fireOffset, Constants.ScreenHigh - fireOffset, fireOffset, fireOffset);
			JoyButton.Initialize(firePosn, fireColl);
		}

		public void LoadContent()
		{
			Texture2D[] theTextures = null;

			theTextures = new Texture2D[2] { Assets.PlayTexture, Assets.PauseTexture };
			GameState.LoadContent(theTextures);

			theTextures = new Texture2D[2] { Assets.SoundOnTexture, Assets.SoundOffTexture };
			GameSound.LoadContent(theTextures);

			theTextures = new Texture2D[2] { Assets.ButtonOnTexture, Assets.ButtonOffTexture };
			JoyButton.LoadContent(theTextures);
		}

		public void Update(GameTime gameTime)
		{
		}
		
		public void Draw()
		{
			GameState.Draw();
			GameSound.Draw();
			JoyButton.Draw();

		}

		// Properties.
		public GameSound GameSound { get; private set; }
		public GameState GameState { get; private set; }
		public JoyButton JoyButton { get; private set; }
	}
}
