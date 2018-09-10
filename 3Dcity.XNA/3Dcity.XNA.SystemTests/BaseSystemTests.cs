using System;
using WindowsGame.Common.Interfaces;
using NUnit.Framework;
using WindowsGame.Common;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Static;
using WindowsGame.Common.TheGame;
using WindowsGame.Master.Interfaces;
using WindowsGame.Master.IoC;
using WindowsGame.Master.Managers;
using WindowsGame.SystemTests.Master.Implementation;

namespace WindowsGame.SystemTests
{
	public abstract class BaseSystemTests
	{
		protected IBulletManager BulletManager;
		protected ICollisionManager CollisionManager;
		protected ICommandManager CommandManager;
		protected IConfigManager ConfigManager;
		protected IContentManager ContentManager;
		protected IControlManager ControlManager;
		protected IDeviceManager DeviceManager;
		protected IEnemyManager EnemyManager;
		protected IEventManager EventManager;
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

		// mklink /D D:\3Dcity.XNA.Content D:\SVN\3Dcity\3Dcity.XNA\3Dcity.XNA\3Dcity.XNA\bin\x86\Debug\
		// mklink /D E:\3Dcity.XNA.Content E:\GitHub\StevePro7\3Dcity\3Dcity.XNA\3Dcity.XNA\3Dcity.XNA\bin\x86\Debug\
		protected const String CONTENT_ROOT = @"E:\3Dcity.XNA.Content\";

#pragma warning disable 618
		[TestFixtureSetUp]
#pragma warning restore 618
		public void TestFixtureSetUp()
		{
			Registration.Initialize();
			IoCContainer.Initialize<IFileProxy, TestFileProxy>();

			IGameManager manager = GameFactory.Resolve();
			MyGame.Construct(manager);
		}

#pragma warning disable 618
		[TestFixtureTearDown]
#pragma warning restore 618
		public void TestFixtureTearDown()
		{
			GameFactory.Release();
		}

	}
}