using System;
using WindowsGame.Common.Managers;
using WindowsGame.Engine.IoC;

namespace WindowsGame.Common.Static
{
	public static class Registration
	{
		public static void Initialize()
		{
			//IoCContainer.Initialize<IGameManager, GameManager>();

			IoCContainer.Initialize<ICollisionManager, CollisionManager>();
			IoCContainer.Initialize<IConfigManager, ConfigManager>();
			IoCContainer.Initialize<IContentManager, ContentManager>();
			IoCContainer.Initialize<IDeviceManager, DeviceManager>();
			IoCContainer.Initialize<IImageManager, ImageManager>();
			IoCContainer.Initialize<IInputManager, InputManager>();
			IoCContainer.Initialize<IRandomManager, RandomManager>();
			IoCContainer.Initialize<IResolutionManager, ResolutionManager>();
			IoCContainer.Initialize<IScoreManager, ScoreManager>();
			IoCContainer.Initialize<IScreenManager, ScreenManager>();
			IoCContainer.Initialize<ISoundManager, SoundManager>();
			IoCContainer.Initialize<ISpriteManager, SpriteManager>();
			IoCContainer.Initialize<IStorageManager, StorageManager>();
			IoCContainer.Initialize<ITextManager, TextManager>();
			IoCContainer.Initialize<IThreadManager, ThreadManager>();

//            IoCContainer.Initialize<IFileProxy, RealFileProxy>();
//            IoCContainer.Initialize<IFileManager, FileManager>();

//#if (WINDOWS && MOBILE)
//            IoCContainer.Initialize<IDeviceFactory, MobilesDeviceFactory>();
//            IoCContainer.Initialize<IInputFactory, WindowsInputFactory>();
//            IoCContainer.Initialize<ILogger, Logger.Implementation.RealLogger>();
//#elif WINDOWS
//            IoCContainer.Initialize<IDeviceFactory, WindowsDeviceFactory>();
//            IoCContainer.Initialize<IInputFactory, WindowsInputFactory>();
//            IoCContainer.Initialize<ILogger, Logger.Implementation.RealLogger>();
//#endif
//#if !WINDOWS
//            IoCContainer.Initialize<IDeviceFactory, MobilesDeviceFactory>();
//            IoCContainer.Initialize<IInputFactory, MobilesInputFactory>();
//            IoCContainer.Initialize<ILogger, Library.Implementation.TestLogger>();
//#endif
		}
	}
}