using System;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IImageManager 
	{
		void LoadContent();

		Rectangle[] GridRectangles { get; }
		Rectangle[] StarRectangles { get; }
		Rectangle BackRectangle { get; }

		Rectangle JoypadRectangle { get; }
		Rectangle[] JoyButtonRectangles { get; }
		Rectangle[] GameStateRectangles { get; }
		Rectangle[] GameSoundRectangles { get; }
	}

	public class ImageManager : IImageManager 
	{
		public void LoadContent()
		{
			const Byte halfSize = 40;
			const Byte iconSize = 70;
			const Byte baseSize = 80;
			const Byte dbleSize = 160;
			const UInt16 left = Constants.ScreenWide + baseSize;

			GridRectangles = new Rectangle[Constants.MAX_GRID];
			GridRectangles[0] = new Rectangle(0, Constants.ScreenHalf * 1, Constants.ScreenWide, Constants.ScreenHalf);
			GridRectangles[1] = new Rectangle(0, Constants.ScreenHalf * 2, Constants.ScreenWide, Constants.ScreenHalf);
			GridRectangles[2] = new Rectangle(0, Constants.ScreenHalf * 3, Constants.ScreenWide, Constants.ScreenHalf);

			StarRectangles = new Rectangle[Constants.MAX_STAR];
			StarRectangles[0] = new Rectangle(Constants.ScreenWide + halfSize * 0, dbleSize, halfSize, Constants.ScreenWide);
			StarRectangles[1] = new Rectangle(Constants.ScreenWide + halfSize * 1, dbleSize, halfSize, Constants.ScreenWide);

			BackRectangle = new Rectangle(0, Constants.ScreenHalf * 0, Constants.ScreenWide, Constants.ScreenHalf);


			JoypadRectangle = new Rectangle(Constants.ScreenWide, 0, dbleSize, dbleSize);
			JoyButtonRectangles = new Rectangle[2];
			JoyButtonRectangles[0] = new Rectangle(left, baseSize * 2, baseSize, baseSize);
			JoyButtonRectangles[1] = new Rectangle(left, baseSize * 3, baseSize, baseSize);

			GameStateRectangles = new Rectangle[2];
			GameStateRectangles[0] = new Rectangle(left, baseSize * 4, iconSize, iconSize);
			GameStateRectangles[1] = new Rectangle(left, baseSize * 5, iconSize, iconSize);

			GameSoundRectangles = new Rectangle[2];
			GameSoundRectangles[0] = new Rectangle(left, baseSize * 6, iconSize, iconSize);
			GameSoundRectangles[1] = new Rectangle(left, baseSize * 7, iconSize, iconSize);
		}

		public Rectangle[] GridRectangles { get; private set; }
		public Rectangle[] StarRectangles { get; private set; }
		public Rectangle BackRectangle { get; private set; }

		public Rectangle JoypadRectangle { get; private set; }
		public Rectangle[] JoyButtonRectangles { get; private set; }
		public Rectangle[] GameStateRectangles { get; private set; }
		public Rectangle[] GameSoundRectangles { get; private set; }
	}
}
