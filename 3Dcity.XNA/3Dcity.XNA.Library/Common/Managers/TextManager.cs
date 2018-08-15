using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Objects;

namespace WindowsGame.Common.Managers
{
	public interface ITextManager 
	{
		void Initialize();
		void Initialize(String contentRoot);
		void Draw();

		IList<TextData> LoadTextData(String screen);
		IList<TextData> LoadTextData(String screen, Byte textsSize, UInt32 offsetX, Single fontX, Single fontY);
		Vector2 GetTextPosition(SByte x, SByte y);
		Vector2 GetTextPosition(SByte x, SByte y, Byte textsSize, UInt32 offsetX, Single fontX, Single fontY);

		void Draw(IEnumerable<TextData> textDataList);
	}

	public class TextManager : ITextManager 
	{
		private String textFileRoot;

		private static Char[] DELIM;
		private static Char[] PIPES;

		private const String TEXTS_DIRECTORY = "Texts";

		public void Initialize()
		{
			//String contentRoot = MyGame.Manager.ContentManager.ContentRoot;
			//Initialize(contentRoot);
			Initialize(String.Empty);
		}

		public void Initialize(String root)
		{
			DELIM = new[] { ',' };
			PIPES = new[] { '|' };

			textFileRoot = String.Format("{0}{1}/{2}/{3}", root, Constants.CONTENT_DIRECTORY, Constants.DATA_DIRECTORY, TEXTS_DIRECTORY);
			//textFileRoot = String.Format("{0}/{1}/{2}", contentRoot, Constants.DATA_DIRECTORY, TEXTS_DIRECTORY);
		}

		public void Draw()
		{
		}

		public IList<TextData> LoadTextData(String screen)
		{
			return LoadTextData(screen, Constants.TextsSize, Constants.GameOffsetX, Constants.FontOffsetX, Constants.FontOffsetY);
		}

		public IList<TextData> LoadTextData(String screen, Byte textsSize, UInt32 offsetX, Single fontX, Single fontY)
		{
			//String file = GetTextFile(screen + ".txt");
			String file = String.Format("{0}/{1}.txt", textFileRoot, screen);
			var lines = MyGame.Manager.FileManager.LoadTxt(file);

			var textDataList = new List<TextData>();
			foreach (string line in lines)
			{
				if (line.StartsWith("--"))
				{
					continue;
				}

				String[] items = line.Split(DELIM);
				SByte x = Convert.ToSByte(items[0]);
				SByte y = Convert.ToSByte(items[1]);
				String message = items[2];

				Color color = Color.White;
				if (items.Length > 3)
				{
					color = ConvertFromHex(items[3]);
				}
				Vector2 postion = GetTextPosition(x, y, textsSize, offsetX, fontX, fontY);
				TextData item = new TextData(postion, message, color);

				textDataList.Add(item);
			}

			return textDataList;
		}

		public Vector2 GetTextPosition(SByte x, SByte y)
		{
			return GetTextPosition(x, y, Constants.TextsSize, Constants.GameOffsetX, Constants.FontOffsetX, Constants.FontOffsetY);
		}
		public Vector2 GetTextPosition(SByte x, SByte y, Byte textsSize, UInt32 offsetX, Single fontX, Single fontY)
		{
			return new Vector2(x * textsSize + offsetX + fontX, y * textsSize + fontY);
		}

		public void Draw(IEnumerable<TextData> textDataList)
		{
			foreach (TextData data in textDataList)
			{
				Engine.SpriteBatch.DrawString(Assets.EmulogicFont, data.Text, data.Position, data.Color);
			}
		}

		private static Color ConvertFromHex(String hexCode)
		{
			Color color = Color.White;
			if (hexCode.StartsWith("#"))
			{
				hexCode = hexCode.Substring(1, hexCode.Length - 1);
			}

			if (6 != hexCode.Length)
			{
				return color;
			}

			Byte r = Byte.Parse(hexCode.Substring(0, 2), NumberStyles.HexNumber);
			Byte g = Byte.Parse(hexCode.Substring(2, 2), NumberStyles.HexNumber);
			Byte b = Byte.Parse(hexCode.Substring(4, 2), NumberStyles.HexNumber);

			//Byte r = Convert.ToByte(hexCode.Substring(0, 2));
			//Byte g = Convert.ToByte(hexCode.Substring(2, 2));
			//Byte b = Convert.ToByte(hexCode.Substring(4, 2));

			return new Color(r, g, b);
		}

	}
}
