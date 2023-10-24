using Godot;
using System;
using System.Collections;

public partial class PlayerMovement : CharacterBody2D
{
	[Export] public int Speed { get; set; } = 400;
	[Export] public bool isDashing = false;
	[Export] public bool doMovement = true;
	bool _isMoving = false;
	[Export] public bool isMoving = false;
	
	//Dash Variables
	[Export] public float dashSpeed = 700f;
	[Export] public bool canDash = true;
	float dashTime = 0.1f;
	Vector2 dashDir;
	
	//Animation
	AnimatedSprite2D animPlayer;
	PlayerSprite playerSprite;
	string animationState = "";
	
	//Dash Ghost
	PackedScene ghost;
	Node2D root;

	public override void _Ready()
	{
		animPlayer = GetNode<AnimatedSprite2D>("PlayerSprite");
		playerSprite = GetNode<PlayerSprite>("PlayerSprite");
		ghost = GD.Load<PackedScene>("res://Scenes/ghost.tscn");
		root = GetNode<Node2D>(".");
	}
	

	public void GetInput()
	{
		if (doMovement)
		{
			Vector2 inputDirection = Input.GetVector("Left", "Right", "Up", "Down");
			Velocity = inputDirection * Speed;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
		
		//Dashing
		
		if (Input.IsActionJustPressed("Dash") && !isDashing)
		{
			Dash();
		}	
		
		if (isDashing)
		{
			Velocity = dashDir * dashSpeed;
			MoveAndSlide();
		}
		
		//Animations
		CheckMovement();
	}

	public async void Dash()
	{
		EmitSignal(SignalName.OnDash);
		
		isDashing = true;
		doMovement = false;

		Vector2 mousePos = GetGlobalMousePosition();

		dashDir = (mousePos - this.GlobalPosition).Normalized();
	
		Velocity = dashDir * dashSpeed;
		MoveAndSlide();
		
		SpawnGhosts();
		
		GetNode<Cam>("/root/root/MainCamera").Shake(3f, 0.1);
		
		await ToSignal(GetTree().CreateTimer(dashTime), "timeout");

		isDashing = false;
		Velocity = Vector2.Zero;
		doMovement = true;

		//stamina -= 50f;
	}
	
	async void SpawnGhosts()
	{
		SpawnGhost(0.1);
		await ToSignal(GetTree().CreateTimer(0.02), "timeout");
		SpawnGhost(0.08);
		await ToSignal(GetTree().CreateTimer(0.02), "timeout");
		SpawnGhost(0.06);
		await ToSignal(GetTree().CreateTimer(0.02), "timeout");
		SpawnGhost(0.04);
		await ToSignal(GetTree().CreateTimer(0.02), "timeout");
	}
	
	async void SpawnGhost(double timeToWait)
	{
		Node2D ghostInstance = (Node2D)ghost.Instantiate();
		GetParent().AddChild(ghostInstance);
		ghostInstance.GlobalPosition = this.GlobalPosition;
		ghostInstance.GetNode<Sprite2D>(".").FlipH = playerSprite.FlipH;
		await ToSignal(GetTree().CreateTimer(timeToWait), "timeout");
		ghostInstance.QueueFree();
	}
	
	void CheckMovement()
	{
		if (Velocity != Vector2.Zero)
		{
			bool movingLeft = CheckMovementDirection();
			
			isMoving = true;
			if (playerSprite.animationState != "Walk")
			{
				playerSprite.Animate(true);
			}
		}
		else
		{
			if (playerSprite.animationState != "Idle")
			{
				playerSprite.Animate(false);
			}
		}
	}
	
	
	//true is moving left
	public bool CheckMovementDirection()
	{
		if (Velocity.X < 0)
		{
			return true;
		}
		
		return false;
	}
	
	
	
	//Signals
	[Signal]
	public delegate void OnDashEventHandler();
	
}
