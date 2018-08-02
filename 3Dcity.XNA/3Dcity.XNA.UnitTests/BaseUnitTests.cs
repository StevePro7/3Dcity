using WindowsGame.Common.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;
using WindowsGame.Common;
using WindowsGame.Common.Managers;
using WindowsGame.Common.TheGame;
using WindowsGame.Define.Interfaces;
using WindowsGame.Define.Managers;

namespace WindowsGame.UnitTests
{
	public abstract class BaseUnitTests
	{
		protected IBulletManager BulletManager;
		protected ICollisionManager CollisionManager;
		protected IConfigManager ConfigManager;
		protected IContentManager ContentManager;
		protected IControlManager ControlManager;
		protected IDeviceManager DeviceManager;
		protected IEnemyManager EnemyManager;
		protected IExplosionManager ExplosionManager;
		protected IIconManager IconManager;
		protected IImageManager ImageManager;
		protected IInputManager InputManager;
		protected IRandomManager RandomManager;
		protected IRenderManager RenderManager;
		protected IResolutionManager ResolutionManager;
		protected IScoreManager ScoreManager;
		protected IScreenManager ScreenManager;
		protected ISoundManager SoundManager;
		protected ISpriteManager SpriteManager;
		protected IStateManager StateManager;
		protected IStorageManager StorageManager;
		protected ITextManager TextManager;
		protected IThreadManager ThreadManager;
		protected IFileManager FileManager;
		protected ILogger Logger;

#pragma warning disable 618
		[TestFixtureSetUp]
#pragma warning restore 618
		public void TestFixtureSetUp()
		{
			BulletManager = MockRepository.GenerateStub<IBulletManager>();
			CollisionManager = MockRepository.GenerateStub<ICollisionManager>();
			ConfigManager = MockRepository.GenerateStub<IConfigManager>();
			ContentManager = MockRepository.GenerateStub<IContentManager>();
			ControlManager = MockRepository.GenerateStub<IControlManager>();
			DeviceManager = MockRepository.GenerateStub<IDeviceManager>();
			EnemyManager = MockRepository.GenerateStub<IEnemyManager>();
			ExplosionManager = MockRepository.GenerateStub<IExplosionManager>();
			IconManager = MockRepository.GenerateStub<IIconManager>();
			ImageManager = MockRepository.GenerateStub<IImageManager>();
			InputManager = MockRepository.GenerateStub<IInputManager>();
			RandomManager = MockRepository.GenerateStub<IRandomManager>();
			RenderManager = MockRepository.GenerateStub<IRenderManager>();
			ResolutionManager = MockRepository.GenerateStub<IResolutionManager>();
			ScoreManager = MockRepository.GenerateStub<IScoreManager>();
			ScreenManager = MockRepository.GenerateStub<IScreenManager>();
			SoundManager = MockRepository.GenerateStub<ISoundManager>();
			SpriteManager = MockRepository.GenerateStub<ISpriteManager>();
			StateManager = MockRepository.GenerateStub<IStateManager>();
			StorageManager = MockRepository.GenerateStub<IStorageManager>();
			TextManager = MockRepository.GenerateStub<ITextManager>();
			ThreadManager = MockRepository.GenerateStub<IThreadManager>();
			FileManager = MockRepository.GenerateStub<IFileManager>();
			Logger = MockRepository.GenerateStub<ILogger>();
		}

		protected void SetUp()
		{
			IGameManager manager = new GameManager
			(
				BulletManager,
				CollisionManager,
				ConfigManager,
				ContentManager,
				ControlManager,
				DeviceManager,
				EnemyManager,
				ExplosionManager,
				IconManager,
				ImageManager,
				InputManager,
				RandomManager,
				RenderManager,
				ResolutionManager,
				ScoreManager,
				ScreenManager,
				SoundManager,
				SpriteManager,
				StateManager,
				StorageManager,
				TextManager,
				ThreadManager,
				FileManager,
				Logger
			);

			MyGame.Construct(manager);
		}

#pragma warning disable 618
		[TestFixtureTearDown]
#pragma warning restore 618
		public void TestFixtureTearDown()
		{
			BulletManager = null;
			CollisionManager = null;
			ConfigManager = null;
			ContentManager = null;
			ControlManager = null;
			DeviceManager = null;
			EnemyManager = null;
			ExplosionManager = null;
			IconManager = null;
			ImageManager = null;
			InputManager = null;
			RandomManager = null;
			RenderManager = null;
			ResolutionManager = null;
			ScoreManager = null;
			ScreenManager = null;
			SoundManager = null;
			SpriteManager = null;
			StateManager = null;
			StorageManager = null;
			TextManager = null;
			ThreadManager = null;
			FileManager = null;
			Logger = null;
		}

	}
}
