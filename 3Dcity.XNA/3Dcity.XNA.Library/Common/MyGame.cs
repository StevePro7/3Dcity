using Microsoft.Xna.Framework;
using WindowsGame.Common.TheGame;
using WindowsGame.Define;
using System;

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
		}

		public static void LoadContent()
		{
			//Engine.Game.IsFixedTimeStep = Constants.IsFixedTimeStep;
			//Engine.Game.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / Constants.FramesPerSecond);
			//Engine.Game.IsMouseVisible = Constants.IsMouseVisible;
			//Manager.ResolutionManager.LoadContent(Constants.IsFullScreen, Constants.ScreenWide, Constants.ScreenHigh, Constants.UseExposed, Constants.ExposeWide, Constants.ExposeHigh);
		}

		public static void LoadContentAsync()
		{
			GC.Collect();
		}

		public static void UnloadContent()
		{
			Engine.Game.Content.Unload();
		}

		public static void Update(GameTime gameTime)
		{
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