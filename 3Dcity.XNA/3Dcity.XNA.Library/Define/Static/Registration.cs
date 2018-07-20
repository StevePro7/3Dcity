using WindowsGame.Define.Implementation;
using WindowsGame.Define.Interfaces;
using WindowsGame.Define.IoC;
using WindowsGame.Define.Managers;

namespace WindowsGame.Define.Static
{
	public static class Registration
	{
		public static void Initialize()
		{
			// Engine.
			IoCContainer.Initialize<IRandomManager, RandomManager>();
			IoCContainer.Initialize<IResolutionManager, ResolutionManager>();
			IoCContainer.Initialize<IStorageManager, StorageManager>();
			IoCContainer.Initialize<IThreadManager, ThreadManager>();

			IoCContainer.Initialize<IFileProxy, ProdFileProxy>();
			IoCContainer.Initialize<IFileManager, FileManager>();
		}

	}
}
