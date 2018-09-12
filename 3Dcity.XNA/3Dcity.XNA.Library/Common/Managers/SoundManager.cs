using System;
using WindowsGame.Common.Static;
using WindowsGame.Master.Factorys;
using Microsoft.Xna.Framework.Audio;

namespace WindowsGame.Common.Managers
{
	public interface ISoundManager 
	{
		void Initialize();

		void GamePause(Boolean gamePause);
		void GameQuiet(Boolean gameQuiet);
		void SetPlaySound(Boolean thePlaySound);

		void PlaySoundEffect();
		void PlayMusic();
		void PauseMusic();
		void ResumeMusic();
		void StopMusic();
	}

	public class SoundManager : ISoundManager 
	{
		private readonly ISoundFactory soundFactory;

		public SoundManager(ISoundFactory soundFactory)
		{
			this.soundFactory = soundFactory;
		}

		public void Initialize()
		{
			// TODO revert
			//soundFactory.Initialize();
			soundFactory.Initialize(false, false);
		}

		public void GamePause(Boolean gamePause)
		{
			if (gamePause)
			{
				PauseAllAudio();
			}
			else
			{
				Boolean gameQuiet = MyGame.Manager.StateManager.GameQuiet;
				if (!gameQuiet)
				{
					ResumeAllAudio();
				}
			}
		}

		public void GameQuiet(Boolean gameQuiet)
		{
			if (gameQuiet)
			{
				PauseAllAudio();
			}
			else
			{
				ResumeAllAudio();
			}

			SetPlaySound(!gameQuiet);
		}

		public void SetPlaySound(Boolean thePlaySound)
		{
			soundFactory.SetPlaySound(thePlaySound);
		}

		public void PlaySoundEffect()
		{
			SoundEffectType key = SoundEffectType.Funny;
			//SoundEffectType key = SoundEffectType.Cheat;
			SoundEffectInstance value = Assets.SoundEffectDictionary[key];
			soundFactory.PlaySoundEffect(value);
		}

		public void PlayMusic()
		{
			soundFactory.PlayMusic(Assets.GameMusicSong);
		}

		public void PauseMusic()
		{
			soundFactory.PauseMusic();
		}

		public void ResumeMusic()
		{
			soundFactory.ResumeMusic();
		}

		public void StopMusic()
		{
			soundFactory.StopMusic();
		}


		private void PauseAllAudio()
		{
			PauseMusic();
			if (!MyGame.Manager.ConfigManager.GlobalConfigData.LoadAudio)
			{
				return;
			}
			foreach (SoundEffectInstance soundEffect in Assets.SoundEffectDictionary.Values)
			{
				soundFactory.PauseSoundEffect(soundEffect);
			}
		}

		private void ResumeAllAudio()
		{
			ResumeMusic();
			if (!MyGame.Manager.ConfigManager.GlobalConfigData.LoadAudio)
			{
				return;
			}
			foreach (SoundEffectInstance soundEffect in Assets.SoundEffectDictionary.Values)
			{
				soundFactory.ResumeSoundEffect(soundEffect);
			}
		}

	}
}
