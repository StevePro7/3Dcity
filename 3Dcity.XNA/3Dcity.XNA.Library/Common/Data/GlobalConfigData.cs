using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Data
{
	public struct GlobalConfigData
	{
		public ScreenType ScreenType;
		public Byte FramesPerSecond;
		public UInt16 SplashDelay;
		public UInt16 JoypadX;
		public UInt16 JoypadY;
		public Boolean QuitsToExit;
	}
}