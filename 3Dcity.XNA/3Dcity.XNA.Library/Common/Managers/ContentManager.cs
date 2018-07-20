using System;
using WindowsGame.Define.Factorys;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Managers
{
	public interface IContentManager 
	{
		void Initialize();
		void Initialize(String root);
		void LoadContent();
	}

	public class ContentManager : IContentManager 
	{
		private readonly IContentFactory contentFactory;

		public ContentManager(IContentFactory contentFactory)
		{
			this.contentFactory = contentFactory;
		}

		public void Initialize()
		{
			Initialize(String.Empty);
		}
		public void Initialize(String root)
		{
		}

		public void LoadContent()
		{
			Texture2D graphic = contentFactory.LoadTexture("Emulogic");
		}

	}
}
