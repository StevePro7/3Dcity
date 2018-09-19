using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MediaPlayerX = Microsoft.Xna.Framework.Media.MediaPlayer;

namespace WindowsGame.Master.Factorys
{
	public interface ISoundFactory
	{
		void Initialize();
		void Initialize(Boolean thePlayMusic, Boolean thePlaySound);

		void SetPlayMusic(Boolean playAudio);
		void SetPlaySound(Boolean playAudio);

		void PlayMusic(Song song);
		void PlayMusic(Song song, Boolean isRepeating);
		void StopMusic();
		void PauseMusic();
		void ResumeMusic();

		void PlaySoundEffect(SoundEffectInstance soundEffect);
		void PauseSoundEffect(SoundEffectInstance soundEffect);
		void ResumeSoundEffect(SoundEffectInstance soundEffect);
		void StopSoundEffect(SoundEffectInstance soundEffect);

		void SetMinVolume();
		void SetMaxVolume();
		void SetVolume(Single volume);
	}

	public class SoundFactory : ISoundFactory
	{
		private Boolean playSound;
		private Boolean playMusic;

		public void Initialize()
		{
			Initialize(true, true);
		}

		public void Initialize(Boolean thePlayMusic, Boolean thePlaySound)
		{
			playMusic = thePlayMusic;
			playSound = thePlaySound;
		}

		public void SetPlayMusic(Boolean playAudio)
		{
			playMusic = playAudio;
		}
		public void SetPlaySound(Boolean playAudio)
		{
			playSound = playAudio;
		}

		public void PlayMusic(Song song)
		{
			PlayMusic(song, true);
		}

		public void PlayMusic(Song song, Boolean isRepeating)
		{
			if (!playMusic)
			{
				return;
			}

			if (MediaState.Playing == MediaPlayerX.State)
			{
				return;
			}

			MediaPlayerX.Play(song);
			MediaPlayerX.IsRepeating = isRepeating;
		}

		public void PauseMusic()
		{
			if (MediaState.Playing == MediaPlayerX.State)
			{
				MediaPlayerX.Pause();
			}
		}

		public void ResumeMusic()
		{
			if (MediaState.Paused == MediaPlayerX.State)
			{
				MediaPlayerX.Resume();
			}
		}

		public void StopMusic()
		{
			if (MediaState.Playing == MediaPlayerX.State)
			{
				MediaPlayerX.Stop();
			}
		}

		public void PlaySoundEffect(SoundEffectInstance soundEffect)
		{
			if (!playSound)
			{
				return;
			}

			if (SoundState.Playing == soundEffect.State)
			{
				return;
			}

			soundEffect.Play();
		}

		public void PauseSoundEffect(SoundEffectInstance soundEffect)
		{
			if (SoundState.Playing == soundEffect.State)
			{
				soundEffect.Pause();
			}
		}

		public void ResumeSoundEffect(SoundEffectInstance soundEffect)
		{
			if (SoundState.Paused == soundEffect.State)
			{
				soundEffect.Resume();
			}
		}

		public void StopSoundEffect(SoundEffectInstance soundEffect)
		{
			if (SoundState.Playing == soundEffect.State)
			{
				soundEffect.Stop();
			}
		}

		public void SetMinVolume()
		{
			SetVolume(0);
		}

		public void SetMaxVolume()
		{
			SetVolume(100);
		}

		public void SetVolume(Single volume)
		{
			MediaPlayerX.Volume = volume;
		}

	}
}
