using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace sprite_collisions;

public class Player : Sprite
{
    private float debugTimer = 0.5f;

    public Player(Texture2D texture)
        : base(texture)
        {

        }

    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        Move();

        foreach (var sprite in sprites)
        {
            if (sprite.Tag == this.Tag) { continue; }


            debugTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (debugTimer<= 0f && this.Tag == "Player1 (Blue)" && sprite.Tag == "Player4 (Black)")
            {
               /* bool hitLeft = this.IsTouchingLeft(sprite);
                if (hitLeft)
                    Debug.Write(this.Tag + " is touching left side of " + sprite.Tag + "\n"  + "Velocity.X = " + this.Velocity.X.ToString() + "\n");
                debugTimer = 0.5f;
                */
                bool hitLeft = this.IsTouchingLeft(sprite);
                debugString = "VelocityX = " + this.Velocity.X.ToString() + "\n"
                               + "IsTouchingLeft (Player4 (Black)): " + hitLeft.ToString() + "\n" ;

            }


            // Rather than only setting velocity to 0 here, it would also make sense to update our position so we are touching the object we are colliding with

            // If we are traveling right, check if we are hitting the left side of a sprite
            if (this.Velocity.X > 0 && this.IsTouchingLeft(sprite))
            {
                this.Velocity.X = 0; // stop the object from moving at the instant before collision
                this.Position.X -= this.Rectangle.Right - sprite.Rectangle.Left; // Set the position such that it is exactly touching
                /*if (this.Tag == "Player1 (Blue)")
                    Debug.Write(this.Tag + " is touching left side of " + sprite.Tag + "\n" + "Velocity.X = " + this.Velocity.X.ToString() + "\n");*/

            } 

            // If we are traveling left, check if we are hitting the right side of a sprite
            if (this.Velocity.X < 0 && this.IsTouchingRight(sprite))
            {
                this.Velocity.X = 0; // stop the object from moving at the instant before collision
                this.Position.X -= this.Rectangle.Left - sprite.Rectangle.Right; // Set the position such that it is exactly touching
            } 

            // Stop Vertical Movement based on up/down collisions
            if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite)) 
            {
                this.Velocity.Y = 0;
                this.Position.Y -= this.Rectangle.Bottom - sprite.Rectangle.Top; // Set the position such that it is exactly touching
            }
            if (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite)) 
            {
                this.Velocity.Y = 0;
                this.Position.Y -= this.Rectangle.Top - sprite.Rectangle.Bottom; // Set the position such that it is exactly touching
            }
        }

        // Had to move this out of the foreach loop to allow the player to collide with multiple objects
        Position += Velocity; // Update position based on velocity
        Velocity = Vector2.Zero; // Reset velocity
    }

    private void Move()
    {
        // Check Keys and apply movement
        if (Keyboard.GetState().IsKeyDown(input.Left))
        {
            Velocity.X = -speed;
        } else if (Keyboard.GetState().IsKeyDown(input.Right))
        {
            Velocity.X = speed;
        }

        if (Keyboard.GetState().IsKeyDown(input.Up))
        {
            Velocity.Y = -speed;
        } else if (Keyboard.GetState().IsKeyDown(input.Down))
        {
            Velocity.Y = speed;
        }
    }
}