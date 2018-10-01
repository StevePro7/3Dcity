﻿using System;

namespace WindowsGame.Common.Managers
{
	public interface IStateManager
	{
		void Initialize();
		void ToggleGameState();
		void ToggleGameSound();
		void UpdateGameSound();

		void SetCoolMusic(Boolean coolMusic);
		void SetIsGodMode(Boolean isGodMode);

		Boolean GamePause { get; }
		Boolean GameQuiet { get; }
		Boolean CoolMusic { get; }
		Boolean IsGodMode { get; }
	}

	public class StateManager : IStateManager
	{
		public void Initialize()
		{
			GamePause = false;
			UpdateGameSound();
		}

		public void ToggleGameState()
		{
			GamePause = !GamePause;
		}

		public void ToggleGameSound()
		{
			GameQuiet = !GameQuiet;
		}

		public void UpdateGameSound()
		{
			GameQuiet = !MyGame.Manager.SoundManager.PlayAudio;
		}

		public void SetCoolMusic(Boolean coolMusic)
		{
			CoolMusic = coolMusic;
		}

		public void SetIsGodMode(Boolean isGodMode)
		{
			IsGodMode = isGodMode;
		}

		public Boolean GamePause { get; private set; }
		public Boolean GameQuiet { get; private set; }
		public Boolean CoolMusic { get; private set; }
		public Boolean IsGodMode { get; private set; }
	}
}
