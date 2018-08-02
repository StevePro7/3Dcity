using WindowsGame.Common.Devices;
using WindowsGame.Common.Inputs;
using WindowsGame.Common.Inputs.Types;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;
using WindowsGame.Common.TheGame;
using WindowsGame.Define.Implementation;
//using WindowsGame.Define.Inputs;
using WindowsGame.Define.Interfaces;
using WindowsGame.Define.IoC;
using IJoystickInput = WindowsGame.Common.Inputs.Types.IJoystickInput;
using JoystickInput = WindowsGame.Common.Inputs.Types.JoystickInput;
//using MouseScreenInput = WindowsGame.Common.Inputs.Types.MouseScreenInput;
using TheRegistration = WindowsGame.Define.Static.Registration;
//using TouchScreenInput = WindowsGame.Common.Inputs.Types.TouchScreenInput;

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
			IoCContainer.Initialize<IIconManager, IconManager>();
			IoCContainer.Initialize<IImageManager, ImageManager>();
			IoCContainer.Initialize<IRenderManager, RenderManager>();
			IoCContainer.Initialize<IScoreManager, ScoreManager>();
			IoCContainer.Initialize<IScreenManager, ScreenManager>();
			IoCContainer.Initialize<ISoundManager, SoundManager>();
			IoCContainer.Initialize<ISpriteManager, SpriteManager>();
			IoCContainer.Initialize<IStateManager, StateManager>();
			IoCContainer.Initialize<IStorageManager, StorageManager>();
			IoCContainer.Initialize<ITextManager, TextManager>();
			IoCContainer.Initialize<IThreadManager, ThreadManager>();

			IoCContainer.Initialize<IJoystickInput, JoystickInput>();		// TODO move to engine!
			//IoCContainer.Initialize<IKeyboardInput, KeyboardInput>();
			//IoCContainer.Initialize<IMouseScreenInput, MouseScreenInput>();
			//IoCContainer.Initialize<ITouchScreenInput, TouchScreenInput>();

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