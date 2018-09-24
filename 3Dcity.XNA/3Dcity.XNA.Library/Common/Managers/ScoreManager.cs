using System;
using System.Runtime.Remoting.Messaging;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Objects;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IScoreManager 
	{
		void Initialize();
		void LoadContent();
		void Reset();
		void Update(GameTime gameTime);
		void Draw();

		void UpdateGameScore(Byte index);
		void SetHighScore(UInt32 score);

		// Properties.
		UInt32 HighScore { get; }
		Byte MissesTotal { get; }
		Byte ScoreAvoid { get; }
		Byte ScoreKills { get; }
	}

	public class ScoreManager : IScoreManager
	{
		private Vector2 gameScorePosition;
		private Vector2 highScorePosition;
		private TextData[] missTextData;
		private TextData textData;
		private String gameScoreText;
		private String highScoreText;
		private UInt32 gameScore;
		private UInt16 scoreDelay;
		private UInt16 scoreTimer;
		private Boolean scoreFlag;
		private Boolean scoreBlink;

		public void Initialize()
		{
			scoreDelay = MyGame.Manager.ConfigManager.GlobalConfigData.ScoreDelay;
			SetHighScore(Constants.DEF_HIGH_SCORE);
		}

		public void LoadContent()
		{
			gameScorePosition = MyGame.Manager.TextManager.GetTextPosition(8, 1);
			highScorePosition = MyGame.Manager.TextManager.GetTextPosition(31, 1);
			missTextData = GetMissTextDataList();
			Vector2 scorePosition = MyGame.Manager.TextManager.GetTextPosition(4, 1);
			textData = new TextData(scorePosition, Globalize.PLAYER_FLASH, Color.Yellow);
			scoreBlink = MyGame.Manager.ConfigManager.GlobalConfigData.ScoreBlink;
		}

		public void Reset()
		{
			//HighScore = 10000;
			MissesTotal = 0;
			gameScore = 0;
			gameScoreText = GetGameScoreText();
			scoreTimer = 0;
			scoreFlag = true;
		}

		public void Update(GameTime gameTime)
		{
			if (!scoreBlink)
			{
				return;
			}

			scoreTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			if (scoreTimer >= scoreDelay)
			{
				scoreTimer -= scoreDelay;
				scoreFlag = !scoreFlag;
			}
		}

		public void Draw()
		{
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, gameScoreText, gameScorePosition, Color.White);
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, highScoreText, highScorePosition, Color.White);

			for (Byte index = 0; index < MissesTotal; index++)
			{
				MyGame.Manager.TextManager.Draw(missTextData[index]);
			}

			if (scoreFlag)
			{
				MyGame.Manager.TextManager.Draw(textData);
			}
		}

		public void UpdateGameScore(Byte index)
		{
			gameScore += Constants.ENEMYS_SCORE[index];
			if (gameScore >= Constants.MAX_HIGH_SCORE)
			{
				gameScore = Constants.MAX_HIGH_SCORE;
			}
			gameScoreText = GetGameScoreText();

			if (gameScore > HighScore)
			{
				HighScore = gameScore;
				highScoreText = GetHighScoreText();
			}
		}
		public void SetHighScore(UInt32 score)
		{
			HighScore = score;
			highScoreText = GetHighScoreText();
		}

		private TextData[] GetMissTextDataList()
		{
			TextData[] data = new TextData[Constants.MAX_MISSES];
			for (Byte index = 0; index < Constants.MAX_MISSES; index++)
			{
				data[index] = GetMissTextData((Byte)(11 - index));
			}

			return data;
		}

		private static TextData GetMissTextData(Byte y)
		{
			Vector2 position = MyGame.Manager.TextManager.GetTextPosition(39, (SByte)y);
			return new TextData(position, Globalize.MISS_TEXT);
		}

		private String GetGameScoreText()
		{
			return gameScore.ToString().PadLeft(5, '0');
		}
		private String GetHighScoreText()
		{
			return HighScore.ToString().PadLeft(5, '0');
		}

		public UInt32 HighScore { get; private set; }
		public Byte MissesTotal { get; private set; }
		public Byte ScoreAvoid { get; private set; }
		public Byte ScoreKills { get; private set; }
	}
}
