using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using GeonBit.UI;
using GeonBit.UI.Entities;

namespace Prime
{
	public class PrimeGame : Game
	{
		public static PrimeGame Game;

		// The spritebatch that draws tem all
		private SpriteBatch drawer;

		// Monogame Required stuff
		GraphicsDeviceManager graphics;

		// The currently active scene
		private Scene activeScene;
		public Scene ActiveScene
		{
			get
			{
				return activeScene;
			}
			set
			{
				activeScene = value;
				activeScene.Game = this;
				activeScene.Content = new ContentManager(Content.ServiceProvider);
				activeScene.Content.RootDirectory = "Content";

				activeScene.Initialize();
                UserInterface.Active = activeScene.UI;

				activeScene.UI.UseRenderTarget = true;
			}
		}

		private ViewportAdapter viewPortAdapter;
		public ViewportAdapter ViewportAdapter
		{
			get
			{
				return viewPortAdapter;
			}
		}

		// Utility for getting the center of the screen
		public static Vector2 Center
		{
			get
			{
				return new Vector2(1280, 720) / 2;
			}
		}

		public PrimeGame(Scene s)
		{
			Game = this;

			graphics = new GraphicsDeviceManager(this);
			this.Window.AllowUserResizing = true;

			Content.RootDirectory = "Content";

			activeScene = s;

			base.IsMouseVisible = true;
		}
        
		protected override void Initialize()
		{
			base.Initialize();

			this.viewPortAdapter = new BoxingViewportAdapter(this.Window, this.graphics, 1280, 720);

			UserInterface.Initialize(this.Content, "hd");
            
			// Initialize the first scene
			ActiveScene = activeScene;
		}

		protected override void LoadContent()
		{
			drawer = new SpriteBatch(GraphicsDevice);
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			Time.GameTime = gameTime;
			Input.Update();
			Tasks.Update();

			UserInterface.Active.Update(gameTime);
			activeScene.Update();

			Colliders.Update();
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(activeScene.ClearColor);

			Time.GameTime = gameTime;

            UserInterface.Active.Draw(drawer);

			drawer.Begin(transformMatrix: activeScene.Cam.Camera2D.GetViewMatrix(), samplerState: SamplerState.PointClamp);
			activeScene.Draw(drawer);
            drawer.End();
            
            UserInterface.Active.DrawMainRenderTarget(drawer);

			// Draw GeonBit's UI
			// var vp = GraphicsDevice.Viewport;

            base.Draw(gameTime);
		}
	}
}
