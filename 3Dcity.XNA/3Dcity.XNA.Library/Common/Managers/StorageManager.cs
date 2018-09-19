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

			MyGame.Manager.SoundManager.SetPlayAudio(storagePersistData.PlayAudio);
			MyGame.Manager.StateManager.UpdateGameSound();
		}

		public void SaveContent()
		{
			//if (null == storagePersistData)
			//{
			//    return;
			//}

			if (null == storagePersistData)
			{
				storagePersistData = new StoragePersistData
				{
					PlayAudio = MyGame.Manager.ConfigManager.GlobalConfigData.PlayAudio
					//LevelType = MyGame.Manager.LevelManager.LevelType;
				};
			}
			//else
			//{
			//    storagePersistData.PlayAudio = MyGame.Manager.StateManager.GameSound;
			//}

			storagePersistData.PlayAudio = MyGame.Manager.SoundManager.PlayAudio;
			storageFactory.SaveContent(storagePersistData);
		}

	}
}
