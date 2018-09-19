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

		void SetPlayAudio(Boolean playAudio);

		void PlaySoundEffect();
		void PlayMusic();
		void PauseMusic();
		void ResumeMusic();
		void StopMusic();

		Boolean PlayAudio { get; }
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
			PlayAudio = MyGame.Manager.ConfigManager.GlobalConfigData.PlayAudio;

			// TODO revert, refactor, etc.
			if (MyGame.Manager.ConfigManager.GlobalConfigData.LoadAudio && PlayAudio)
			{
				soundFactory.Initialize();
			}
			else
			{
				soundFactory.Initialize(PlayAudio, PlayAudio);
			}
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

			SetPlayAudio(!gameQuiet);
		}

		public void SetPlayAudio(Boolean playAudio)
		{
			PlayAudio = playAudio;
			soundFactory.SetPlayMusic(playAudio);
			soundFactory.SetPlaySound(playAudio);
		}

		public void PlaySoundEffect()
		{
			if (null == Assets.SoundEffectDictionary)
			{
				return;
			}

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
			if (!MyGame.Manager.ConfigManager.GlobalConfigData.PlayAudio)
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
			if (!MyGame.Manager.ConfigManager.GlobalConfigData.PlayAudio)
			{
				return;
			}

			foreach (SoundEffectInstance soundEffect in Assets.SoundEffectDictionary.Values)
			{
				soundFactory.ResumeSoundEffect(soundEffect);
			}
		}

		public Boolean PlayAudio { get; private set; }
	}
}
