﻿using System;

namespace WindowsGame.Common.Data
{
	public struct LevelConfigData
	{
		public Byte LevelNo;
		public String LevelName;
		public Boolean BonusLevel;
		public UInt16 GridDelay;
		public Byte BulletMaxim;
		public UInt16 BulletFrame;
		public UInt16 BulletShoot;
		public Byte EnemySpawn;
		public Byte EnemyTotal;

		public UInt16 EnemyFrameyDelay;
		public UInt16 EnemyFrameDelta;
		public UInt16 EnemyStartDelay;
		public UInt16 EnemyStartDelta;

		public Byte EnemySpeedNone;
		public Byte EnemySpeedWave;
		public Byte EnemySpeedFast;

		//public UInt16 EnemyDelay;
		//public UInt16 EnemyStart;
		public UInt16 ExplodeDelay;
		public UInt16 StartTimer;
		
	}
}
