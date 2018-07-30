using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;
using WindowsGame.Define.Interfaces;
using WindowsGame.Define.Managers;

namespace WindowsGame.Common.TheGame
{
	public interface IGameManager
	{
		IBulletManager BulletManager { get; }
		ICollisionManager CollisionManager { get; }
		IConfigManager ConfigManager { get; }
		IContentManager ContentManager { get; }
		IControlManager ControlManager { get; }
		IDeviceManager DeviceManager { get; }
		IEnemyManager EnemyManager { get; }
		IExplosionManager ExplosionManager { get; }
		IImageManager ImageManager { get; }
		IInputManager InputManager { get; }
		IRandomManager RandomManager { get; }
		IRenderManager RenderManager { get; }
		IResolutionManager ResolutionManager { get; }
		IScoreManager ScoreManager { get; }
		IScreenManager ScreenManager { get; }
		ISoundManager SoundManager { get; }
		ISpriteManager SpriteManager { get; }
		IStorageManager StorageManager { get; }
		ITextManager TextManager { get; }
		IThreadManager ThreadManager { get; }
		IFileManager FileManager { get; }
		ILogger Logger { get; }
	}

	public class GameManager : IGameManager
	{
		public GameManager
		(
			IBulletManager bulletManager,
			ICollisionManager collisionManager,
			IConfigManager configManager,
			IContentManager contentManager,
			IControlManager controlManager,
			IDeviceManager deviceManager,
			IEnemyManager enemyManager,
			IExplosionManager explosionManager,
			IImageManager imageManager,
			IInputManager inputManager,
			IRandomManager randomManager,
			IRenderManager renderManager,
			IResolutionManager resolutionManager,
			IScoreManager scoreManager,
			IScreenManager screenManager,
			ISoundManager soundManager,
			ISpriteManager spriteManager,
			IStorageManager storageManager,
			ITextManager textManager,
			IThreadManager threadManager,
			IFileManager fileManager,
			ILogger logger
		)
		{
			BulletManager = bulletManager;
			CollisionManager = collisionManager;
			ConfigManager = configManager;
			ContentManager = contentManager;
			ControlManager = controlManager;
			DeviceManager = deviceManager;
			EnemyManager = enemyManager;
			ExplosionManager = explosionManager;
			ImageManager = imageManager;
			InputManager = inputManager;
			RandomManager = randomManager;
			RenderManager = renderManager;
			ResolutionManager = resolutionManager;
			ScoreManager = scoreManager;
			ScreenManager = screenManager;
			SoundManager = soundManager;
			SpriteManager = spriteManager;
			StorageManager = storageManager;
			TextManager = textManager;
			ThreadManager = threadManager;
			FileManager = fileManager;
			Logger = logger;
		}

		public IBulletManager BulletManager { get; private set; }
		public ICollisionManager CollisionManager { get; private set; }
		public IConfigManager ConfigManager { get; private set; }
		public IContentManager ContentManager { get; private set; }
		public IControlManager ControlManager { get; private set; }
		public IDeviceManager DeviceManager { get; private set; }
		public IEnemyManager EnemyManager { get; private set; }
		public IExplosionManager ExplosionManager { get; private set; }
		public IImageManager ImageManager { get; private set; }
		public IInputManager InputManager { get; private set; }
		public IRandomManager RandomManager { get; private set; }
		public IRenderManager RenderManager { get; private set; }
		public IResolutionManager ResolutionManager { get; private set; }
		public IScoreManager ScoreManager { get; private set; }
		public IScreenManager ScreenManager { get; private set; }
		public ISoundManager SoundManager { get; private set; }
		public ISpriteManager SpriteManager { get; private set; }
		public IStorageManager StorageManager { get; private set; }
		public ITextManager TextManager { get; private set; }
		public IThreadManager ThreadManager { get; private set; }
		public IFileManager FileManager { get; private set; }
		public ILogger Logger { get; private set; }
	}
}