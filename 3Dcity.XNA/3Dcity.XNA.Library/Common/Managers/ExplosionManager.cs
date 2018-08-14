using System;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IExplosionManager 
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);
		void Draw();

		Explosion Explosion { get; }
	}

	public class ExplosionManager : IExplosionManager 
	{
		private const Byte MAX_FRAMES = 12;

		public void Initialize()
		{
			Explosion = new Explosion();
			Explosion.Initialize(new Vector2(400, 300));
			Explosion.Initialize(MAX_FRAMES, 500);		// TODO make the bullet delay configurable
		}

		public void LoadContent()
		{
			//Explosion.LoadContent(Assets.Explosion64Texture);
			//Explosion.LoadContent(Assets.Explosion80Texture);
			//Explosion.LoadContent(Assets.Explosion128Texture);
			Explosion.LoadContent(Assets.Explosion160Texture);
		}

		public void Update(GameTime gameTime)
		{
			Explosion.Update(gameTime);
		}

		public void Draw()
		{
			Explosion.Draw();
		}

		public Explosion Explosion { get; private set; }

	}
}
