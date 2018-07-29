using WindowsGame.Common.Devices;
using WindowsGame.Common.Inputs;
using WindowsGame.Common.Inputs.Types;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;
using WindowsGame.Common.TheGame;
using WindowsGame.Define.Implementation;
using WindowsGame.Define.Interfaces;
using WindowsGame.Define.IoC;
using TheRegistration = WindowsGame.Define.Static.Registration;

namespace WindowsGame.Common.Static
{
	public static class Registration
	{
		public static void Initialize()
		{
			// Initialize engine first.
			TheRegistration.Initialize();

			IoCContainer.Initialize<IGameManager, GameManager>();

			IoCContainer.Initialize<IBulletManager, BulletManager>();
			IoCContainer.Initialize<ICollisionManager, CollisionManager>();
			IoCContainer.Initialize<IConfigManager, ConfigManager>();
			IoCContainer.Initialize<IContentManager, ContentManager>();
			IoCContainer.Initialize<IControlManager, ControlManager>();
			IoCContainer.Initialize<IDeviceManager, DeviceManager>();
			IoCContainer.Initialize<IEnemyManager, EnemyManager>();
			IoCContainer.Initialize<IExplosionManager, ExplosionManager>();
			IoCContainer.Initialize<IImageManager, ImageManager>();
			//IoCContainer.Initialize<IInputManager, InputManager>();
			IoCContainer.Initialize<IRenderManager, RenderManager>();
			IoCContainer.Initialize<IScoreManager, ScoreManager>();
			IoCContainer.Initialize<IScreenManager, ScreenManager>();
			IoCContainer.Initialize<ISoundManager, SoundManager>();
			IoCContainer.Initialize<ISpriteManager, SpriteManager>();
			IoCContainer.Initialize<IStorageManager, StorageManager>();
			IoCContainer.Initialize<ITextManager, TextManager>();
			IoCContainer.Initialize<IThreadManager, ThreadManager>();

			IoCContainer.Initialize<IJoystickInput, JoystickInput>();
			IoCContainer.Initialize<IKeyboardInput, KeyboardInput>();
			IoCContainer.Initialize<IMouseScreenInput, MouseScreenInput>();
			IoCContainer.Initialize<ITouchScreenInput, TouchScreenInput>();

#if WINDOWS
			IoCContainer.Initialize<IDeviceFactory, WindowsDeviceFactory>();
			IoCContainer.Initialize<IInputManager, DesktopInputManager>();
			IoCContainer.Initialize<ILogger, ProdLogger>();
#endif

#if !WINDOWS
			IoCContainer.Initialize<IDeviceFactory, MobilesDeviceFactory>();
			IoCContainer.Initialize<IInputManager, MobilesInputManager>();
			IoCContainer.Initialize<ILogger, TestLogger>();
#endif

//            IoCContainer.Initialize<IDeviceFactory, MobilesDeviceFactory>();
//            IoCContainer.Initialize<IInputFactory, WindowsInputFactory>();
//            IoCContainer.Initialize<ILogger, Logger.Implementation.RealLogger>();
//#elif WINDOWS
//            IoCContainer.Initialize<IDeviceFactory, WindowsDeviceFactory>();
//            IoCContainer.Initialize<IInputFactory, WindowsInputFactory>();
//            IoCContainer.Initialize<ILogger, ProdLogger>();
//#endif
//#if !WINDOWS
//            IoCContainer.Initialize<IDeviceFactory, MobilesDeviceFactory>();
//            IoCContainer.Initialize<IInputFactory, MobilesInputFactory>();
//            IoCContainer.Initialize<ILogger, Library.Implementation.TestLogger>();
//#endif

		}
	}
}