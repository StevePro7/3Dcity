namespace WindowsGame.Common.Static
{
	public enum PlatformType
	{
		Desktop = 0,
		Mobiles = 1
	}

	public enum LevelType
	{
		Easy = 0,
		Hard = 1
	}

	public enum BulletType
	{
		Idle,
		Fire,
		Kill
	}

	public enum ExplodeType
	{
		Small = 0,
		Big = 1
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
		Ready,
		Play,
		Demo,
		Exit,
		Test,
	}

}