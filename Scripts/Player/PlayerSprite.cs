using Godot;
using System;

public partial class PlayerSprite : AnimatedSprite2D
{
	[Export] public bool facingLeft;
	Vector2 thisPos;
	Vector2 mousePos;
	
	PlayerMovement pMovement;
	
	[Export] public string animationState;
	[Export] public float animationSpeed = 1.4f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		pMovement = GetParent<PlayerMovement>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		thisPos = this.GlobalPosition;
		mousePos = GetGlobalMousePosition();
		
		if (mousePos.X < thisPos.X)
		{
			facingLeft = true;
			if (animationState == "Walk")
			{
				Animate(true);
			}
		}
		else
		{
			facingLeft = false;
			if (animationState == "Walk")
			{
				Animate(true);
			}
		}
		
		FlipH = facingLeft;
	}
	
	public void Animate(bool fIsMoving)
	{
		if (fIsMoving)
		{
			this.Play("walk");
			animationState = "Walk";
			
			//have facingLeft var and pMovement.Velocity to fuck with
			if (facingLeft && pMovement.Velocity.X > 0 || !facingLeft && pMovement.Velocity.X < 0)
			{
				this.SpeedScale = -animationSpeed;
			}
			else
			{
				this.SpeedScale = animationSpeed;
			}
		}
		else
		{
			this.SpeedScale = animationSpeed;
			this.Play("default");
			animationState = "Idle";
		}
	}
}
