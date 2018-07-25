using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Common.TheGame;
using WindowsGame.Define;

namespace WindowsGame.Common
{
	public static class MyGame
	{
		public static void Construct(IGameManager manager)
		{
			Manager = manager;
		}

		public static void Initialize()
		{
			Manager.Logger.Initialize();

			// Load initial content then config!
			Manager.ContentManager.Initialize();
			Manager.ContentManager.LoadContentSplash();

			Manager.ConfigManager.Initialize();
			Manager.ConfigManager.LoadContent();

			Manager.ResolutionManager.Initialize();
			Manager.ScreenManager.Initialize();
			Manager.ThreadManager.Initialize();
		}

		public static void LoadContent()
		{
			Byte framesPerSecond = MyGame.Manager.ConfigManager.GlobalConfigData.FramesPerSecond;
			Engine.Game.IsFixedTimeStep = Constants.IsFixedTimeStep;
			Engine.Game.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / framesPerSecond);
			Engine.Game.IsMouseVisible = Constants.IsMouseVisible;
			Manager.ResolutionManager.LoadContent(Constants.IsFullScreen, Constants.ScreenWide, Constants.ScreenHigh, Constants.UseExposed, Constants.ExposeWide, Constants.ExposeHigh);
		}

		public static void LoadContentAsync()
		{
			Manager.InputManager.Initialize();
			Manager.ScoreManager.Initialize();
			Manager.RandomManager.Initialize();
			Manager.StorageManager.Initialize();
			Manager.TextManager.Initialize();

			Manager.CollisionManager.LoadContent();
			Manager.ContentManager.LoadContent();
			Manager.ImageManager.LoadContent();

			Manager.ScoreManager.LoadContent();
			Manager.ScreenManager.LoadContent();
			Manager.StorageManager.LoadContent();

			GC.Collect();
		}

		public static void UnloadContent()
		{
			Engine.Game.Content.Unload();
		}

		public static void Update(GameTime gameTime)
		{
			Manager.InputManager.Update(gameTime);

#if WINDOWS
			Boolean escape = Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape);
			//Boolean escape = Manager.InputManager.Escape();
			if (escape)
			{
				Engine.Game.Exit();
				return;
			}
#endif

			Manager.ScreenManager.Update(gameTime);
		}

		public static void Draw()
		{
			Manager.ScreenManager.Draw();
		}

		public static void OnActivated()
		{
		}
		public static void OnDeactivated()
		{

#if ANDROID
			Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
			System.Environment.Exit(0);
#endif
		}

		public static IGameManager Manager { get; private set; }
	}

}