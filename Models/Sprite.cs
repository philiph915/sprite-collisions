using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprite_collisions
{
    public class Sprite
    {
        protected Texture2D _texture;

        public Vector2 Position;    // position is the top left corner of the sprite texture
        public Vector2 Velocity = Vector2.Zero;
        public Color Color = Color.White;
        public float speed;
        public Input input;
        public string Tag = "";
        public string debugString = "";
    
        // This Rectangle is the Sprite object's collider
        public Rectangle Rectangle
        {
            get     // Creating this Rectangle Property with a public get method allows for collision detection algorithm to access it
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        // virtual voids are intended to be overridden by a derived class
        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {    }
    
        public virtual void Draw(SpriteBatch spriteBatch)
        { 
            spriteBatch.Draw(_texture, Position, Color);
        }

        #region Collision

        // Compare this sprite object with another sprite object to check for collision
        
        // This tests if this object's right side is hitting another object's left side
        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
                    this.Rectangle.Left < sprite.Rectangle.Left &&
                    this.Rectangle.Bottom > sprite.Rectangle.Top &&
                    this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
                    this.Rectangle.Right > sprite.Rectangle.Right &&
                    this.Rectangle.Bottom > sprite.Rectangle.Top &&
                    this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        // Tests if this object's bottom side is touching another object's top side
        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
                    this.Rectangle.Top < sprite.Rectangle.Top &&
                    this.Rectangle.Right > sprite.Rectangle.Left &&
                    this.Rectangle.Left < sprite.Rectangle.Right;
        }

        // Tests if this object's top side is touching another object's bottom side
        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
                    this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
                    this.Rectangle.Right > sprite.Rectangle.Left &&
                    this.Rectangle.Left < sprite.Rectangle.Right;
        }

        #endregion
    }
}