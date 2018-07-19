using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

namespace WindowsGame.Define.Managers
{
	public interface IStorageManager
	{
		void Initialize();
		void Initialize(String theFileName);
		T LoadContent<T>();
		void SaveContent<T>(T data);
	}

	public class StorageManager : IStorageManager
	{
		private IsolatedStorageFile storage;
		private String fileName;

		public void Initialize()
		{
			Initialize("GameData.xml");
		}

		public void Initialize(String theFileName)
		{
			fileName = theFileName;
		}

		public T LoadContent<T>()
		{
			T data = default(T);

			try
			{
				using (storage = GetUserStoreAsAppropriateForCurrentPlatform())
				{
					if (storage.FileExists(fileName))
					{
						using (IsolatedStorageFileStream fileStream = new IsolatedStorageFileStream(fileName, FileMode.Open, storage))
						{
							XmlSerializer serializer = new XmlSerializer(typeof(T));
							data = (T)serializer.Deserialize(fileStream);
						}
					}
				}
			}
			catch
			{
			}

			return data;
		}

		public void SaveContent<T>(T data)
		{
			try
			{
				using (storage = GetUserStoreAsAppropriateForCurrentPlatform())
				{
					using (IsolatedStorageFileStream fileStream = new IsolatedStorageFileStream(fileName, FileMode.Create, storage))
					{
						XmlSerializer serializer = new XmlSerializer(typeof(T));
						serializer.Serialize(fileStream, data);
					}
				}
			}
			catch
			{
			}
		}

		// http://blogs.msdn.com/b/shawnhar/archive/2010/12/16/isolated-storage-windows-and-clickonce.aspx
		private static IsolatedStorageFile GetUserStoreAsAppropriateForCurrentPlatform()
		{
#if WINDOWS
			return IsolatedStorageFile.GetUserStoreForDomain();
#else
			return IsolatedStorageFile.GetUserStoreForApplication();
#endif
		}

	}
}
