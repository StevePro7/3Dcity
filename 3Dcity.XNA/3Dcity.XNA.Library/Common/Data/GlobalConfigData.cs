using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Data
{
	public struct GlobalConfigData
	{
		public ScreenType ScreenType;
		public LevelType LevelType;
		public Byte LevelIndex;
		public Byte FramesPerSecond;
		public Byte MaximLevel;
		public Byte MaxBullets;
		public Boolean LoadAudio;
		public Boolean PlayAudio;
		public UInt16 StarDelay;
		public UInt16 GridDelay;		// TODO delete
		public UInt16 ExplodeDelay;
		public UInt16 SplashDelay;
		public UInt16 SelectDelay;
		public UInt16 IntroDelay;
		public UInt16 TitleDelay;
		public UInt16 LoadDelay;
		public UInt16 ReadyDelay;
		public UInt16 DeadDelay;
		public UInt16 ScoreDelay;
		public Single EventRatio;
		public Boolean EnemyBlink;
		public Boolean ScoreBlink;
		public Boolean UpdateStar;
		public Boolean UpdateGrid;
		public Boolean RenderBack;
		public Boolean RenderIcon;
		public Boolean CoolMusic;
		public UInt16 TargetX;
		public UInt16 TargetY;
		public UInt16 EnemysX;
		public UInt16 EnemysY;
		//public Byte EnemyFrame;
		//public Byte EnemyIndex;
		//public Byte EnemySpawn;
		//public Byte EnemyTotal;
		public Boolean IsGodMode;
		public Boolean DebugTester;
		public Boolean DonotSave;
		public Boolean QuitsToExit;
	}
}
