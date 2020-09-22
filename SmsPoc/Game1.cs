using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SmsPoc
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		// Background (Titan planet)
		Texture2D titanTexture;
		Vector2 titanPosition;

		// Ironman figure
		Texture2D ironmanTexture;
		Vector2 ironmanPosition;
		float ironmanSpeed = 100f;
		bool isIronmanGrabbed = false;
		int ironmanWidth = 70;
		int ironmanHeight = 197;

		// Thanos figure
		Texture2D thanosTexture;
		Vector2 thanosPosition;
		float thanosSpeed = 100f;
		bool isThanosGrabbed = false;
		int thanosWidth = 93;
		int thanosHeight = 230;


		// Misc
		Point oldMousePosition;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			// Initialize background and figures (position and speed)
			titanPosition = new Vector2(0, 0);

			ironmanPosition = new Vector2(10, 10);
			ironmanSpeed = 100f;

			thanosPosition = new Vector2(400, 10);
			thanosSpeed = 100f;

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			titanTexture = Content.Load<Texture2D>("titan");
			ironmanTexture = Content.Load<Texture2D>("ironman");
			thanosTexture = Content.Load<Texture2D>("thanos");
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			var keyboardState = Keyboard.GetState();

			if (keyboardState.IsKeyDown(Keys.Up))
				thanosPosition.Y -= thanosSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (keyboardState.IsKeyDown(Keys.Down))
				thanosPosition.Y += thanosSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (keyboardState.IsKeyDown(Keys.Left))
				thanosPosition.X -= thanosSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (keyboardState.IsKeyDown(Keys.Right))
				thanosPosition.X += thanosSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (keyboardState.IsKeyDown(Keys.W))
				ironmanPosition.Y -= ironmanSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (keyboardState.IsKeyDown(Keys.S))
				ironmanPosition.Y += ironmanSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (keyboardState.IsKeyDown(Keys.A))
				ironmanPosition.X -= ironmanSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (keyboardState.IsKeyDown(Keys.D))
				ironmanPosition.X += ironmanSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			var mouseState = Mouse.GetState();

			if (mouseState.LeftButton == ButtonState.Pressed)
			{
				if (isThanosGrabbed)
				{
					thanosPosition.X -= oldMousePosition.X - mouseState.X;
					thanosPosition.Y -= oldMousePosition.Y - mouseState.Y;
				}
				else if(isIronmanGrabbed)
				{
					ironmanPosition.X -= oldMousePosition.X - mouseState.X;
					ironmanPosition.Y -= oldMousePosition.Y - mouseState.Y;
				}

				else
				{
					var thanosArea = new Rectangle(
						new Point((int)thanosPosition.X, (int)thanosPosition.Y),
						new Point(thanosWidth, thanosHeight));
					var ironmanArea = new Rectangle(
						new Point((int)ironmanPosition.X, (int)ironmanPosition.Y),
						new Point(ironmanWidth, ironmanHeight));

					isThanosGrabbed = thanosArea.Contains(mouseState.X, mouseState.Y);
					isIronmanGrabbed = ironmanArea.Contains(mouseState.X, mouseState.Y);
				}

				oldMousePosition.X = mouseState.X;
				oldMousePosition.Y = mouseState.Y;
			}
			else
			{
				isThanosGrabbed = false;
				isIronmanGrabbed = false;
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			_spriteBatch.Begin();
			_spriteBatch.Draw(titanTexture, titanPosition, Color.White);
			_spriteBatch.Draw(ironmanTexture, ironmanPosition, Color.White);
			_spriteBatch.Draw(thanosTexture, thanosPosition, Color.White);
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
