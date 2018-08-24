using System;
using System.Collections.Generic;
using WindowsGame.Common.Screens;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Managers
{
	public interface IScreenManager 
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);
		void Draw();
	}

	public class ScreenManager : IScreenManager 
	{
		private IDictionary<Int32, IScreen> screens;
		private Int32 currScreen = (Int32)ScreenType.Splash;
		private Int32 nextScreen = (Int32)ScreenType.Splash;
		private Color color;

		public void Initialize()
		{
			screens = GetScreens();
			screens[(Int32)ScreenType.Splash].Initialize();
			screens[(Int32)ScreenType.Init].Initialize();
			color = GetColor();
		}

		public void LoadContent()
		{
			foreach (var key in screens.Keys)
			{
				if ((Int32)ScreenType.Splash == key || (Int32)ScreenType.Init == key)
				{
					continue;
				}

				screens[key].Initialize();
			}
		}

		public void Update(GameTime gameTime)
		{
			if (currScreen != nextScreen)
			{
				currScreen = nextScreen;
				screens[currScreen].LoadContent();
				color = GetColor();
			}

			nextScreen = screens[currScreen].Update(gameTime);
		}

		public void Draw()
		{
			MyGame.Manager.ResolutionManager.BeginDraw(color);
			Engine.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, MyGame.Manager.ResolutionManager.TransformationMatrix);
			screens[currScreen].Draw();
			Engine.SpriteBatch.End();
		}

		private Color GetColor()
		{
			return Color.Black;
		}

		private static Dictionary<Int32, IScreen> GetScreens()
		{
			return new Dictionary<Int32, IScreen>
			{
				{(Int32)ScreenType.Splash, new SplashScreen()},
				{(Int32)ScreenType.Init, new InitScreen()},
				{(Int32)ScreenType.Title, new TitleScreen()},
				{(Int32)ScreenType.Ready, new ReadyScreen()},
				{(Int32)ScreenType.Play, new PlayScreen()},
				{(Int32)ScreenType.DemoX, new DemoScreen()},
				{(Int32)ScreenType.Exit, new ExitScreen()},
				{(Int32)ScreenType.Test, new TestScreen()},
			};
		}

	}
}
