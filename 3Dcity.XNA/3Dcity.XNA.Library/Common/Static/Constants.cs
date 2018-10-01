using System;

namespace WindowsGame.Common.Static
{
	public static class Constants
	{
		public const String CONTENT_DIRECTORY = "Content";

		public const String DATA_DIRECTORY = "Data";

		// TODO delete!
		//public const UInt16 TestFrameDelay = 400;

		// Global data.
		public const Boolean IsFixedTimeStep = true;
		//public const UInt32 FramesPerSecond = 100;

		public const Byte TextsSize = 20;

		public const SByte FontOffsetX = -1;
		public const SByte FontOffsetY = -4;

		public const Single GeneralTolerance = 0.1f;		//TODO make configurable?  Or set to 0.0f to take out!
		public const Single JoystickTolerance = 0.1f;	// Original is 0.4f;

		public const SByte INVALID_INDEX = -1;

		// Maximums.
		public const Byte MAX_LEVELS = 16;
		public const Byte MAX_GRID = 3;
		public const Byte MAX_STAR = 2;
		public const Byte MAX_MISSES = 4;

		public const Byte MAX_ENEMYS_SPAWN = 8;
		public const Byte MAX_ENEMYS_FRAME = 13;
		public const Byte MAX_BULLET_SHOOT = 10;
		public const Byte MAX_BULLET_FRAME = 6;
		public const Byte MAX_EXPLODE_TYPE = 2;
		public const Byte MAX_EXPLODE_SPAWN = 8;
		public const Byte MAX_EXPLODE_FRAME = 12;

		public static readonly UInt16[] ENEMY_OFFSET_X = new UInt16[MAX_ENEMYS_SPAWN] { 0, 160, 320, 480, 640, 190, 350, 510 };
		public static readonly UInt16[] ENEMY_OFFSET_Y = new UInt16[MAX_ENEMYS_SPAWN] { 80, 80, 80, 80, 80, 280, 280, 280 };
		public const Byte ENEMY_RANDOM_X = 32;
		public const Byte ENEMY_RANDOM_Y = 72;

		public static readonly SByte[] EXPLODE_OFFSET_X = new SByte[2] { 20, -20 };
		public static readonly SByte[] EXPLODE_OFFSET_Y = new SByte[2] { 20, -20 };

		public static readonly Byte[] CURSOR_OFFSET_X = new Byte[3] { 30, 80, 130 };

		public static readonly UInt16[] ENEMYS_SCORE = new UInt16[MAX_ENEMYS_SPAWN] { 1000, 500, 250, 100, 75, 50, 25, 10 };
		public const UInt32 MAX_HIGH_SCORE = 999999;
		public const UInt32 DEF_HIGH_SCORE = 100000;
		public const Byte MAX_NUM_MISSES = 4;

		public const Byte BOTTOM_SECTOR = 5;
		public const Byte BOTTOM_OFFSET = 190;
		public const UInt16 HALFWAY_DOWN = 280;

		// Sizes.
		public const Byte BorderSize = 4;
		public const Byte HalfSize = 40;
		public const Byte IconSize = 70;
		public const Byte BaseSize = 80;
		public const Byte DbleSize = 160;
		public const Byte TargetSize = 64;
		public const Byte EnemySize = 120;


		// Delimiters.
		public static readonly Char[] Delim0 = { ',' };
		public static readonly Char[] Delim1 = { '|' };
		public static readonly Char[] Delim2 = { ':' };


		// Custom data.
//#if WINDOWS && DEBUG
//        public const PlatformType PlatformType = Static.PlatformType.Desktop;
//        public const Boolean IsFullScreen = false;
//        public const Boolean IsMouseVisible = true;
//        public const UInt16 ScreenWide = 800;
//        public const UInt16 ScreenHigh = 480;
//        public const UInt16 GridHeight = 240;

//        public const Boolean UseExposed = true;
//        public const UInt16 ExposeWide = 800;
//        public const UInt16 ExposeHigh =  480;

//        public const Byte GameOffsetX = 0;
//        public const Byte GameOffsetY = 0;
//#endif

#if WINDOWS && DEBUG
		public const PlatformType PlatformType = Static.PlatformType.Desktop;
		public const Boolean IsFullScreen = false;
		public const Boolean IsMouseVisible = true;
		public const UInt16 ScreenWide = 800;
		public const UInt16 ScreenHigh = 600;// 480;
		public const UInt16 GridHeight = 240;

		public const Boolean UseExposed = true;
		public const UInt16 ExposeWide = 800;
		public const UInt16 ExposeHigh = 600;// 480;

		public const Byte GameOffsetX = 0;
		public const Byte GameOffsetY = 60;// 0
#endif

#if WINDOWS && !DEBUG
		public const PlatformType PlatformType = Static.PlatformType.Desktop;
		public const Boolean IsFullScreen = true;
		public const Boolean IsMouseVisible = true;
		public const UInt16 ScreenWide = 800;
		public const UInt16 ScreenHigh = 600;
		public const UInt16 GridHeight = 240;

		public const Boolean UseExposed = true;
		public const UInt16 ExposeWide = 800;
		public const UInt16 ExposeHigh = 600;

		public const Byte GameOffsetX = 0;
		public const Byte GameOffsetY = 60;
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
		public const UInt16 GridHeight = 240;

		public const Boolean UseExposed = false;
		public const UInt16 ExposeWide = 800;
		public const UInt16 ExposeHigh = 480;

		public const Byte GameOffsetX = 0;
		public const Byte GameOffsetY = 0;
#endif

	}
}