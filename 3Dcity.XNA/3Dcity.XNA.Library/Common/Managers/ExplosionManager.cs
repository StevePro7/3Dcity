using System;
using System.Collections.Generic;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IExplosionManager 
	{
		void Initialize();
		void LoadContent(Byte explodeIndex, ExplodeType explodeType);
		void Reset(Byte theBulletShoot, UInt16 frameDelay);
		void Update(GameTime gameTime);
		void Draw();

		IList<Explosion> ExplosionList { get; }
	}

	public class ExplosionManager : IExplosionManager 
	{
		private Byte maxBombsExplode;

		public void Initialize()
		{
			ExplosionList = new List<Explosion>(Constants.MAX_EXPLODE_SPAWN);
			for (Byte index = 0; index < Constants.MAX_EXPLODE_SPAWN; index++)
			{
				Explosion explode = new Explosion();
				explode.SetID(index);
				explode.Initialize(Constants.MAX_EXPLODE_FRAME);
				ExplosionList.Add(explode);
			}

			maxBombsExplode = Constants.MAX_EXPLODE_SPAWN;
		}

		public void LoadContent(Byte explodeIndex, ExplodeType explodeType)
		{
			// Load the explosion rectangles on demand.
			Explosion explode = ExplosionList[explodeIndex];
			explode.LoadContent(MyGame.Manager.ImageManager.ExplodeRectangles[(Int32)explodeType]);
		}

		public void Reset(Byte theBombsExplode, UInt16 frameDelay)
		{
			maxBombsExplode = theBombsExplode;
			if (maxBombsExplode > Constants.MAX_EXPLODE_SPAWN)
			{
				maxBombsExplode = Constants.MAX_EXPLODE_SPAWN;
			}

			for (Byte index = 0; index < maxBombsExplode; index++)
			{
				Explosion explode = ExplosionList[index];
				explode.Reset(frameDelay);
			}
		}

		public void Update(GameTime gameTime)
		{
			for (Byte index = 0; index < maxBombsExplode; index++)
			{
				Explosion explode = ExplosionList[index];
				if (explode.IsExploding)
				{
					explode.Update(gameTime);
				}
			}
		}

		public void Draw()
		{
			for (Byte index = 0; index < maxBombsExplode; index++)
			{
				Explosion explode = ExplosionList[index];
				if (explode.IsExploding)
				{
					explode.Draw();
				}
			}
		}

		public IList<Explosion> ExplosionList { get; private set; }

	}
}
