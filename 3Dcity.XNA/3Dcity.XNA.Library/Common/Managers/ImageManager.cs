using System;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IImageManager 
	{
		void LoadContent();

		// Sprite sheet #01.
		Rectangle[] GridRectangles { get; }
		Rectangle[] StarRectangles { get; }
		Rectangle BackRectangle { get; }
		Rectangle JoypadRectangle { get; }
		Rectangle[] JoyButtonRectangles { get; }
		Rectangle[] GameStateRectangles { get; }
		Rectangle[] GameSoundRectangles { get; }

		// Sprite sheet #02.
		Rectangle[][] ExplodeRectangles { get; }
		Rectangle[] EnemyRectangles { get; }
		Rectangle[] BulletRectangles { get; }
		Rectangle TargetBigRectangles { get; }
		Rectangle TargetSmallRectangles { get; }
	}

	public class ImageManager : IImageManager 
	{
		public void LoadContent()
		{
			const Byte halfSize = Constants.HalfSize;
			const Byte iconSize = Constants.IconSize;
			const Byte baseSize = Constants.BaseSize;
			const Byte dbleSize = Constants.DbleSize;
			const Byte enemySize = Constants.EnemySize;
			const Byte targetSize = Constants.TargetSize;
			const UInt16 left = Constants.ScreenWide + baseSize;

			// Sprite sheet #01.
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


			// Sprite sheet #02.

			// Explosions.
			ExplodeRectangles = new Rectangle[Constants.MAX_EXPLODE_TYPE][];
			ExplodeRectangles[(Byte)ExplodeType.Small] = new Rectangle[Constants.MAX_EXPLODE_FRAME];
			ExplodeRectangles[(Byte)ExplodeType.Big] = new Rectangle[Constants.MAX_EXPLODE_FRAME];
			UInt16 high = 0 * baseSize;
			for (Byte index = 0; index < Constants.MAX_EXPLODE_FRAME; index++)
			{
				ExplodeRectangles[(Byte)ExplodeType.Small][index] = new Rectangle(index * baseSize, high, baseSize, baseSize);
			}

			high = 1 * baseSize;
			const Byte halfExplode = Constants.MAX_EXPLODE_FRAME / 2;
			for (Byte index = 0; index < halfExplode; index++)
			{
				ExplodeRectangles[(Byte)ExplodeType.Big][index] = new Rectangle(index * dbleSize, high, dbleSize, dbleSize);
			}

			high = 3 * baseSize;
			for (Byte index = halfExplode; index < Constants.MAX_EXPLODE_FRAME; index++)
			{
				ExplodeRectangles[(Byte)ExplodeType.Big][index] = new Rectangle((index - halfExplode) * dbleSize, high, dbleSize, dbleSize);
			}

			// Enemies.
			high = (UInt16)(3 * baseSize + dbleSize);
			EnemyRectangles = new Rectangle[Constants.MAX_ENEMY];
			for (Byte index = 0; index < Constants.MAX_ENEMY; index++)
			{
				EnemyRectangles[index] = new Rectangle(index * enemySize, high, enemySize, enemySize);
			}


			// Bullets.
			high = (UInt16)(3 * baseSize + dbleSize + enemySize);
			BulletRectangles = new Rectangle[Constants.MAX_BULLET];
			for (Byte index = 0; index < Constants.MAX_BULLET; index++)
			{
				BulletRectangles[index] = new Rectangle(index * targetSize, high, targetSize, targetSize);
			}
			int x = 8;
		}

		// Sprite sheet #01.
		public Rectangle[] GridRectangles { get; private set; }
		public Rectangle[] StarRectangles { get; private set; }
		public Rectangle BackRectangle { get; private set; }
		public Rectangle JoypadRectangle { get; private set; }
		public Rectangle[] JoyButtonRectangles { get; private set; }
		public Rectangle[] GameStateRectangles { get; private set; }
		public Rectangle[] GameSoundRectangles { get; private set; }

		// Sprite sheet #02.
		public Rectangle[][] ExplodeRectangles { get; private set; }
		public Rectangle[] EnemyRectangles { get; private set; }
		public Rectangle[] BulletRectangles { get; private set; }
		public Rectangle TargetBigRectangles { get; private set; }
		public Rectangle TargetSmallRectangles { get; private set; }
	}
}
