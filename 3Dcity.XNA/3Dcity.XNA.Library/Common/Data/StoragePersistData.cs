﻿using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Data
{
	public class StoragePersistData
	{
		// High score.
		public UInt32 HighScore;

		// General game.
		public Boolean PlayAudio;
		public LevelType LevelType;
		public Byte LevelIndex;
		//public UInt32 HighScore;
	}
}