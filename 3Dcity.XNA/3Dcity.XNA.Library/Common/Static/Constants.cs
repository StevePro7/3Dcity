﻿using System;

namespace WindowsGame.Common.Static
{
	public static class Constants
	{
		public const String CONTENT_DIRECTORY = "Content";

		public const String DATA_DIRECTORY = "Data";
		

		// Global data.
		public const Boolean IsFixedTimeStep = true;
		public const UInt32 FramesPerSecond = 100;

		// Custom data.
		public const Boolean IsFullScreen = false;
		public const Boolean IsMouseVisible = true;

		public const UInt16 ScreenWide = 640;
		public const UInt16 ScreenHigh = 640;

		public const Boolean UseExposed = true;
		public const UInt16 ExposeWide = 640;
		public const UInt16 ExposeHigh = 640;

		public const Byte GameOffsetX = 0;
	}
}