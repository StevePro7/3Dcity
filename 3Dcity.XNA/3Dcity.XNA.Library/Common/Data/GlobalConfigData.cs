using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Data
{
	public struct GlobalConfigData
	{
		public ScreenType ScreenType;
		public Byte FramesPerSecond;
		public Boolean LoadAudio;
		public UInt16 SplashDelay;
		public UInt16 StarDelay;
		public UInt16 GridDelay;
		public UInt16 JoypadX;
		public UInt16 JoypadY;
		public Byte EnemyIndex;
		public Boolean QuitsToExit;
	}
}
