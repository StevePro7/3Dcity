using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Objects;
using Ninject.Syntax;

namespace WindowsGame.Common.Managers
{
	public interface ITextManager 
	{
		void Initialize();
		void Initialize(String contentRoot);

		IList<TextData> LoadTextData(String screen);
		IList<TextData> LoadTextData(String screen, Byte textsSize, UInt16 offsetX, UInt16 offsetY, Single fontX, Single fontY);
		Vector2 GetTextPosition(SByte x, SByte y);
		Vector2 GetTextPosition(SByte x, SByte y, Byte textsSize, UInt16 offsetX, UInt16 offsetY, Single fontX, Single fontY);

		void Draw(IEnumerable<TextData> textDataList);
		void Draw(TextData textData);
		void DrawCursor(Vector2 position);
		void DrawText(String text, Vector2 position);
		void DrawTitle();
		void DrawControls();
	}

	public class TextManager : ITextManager 
	{
		private String textFileRoot;

		private String titleText;
		private Vector2 titlePosition;
		private String[] controlText;
		private Vector2[] controlPosition;

		private static Char[] DELIM;
		private static Char[] PIPES;

		private const String TEXTS_DIRECTORY = "Texts";

		public void Initialize()
		{
			Initialize(String.Empty);
		}

		public void Initialize(String root)
		{
			DELIM = new[] { ',' };
			PIPES = new[] { '|' };

			textFileRoot = String.Format("{0}{1}/{2}/{3}", root, Constants.CONTENT_DIRECTORY, Constants.DATA_DIRECTORY, TEXTS_DIRECTORY);

			titleText = Globalize.GAME_TITLE;
			titlePosition = GetTextPosition(15, 1);

			controlText = new String[2] { Globalize.MOVE_TITLE, Globalize.FIRE_TITLE };
			controlPosition = new Vector2[2];
			controlPosition[0] = GetTextPosition(3, 23);
			controlPosition[1] = GetTextPosition(34, 23);
		}

		public IList<TextData> LoadTextData(String screen)
		{
			return LoadTextData(screen, Constants.TextsSize, Constants.GameOffsetX, Constants.GameOffsetY, Constants.FontOffsetX, Constants.FontOffsetY);
		}

		public IList<TextData> LoadTextData(String screen, Byte textsSize, UInt16 offsetX, UInt16 offsetY, Single fontX, Single fontY)
		{
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
				Vector2 postion = GetTextPosition(x, y, textsSize, offsetX, offsetY, fontX, fontY);
				TextData item = new TextData(postion, message, color);

				textDataList.Add(item);
			}

			return textDataList;
		}

		public Vector2 GetTextPosition(SByte x, SByte y)
		{
			return GetTextPosition(x, y, Constants.TextsSize, Constants.GameOffsetX, Constants.GameOffsetY, Constants.FontOffsetX, Constants.FontOffsetY);
		}
		public Vector2 GetTextPosition(SByte x, SByte y, Byte textsSize, UInt16 offsetX, UInt16 offsetY, Single fontX, Single fontY)
		{
			return new Vector2(x * textsSize + offsetX + fontX, y * textsSize + offsetY + fontY);
		}

		public void Draw(IEnumerable<TextData> textDataList)
		{
			foreach (TextData textData in textDataList)
			{
				Draw(textData);
			}
		}

		public void Draw(TextData textData)
		{
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, textData.Text, textData.Position, textData.Color);
		}

		public void DrawCursor(Vector2 position)
		{
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, Globalize.CURSOR_RIGHT, position, Color.White);
		}
		public void DrawText(String text, Vector2 position)
		{
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, text, position, Color.White);
		}
		public void DrawTitle()
		{
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, titleText, titlePosition, Color.White);
		}

		public void DrawControls()
		{
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, controlText[0], controlPosition[0], Color.White);
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, controlText[1], controlPosition[1], Color.White);
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

			return new Color(r, g, b);
		}

	}
}
