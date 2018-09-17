using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface ILevelManager 
	{
		void Initialize();
		void LoadContent();
		void SetLevelType(LevelType levelType);

		LevelType LevelType { get; }
		Byte LevelIndex { get; }
		String LevelName { get; }
	}

	public class LevelManager : ILevelManager 
	{
		public void Initialize()
		{
		}

		public void LoadContent()
		{
		}

		public void SetLevelType(LevelType levelType)
		{
			LevelType = levelType;
		}

		public LevelType LevelType { get; private set; }
		public Byte LevelIndex { get; private set; }
		public String LevelName { get; private set; }
	}
}
