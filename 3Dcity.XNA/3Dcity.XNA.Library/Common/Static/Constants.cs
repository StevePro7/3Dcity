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

		public const Single GeneralTolerance = 0.1f;		//TODO make configurable?  Or set to 0.0f to take out!
		public const Single JoystickTolerance = 0.1f;	// Original is 0.4f;

		public const Byte MAX_GRID = 3;
		public const Byte MAX_STAR = 2;

		// Custom data.
#if WINDOWS
		public const PlatformType PlatformType = Static.PlatformType.Desktop;
		public const Boolean IsFullScreen = false;
		public const Boolean IsMouseVisible = true;
		public const UInt16 ScreenWide = 800;
		public const UInt16 ScreenHigh = 480;
		public const UInt16 ScreenHalf = 240;

		public const Boolean UseExposed = true;
		public const UInt16 ExposeWide = 800;
		public const UInt16 ExposeHigh = 480;

		public const Byte GameOffsetX = 0;
#endif

		// IMPORTANT
		// Full screen on Windows seems to need 4/3 ratio
		// e.g. 640x480, 800x600 so allow for GameOffsetY

#if !WINDOWS
		public const PlatformType PlatformType = Static.PlatformType.Mobiles;
		public const Boolean IsFullScreen = true;
		public const Boolean IsMouseVisible = false;
		public const UInt16 ScreenWide = 800;
		public const UInt16 ScreenHigh = 480;
		public const UInt16 ScreenHalf = 240;

		public const Boolean UseExposed = false;
		public const UInt16 ExposeWide = 800;
		public const UInt16 ExposeHigh = 480;

		public const Byte GameOffsetX = 0;
#endif

	}
}