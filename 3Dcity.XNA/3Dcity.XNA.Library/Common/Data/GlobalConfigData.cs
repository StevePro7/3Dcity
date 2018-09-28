using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Data
{
	public struct GlobalConfigData
	{
		public ScreenType ScreenType;
		public Byte FramesPerSecond;
		public Byte MaximLevel;
		public Boolean LoadAudio;
		public Boolean PlayAudio;
		public LevelType LevelType;
		public Byte LevelIndex;
		public UInt16 SplashDelay;
		public UInt16 StarDelay;
		public UInt16 GridDelay;
		public UInt16 IntroDelay;
		public UInt16 SelectDelay;
		public UInt16 ScoreDelay;
		public Single EventRatio;
		public Boolean ScoreBlink;
		public Boolean UpdateStar;
		public Boolean UpdateGrid;
		public Boolean RenderBack;
		public Boolean RenderIcon;
		public Boolean AwesomeMusic;
		public UInt16 TargetX;
		public UInt16 TargetY;
		public UInt16 EnemysX;
		public UInt16 EnemysY;
		public Byte EnemyFrame;
		public Byte EnemyIndex;
		public Byte EnemySpawn;
		public Byte EnemyTotal;
		public Boolean IsGodMode;
		public Boolean QuitsToExit;
	}
}
