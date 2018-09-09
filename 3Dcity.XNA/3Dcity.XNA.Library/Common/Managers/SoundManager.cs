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
		//void ToggleGameSound();
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
			soundFactory.Initialize();
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
			//foreach (SoundEffectInstance soundEffect in Assets.SoundEffectDictionary.Values)
			//{
			//    soundFactory.PauseSoundEffect(soundEffect);
			//}
		}

		private void ResumeAllAudio()
		{
			ResumeMusic();
			//foreach (SoundEffectInstance soundEffect in Assets.SoundEffectDictionary.Values)
			//{
			//    soundFactory.ResumeSoundEffect(soundEffect);
			//}
		}

	}
}
