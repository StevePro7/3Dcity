using Microsoft.Xna.Framework;
using WindowsGame.Common.TheGame;
using WindowsGame.Common.Static;
using System;

namespace WindowsGame.Common
{
	public static class MyGame
	{
		public static void Construct(IGameManager manager)
		{
			Manager = manager;
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