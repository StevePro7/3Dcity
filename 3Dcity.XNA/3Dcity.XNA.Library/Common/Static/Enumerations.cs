namespace WindowsGame.Common.Static
{
	public enum PlatformType
	{
		Desktop = 0,
		Mobiles = 1
	}

	public enum CursorType
	{
		Left = 0,
		Right = 1
	}

	public enum LevelType
	{
		Easy = 0,
		Hard = 1
	}

	public enum EnemyType
	{
		Idle,
		Move,
		Test,
		Dead,
		None
	}

	public enum ExplodeType
	{
		Small = 0,
		Big = 1
	}

	public enum SongType
	{
		AwesomeMusic,
		BossMusic,
		ContMusic,
		GameMusic,
		GameOver,
		GameTitle
	}

	public enum SoundEffectType
	{
		Funny,
		Right,
		Wrong,
		Cheat,
		Early
	}

	public enum EventType
	{
		LargeTargetMove,
		SmallTargetMove,
	}

	public enum SpriteType
	{
		LargeTarget,
		SmallTarget,
		Bullet,
		Enemy,
		Explosion
	}

	public enum ScreenType
	{
		Splash,
		Init,
		Title,
		Diff,
		Level,
		Ready,
		Play,
		Cont,
		Over,
		Demo,
		Exit,
		Test,
	}

}