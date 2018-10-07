using System;

namespace WindowsGame.Common.Data
{
	public struct LevelConfigData
	{
		public Byte LevelNo;
		public String LevelName;
		public Boolean BonusLevel;
		public Byte BulletMaximumNum;
		public UInt16 BulletFrameDelay;
		public UInt16 BulletShootDelay;
		public UInt16 StartTimer;
	}
}
