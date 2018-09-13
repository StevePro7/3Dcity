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
		void Explode(Byte explodeIndex, Vector2 position);
		void Update(GameTime gameTime);
		void Draw();

		IList<Explosion> ExplosionList { get; }
		IDictionary<Byte, Explosion> ExplosionDict { get; }
	}

	public class ExplosionManager : IExplosionManager 
	{
		private IList<Byte> keys;
		private Byte maxBombsExplode;

		public void Initialize()
		{
			ExplosionList = new List<Explosion>(Constants.MAX_ENEMY_SPAWN);
			ExplosionDict = new Dictionary<Byte, Explosion>(Constants.MAX_ENEMY_SPAWN);

			for (Byte index = 0; index < Constants.MAX_EXPLODE_SPAWN; index++)
			{
				Explosion explode = new Explosion();
				explode.SetID(index);
				explode.Initialize(Constants.MAX_EXPLODE_FRAME);
				ExplosionList.Add(explode);
			}

			keys = new List<Byte>();
			maxBombsExplode = Constants.MAX_EXPLODE_SPAWN;
		}

		public void LoadContent(Byte explodeIndex, ExplodeType explodeType)
		{
			// Load the explosion rectangles on demand.
			Explosion explosion = ExplosionList[explodeIndex];
			explosion.LoadContent(MyGame.Manager.ImageManager.ExplodeRectangles[(Int32)explodeType]);
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

		public void Explode(Byte explodeIndex, Vector2 position)
		{
			Explosion explosion = ExplosionList[explodeIndex];
			if (explosion.IsExploding)
			{
				return;
			}

			explosion.SetPosition(position);
			explosion.Explode();

			ExplosionDict.Add(explodeIndex, explosion);
		}

		public void Update(GameTime gameTime)
		{
			if (0 == ExplosionDict.Count)
			{
				return;
			}

			keys.Clear();
			foreach (var key in ExplosionDict.Keys)
			{
				Explosion explosion = ExplosionDict[key];
				if (null != explosion && explosion.IsExploding)
				{
					// Update explosion but check to see if finished.
					explosion.Update(gameTime);
					if (!explosion.IsExploding)
					{
						keys.Add(explosion.ID);
					}
				}
			}

			// https://stackoverflow.com/questions/9892790/deleting-while-iterating-over-a-dictionary
			foreach (var key in keys)
			{
				ExplosionDict.Remove(key);
			}

			//for (Byte index = 0; index < maxBombsExplode; index++)
			//{
			//    Explosion explode = ExplosionList[index];
			//    if (explode.IsExploding)
			//    {
			//        explode.Update(gameTime);
			//    }
			//}
		}

		public void Draw()
		{
			if (0 == ExplosionDict.Count)
			{
				return;
			}

			foreach (var key in ExplosionDict.Keys)
			{
				Explosion explosion = ExplosionDict[key];
				if (null != explosion && explosion.IsExploding)
				{
					explosion.Draw();
				}
				
			}

			//for (Byte index = 0; index < maxBombsExplode; index++)
			//{
			//    Explosion explode = ExplosionList[index];
			//    if (explode.IsExploding)
			//    {
			//        explode.Draw();
			//    }
			//}
		}

		public IList<Explosion> ExplosionList { get; private set; }
		public IDictionary<Byte, Explosion> ExplosionDict { get; private set; }
	}
}
