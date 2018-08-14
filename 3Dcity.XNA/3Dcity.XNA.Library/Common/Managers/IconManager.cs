using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame.Common.Objects;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IIconManager
	{
		// Methods.
		void Initialize();
		void LoadContent();
		void ToggleIcon(BaseObject icon);
		void UpdateIcon(BaseObject icon, Byte index);
		//void Update(GameTime gameTime);
		void Draw();
		void DrawControls();

		// Properties.
		GameSound GameSound { get; }
		GameState GameState { get; }
		JoypadMove JoypadMove { get; }
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


			// Joystick controller.
			JoypadMove = new JoypadMove();
			Vector2 jpPos = new Vector2(20, 300);
			//Rectangle jpColl = new Rectangle(0, 280, 200, 200);
			//Rectangle jpColl = new Rectangle(-200, 80, 600, 600);
			Rectangle jpColl = new Rectangle(-100, 180, 400, 400);
			Rectangle jpBndl = new Rectangle(0, 280, 200, 200);
			JoypadMove.Initialize(jpPos, jpColl, jpBndl);

			// Joystick fire button.
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

			JoypadMove.LoadContent(Assets.JoypadTexture);

			theTextures = new Texture2D[2] { Assets.ButtonOnTexture, Assets.ButtonOffTexture };
			JoyButton.LoadContent(theTextures);
		}

		public void ToggleIcon(BaseObject icon)
		{
			icon.ToggleIcon();
		}
		public void UpdateIcon(BaseObject icon, Byte index)
		{
			icon.UpdateIcon(index);
		}

		//public void Update(GameTime gameTime)
		//{
		//}
		
		public void Draw()
		{
			GameState.Draw();
			GameSound.Draw();
		}

		public void DrawControls()
		{
			JoypadMove.Draw();
			JoyButton.Draw();
		}

		// Properties.
		public GameSound GameSound { get; private set; }
		public GameState GameState { get; private set; }
		public JoypadMove JoypadMove { get; private set; }
		public JoyButton JoyButton { get; private set; }
	}
}
