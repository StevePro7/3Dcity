﻿using System;
using WindowsGame.Common.Data;
using WindowsGame.Master.Factorys;
using WindowsGame.Common.Static;

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

			MyGame.Manager.LevelManager.SetLevelType(storagePersistData.LevelType);
			MyGame.Manager.LevelManager.SetLevelIndex(storagePersistData.LevelIndex);
			MyGame.Manager.ScoreManager.SetHighScore(storagePersistData.HighScore);
		}

		public void SaveContent()
		{
			if (null == storagePersistData)
			{
				storagePersistData = new StoragePersistData
				{
					HighScore = Constants.DEF_HIGH_SCORE,
					PlayAudio = MyGame.Manager.ConfigManager.GlobalConfigData.PlayAudio,
					LevelType = MyGame.Manager.ConfigManager.GlobalConfigData.LevelType,
					LevelIndex = MyGame.Manager.ConfigManager.GlobalConfigData.LevelIndex,
				};
			}
			else
			{
				storagePersistData.HighScore = MyGame.Manager.ScoreManager.HighScore;
				//storagePersistData.HighScore = Constants.DEF_HIGH_SCORE;	// Reset!
				storagePersistData.PlayAudio = MyGame.Manager.SoundManager.PlayAudio;
				storagePersistData.LevelType = MyGame.Manager.LevelManager.LevelType;
				storagePersistData.LevelIndex = MyGame.Manager.LevelManager.LevelIndex;
			}

			storageFactory.SaveContent(storagePersistData);
		}

	}
}
