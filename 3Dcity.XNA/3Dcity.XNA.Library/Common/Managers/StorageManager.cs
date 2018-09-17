using System;
using WindowsGame.Common.Data;
using WindowsGame.Master.Factorys;

namespace WindowsGame.Common.Managers
{
	public interface IStorageManager
	{
		void Initialize();
		void Initialize(String fileName);

		void LoadContent();
		void SaveContent();
	}

	public class StorageManager : IStorageManager
	{
		private readonly IStorageFactory storageFactory;
		private StoragePersistData storagePersistData;

		public StorageManager(IStorageFactory storageFactory)
		{
			this.storageFactory = storageFactory;
		}

		public void Initialize()
		{
			Initialize("GameData.xml");
		}

		public void Initialize(String fileName)
		{
			storageFactory.Initialize(fileName);
		}

		public void LoadContent()
		{
			storagePersistData = storageFactory.LoadContent<StoragePersistData>();

			if (null == storagePersistData)
			{
				return;
			}

			//Boolean playAudio = storagePersistData.PlayAudio;
			//Boolean playAudio = true;
			//MyGame.Manager.StateManager.SetGameSound(playAudio);
			//MyGame.Manager.SoundManager.SetPlayAudio(playAudio);
		}

		public void SaveContent()
		{
			if (null == storagePersistData)
			{
				return;
			}

			//if (null == storagePersistData)
			//{
			//    storagePersistData = new StoragePersistData
			//    {
			//        PlayAudio = MyGame.Manager.ConfigManager.GlobalConfigData.PlayAudio
			//    };
			//}
			//else
			//{
			//    storagePersistData.PlayAudio = MyGame.Manager.StateManager.GameSound;
			//}

			storageFactory.SaveContent(storagePersistData);
		}

	}
}
