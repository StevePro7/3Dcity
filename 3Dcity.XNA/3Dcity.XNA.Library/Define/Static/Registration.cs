using WindowsGame.Define.Implementation;
using WindowsGame.Define.Interfaces;
using WindowsGame.Define.IoC;
using WindowsGame.Define.Managers;
using WindowsGame.Define.Factorys;

namespace WindowsGame.Define.Static
{
	public static class Registration
	{
		public static void Initialize()
		{
			// Factorys.
			IoCContainer.Initialize<IContentFactory, ContentFactory>();
			IoCContainer.Initialize<ISoundFactory, SoundFactory>();

			// Managers.
			IoCContainer.Initialize<IRandomManager, RandomManager>();
			IoCContainer.Initialize<IResolutionManager, ResolutionManager>();
			IoCContainer.Initialize<IStorageManager, StorageManager>();
			IoCContainer.Initialize<IThreadManager, ThreadManager>();

			IoCContainer.Initialize<IFileProxy, ProdFileProxy>();
			IoCContainer.Initialize<IFileManager, FileManager>();
		}

	}
}
