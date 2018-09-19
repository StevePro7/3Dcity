using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IStateManager
	{
		void Initialize();
		void ToggleGameState();
		void ToggleGameSound();

		Boolean GamePause { get; }
		Boolean GameQuiet{ get; }
		//LevelType LevelType { get; }
	}

	public class StateManager : IStateManager
	{
		public void Initialize()
		{
			GamePause = false;
			//GameQuiet = false;
			GameQuiet = !MyGame.Manager.SoundManager.PlayAudio;
		}

		public void ToggleGameState()
		{
			GamePause = !GamePause;
		}

		public void ToggleGameSound()
		{
			GameQuiet = !GameQuiet;
		}

		//public LevelType LevelType { get; private set; }
		public Boolean GamePause { get; private set; }
		public Boolean GameQuiet { get; private set; }

	}
}
