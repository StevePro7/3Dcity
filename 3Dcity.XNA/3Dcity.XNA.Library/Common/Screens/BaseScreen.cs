using System;
using System.Collections.Generic;
using WindowsGame.Common.Objects;
using WindowsGame.Common.Static;
using WindowsGame.Master.Objects;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Screens
{
	public abstract class BaseScreen
	{
		protected UInt16 Timer { get; set; }
		protected IList<TextData> TextDataList { get; private set; }
		protected ScreenType CurrScreen { get; set; }
		protected ScreenType NextScreen { get; set; }
		protected Boolean GamePause { get; set; }

		public virtual void Initialize()
		{
			String screenName = GetType().Name.ToLower();
			screenName = screenName.Replace("screen", String.Empty);

			CurrScreen = (ScreenType)Enum.Parse(typeof(ScreenType), screenName, true);
		}

		public virtual void LoadContent()
		{
			Timer = 0;
		}

		public virtual Int32 Update(GameTime gameTime)
		{
			// Check if game is paused.
			Boolean gameState = MyGame.Manager.InputManager.GameState();
			if (gameState)
			{
				MyGame.Manager.StateManager.ToggleGameState();
				GamePause = MyGame.Manager.StateManager.GamePause;
				MyGame.Manager.SoundManager.GamePause(GamePause);

				BaseObject icon = MyGame.Manager.IconManager.GameState;
				MyGame.Manager.IconManager.ToggleIcon(icon);

				return (Int32)CurrScreen;
			}

			// If game paused then do not check for sound.
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			// Enable / disable sound.
			Boolean gameSound = MyGame.Manager.InputManager.GameSound();
			if (gameSound)
			{
				MyGame.Manager.StateManager.ToggleGameSound();
				Boolean gameQuiet = MyGame.Manager.StateManager.GameQuiet;
				MyGame.Manager.SoundManager.GameQuiet(gameQuiet);

				BaseObject icon = MyGame.Manager.IconManager.GameSound;
				MyGame.Manager.IconManager.ToggleIcon(icon);
			}

			// Update grid + stars.
			MyGame.Manager.RenderManager.UpdateStar(gameTime);
			MyGame.Manager.RenderManager.UpdateGrid(gameTime);

			return (Int32)CurrScreen;
		}


		protected void UpdateTimer(GameTime gameTime)
		{
			Timer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
		}

		public virtual void Draw()
		{
			// TODO remove!
			Master.Engine.Game.Window.Title = GetType().Name;

			MyGame.Manager.RenderManager.Draw();
			MyGame.Manager.IconManager.Draw();
		}

		protected void LoadTextData()
		{
			LoadTextData(GetType().Name);
		}

		//TODO delete unused method...
		//protected void LoadTextData(Byte suffix)
		//{
		//    String name = String.Format("{0}{1}", GetType().Name, suffix);
		//    LoadTextData(name);
		//}

		private void LoadTextData(String screen)
		{
			TextDataList = MyGame.Manager.TextManager.LoadTextData(screen);
		}

	}
}