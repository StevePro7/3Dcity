using System;
using WindowsGame.Engine.Interfaces;
using WindowsGame.Engine.IoC;
using WindowsGame.SystemTests.Implementation;
using NUnit.Framework;
using WindowsGame.Common;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Static;
using WindowsGame.Common.TheGame;

namespace WindowsGame.SystemTests
{
	public abstract class BaseSystemTests
	{
		protected ICollisionManager CollisionManager;
		protected IConfigManager ConfigManager;
		protected IContentManager ContentManager;
		protected IDeviceManager DeviceManager;
		protected IImageManager ImageManager;
		protected IInputManager InputManager;
		protected IRandomManager RandomManager;
		protected IResolutionManager ResolutionManager;
		protected IScoreManager ScoreManager;
		protected IScreenManager ScreenManager;
		protected ISoundManager SoundManager;
		protected ISpriteManager SpriteManager;
		protected IStorageManager StorageManager;
		protected ITextManager TextManager;
		protected IThreadManager ThreadManager;
		protected IFileManager FileManager;
		protected ILogger Logger;

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