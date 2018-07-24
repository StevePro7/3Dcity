using System;

namespace WindowsGame.Common.Static
{
	public static class Constants
	{
		public const String CONTENT_DIRECTORY = "Content";

		public const String DATA_DIRECTORY = "Data";
		

		// Global data.
		public const Boolean IsFixedTimeStep = true;
		public const UInt32 FramesPerSecond = 100;

		public const Byte TextsSize = 20;

		public const SByte FontOffsetX = -1;
		public const SByte FontOffsetY = -4;

		// Custom data.
#if WINDOWS
		public const Boolean IsFullScreen = false;
		public const Boolean IsMouseVisible = true;
		public const UInt16 ScreenWide = 960;//800;
		public const UInt16 ScreenHigh = 640;//480;

		public const Boolean UseExposed = true;
		public const UInt16 ExposeWide = 960;//800;
		public const UInt16 ExposeHigh = 640;//480;

		public const Byte GameOffsetX = 0;
#endif

#if !WINDOWS
		public const Boolean IsFullScreen = true;
		public const Boolean IsMouseVisible = false;
		public const UInt16 ScreenWide = 960;//800;
		public const UInt16 ScreenHigh = 640;//480;

		public const Boolean UseExposed = false;
		public const UInt16 ExposeWide = 960;//800;
		public const UInt16 ExposeHigh = 640;//480;

		public const Byte GameOffsetX = 0;
#endif

	}
}