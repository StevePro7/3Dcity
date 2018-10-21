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
		Kill,
		None
	}

	public enum SpeedType
	{
		None = 0,
		Wave = 1,
		Fast = 2
	}

	public enum SpawnType
	{
		Repeat,
		Spaced
	}

	public enum MoveType
	{
		None,
		Horz,
		Vert,
		Both
	}

	public enum ShipType
	{
		Ship = 0,
		Boss = 1
	}

	public enum ExplodeType
	{
		Small = 0,
		Big = 1
	}

	public enum StatusType
	{
		Black = 0,
		Yellow = 1,
		Red = 2,
		Blue = 3
	}

	public enum SongType
	{
		BossMusic,
		ContMusic,
		CoolMusic,
		GameMusic,
		GameOver,
		GameTitle
	}

	public enum SoundEffectType
	{
		Aaargh,
		Bonus,
		Boss,
		Cheat,
		Extra,
		Finish,
		Fire,
		Over,
		Ready,
		Right,
		Ship,
		Wrong,
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
		Intro,
		Begin,
		Diff,
		Level,
		Load,
		Ready,
		Play,
		Quit,
		Finish,
		Boss,
		Dead,
		Cont,
		Over,
		Resume,
		Beat,
		Demo,
		Exit,
		Test,
	}

}