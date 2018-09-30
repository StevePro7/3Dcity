using System;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IImageManager 
	{
		void LoadContent();

		// Sprite sheet #01.
		Rectangle BackRectangle { get; }
		Rectangle[] GridRectangles { get; }
		Rectangle[] StarRectangles { get; }

		Rectangle TitleRectangle { get; }
		Rectangle JoypadRectangle { get; }
		Rectangle[] JoyButtonRectangles { get; }
		Rectangle[] GameStateRectangles { get; }
		Rectangle[] GameSoundRectangles { get; }
		Rectangle BottomRectangle { get; }

		// Sprite sheet #02.
		Rectangle[][] ExplodeRectangles { get; }
		Rectangle[] EnemyRectangles { get; }
		Rectangle[] BulletRectangles { get; }
		Rectangle[] OrbDiffRectangles { get; }
		Rectangle TargetLargeRectangle { get; }
		Rectangle TargetSmallRectangle { get; }

		Rectangle OrbEasyRectangle { get; }
		Rectangle OrbHardRectangle { get; }
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
			const UInt16 left = Constants.ScreenWide + targetSize;

			// Sprite sheet #01.
			GridRectangles = new Rectangle[Constants.MAX_GRID];
			GridRectangles[0] = new Rectangle(0, Constants.GridHeight * 1, Constants.ScreenWide, Constants.GridHeight);
			GridRectangles[1] = new Rectangle(0, Constants.GridHeight * 2, Constants.ScreenWide, Constants.GridHeight);
			GridRectangles[2] = new Rectangle(0, Constants.GridHeight * 3, Constants.ScreenWide, Constants.GridHeight);

			StarRectangles = new Rectangle[Constants.MAX_STAR];
			StarRectangles[0] = new Rectangle(0, 80, Constants.ScreenWide, halfSize);
			StarRectangles[1] = new Rectangle(0, 960, Constants.ScreenWide, halfSize);

			BackRectangle = new Rectangle(0, Constants.GridHeight * 0, Constants.ScreenWide, Constants.GridHeight);

			TitleRectangle = new Rectangle(Constants.ScreenWide, 0, 224, 160);
			JoypadRectangle = new Rectangle(left, baseSize * 2, dbleSize, dbleSize);
			JoyButtonRectangles = new Rectangle[2];
			JoyButtonRectangles[0] = new Rectangle(left + (0 * baseSize), baseSize * 4, baseSize, baseSize);
			JoyButtonRectangles[1] = new Rectangle(left + (1 * baseSize), baseSize * 4, baseSize, baseSize);

			GameStateRectangles = new Rectangle[2];
			GameStateRectangles[0] = new Rectangle(left + (0 * baseSize), baseSize * 5, iconSize, iconSize);
			GameStateRectangles[1] = new Rectangle(left + (1 * baseSize), baseSize * 5, iconSize, iconSize);

			GameSoundRectangles = new Rectangle[2];
			GameSoundRectangles[0] = new Rectangle(left + (0 * baseSize), baseSize * 6, iconSize, iconSize);
			GameSoundRectangles[1] = new Rectangle(left + (1 * baseSize), baseSize * 6, iconSize, iconSize);

			BottomRectangle = new Rectangle(Constants.ScreenWide, (2 * baseSize), targetSize, Constants.ScreenWide);

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
			EnemyRectangles = new Rectangle[Constants.MAX_ENEMYS_SPAWN];
			for (Byte index = 0; index < Constants.MAX_ENEMYS_SPAWN; index++)
			{
				EnemyRectangles[index] = new Rectangle(index * enemySize, high, enemySize, enemySize);
			}


			// Targets.
			const UInt16 wide = enemySize * Constants.MAX_ENEMYS_SPAWN;
			high = 0;
			//const UInt16 wide = (UInt16)(Constants.MAX_BULLET_FRAME * targetSize);
			TargetLargeRectangle = new Rectangle(wide, high, targetSize, targetSize);
			TargetSmallRectangle = new Rectangle(wide + 12, high + targetSize + 12, halfSize, halfSize);

			// Bullets.
			high = 2 * targetSize;
			BulletRectangles = new Rectangle[Constants.MAX_BULLET_FRAME];
			for (Byte index = 0; index < Constants.MAX_BULLET_FRAME; index++)
			{
				BulletRectangles[index] = new Rectangle(wide, high + index * targetSize, targetSize, targetSize);
			}

			// Orbs.
			OrbDiffRectangles = new Rectangle[2];
			high = (2 + 6) * targetSize;
			OrbEasyRectangle = new Rectangle(wide, high + (0 * targetSize), targetSize, targetSize);
			OrbHardRectangle = new Rectangle(wide, high + (1 * targetSize), targetSize, targetSize);

			OrbDiffRectangles[(Byte)LevelType.Easy] = new Rectangle(wide, high + (0 * targetSize), targetSize, targetSize);
			OrbDiffRectangles[(Byte)LevelType.Hard] = new Rectangle(wide, high + (1 * targetSize), targetSize, targetSize);
		}

		// Sprite sheet #01.
		public Rectangle BackRectangle { get; private set; }
		public Rectangle[] GridRectangles { get; private set; }
		public Rectangle[] StarRectangles { get; private set; }
		public Rectangle TitleRectangle { get; private set; }
		public Rectangle JoypadRectangle { get; private set; }
		public Rectangle[] JoyButtonRectangles { get; private set; }
		public Rectangle[] GameStateRectangles { get; private set; }
		public Rectangle[] GameSoundRectangles { get; private set; }
		public Rectangle BottomRectangle { get; private set; }

		// Sprite sheet #02.
		public Rectangle[][] ExplodeRectangles { get; private set; }
		public Rectangle[] EnemyRectangles { get; private set; }
		public Rectangle[] BulletRectangles { get; private set; }
		public Rectangle[] OrbDiffRectangles { get; private set; }
		public Rectangle TargetLargeRectangle { get; private set; }
		public Rectangle TargetSmallRectangle { get; private set; }
		public Rectangle OrbEasyRectangle { get; private set; }
		public Rectangle OrbHardRectangle { get; private set; }
	}
}
