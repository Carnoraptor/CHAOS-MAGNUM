using Godot;
using System;

public partial class ProjectileBase : Area2D
{
	//Parry variables
	[Export] public bool isParriable = true;
	private bool isParried = false;
	private Vector2 parryDir;
	
	//General
	[Export] public float baseSpeed = 120f;
	[Export] public float currentSpeed;
	[Export] public float projectileDamage = 5f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentSpeed = baseSpeed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (isParried)
		{
			MoveAway();
		}
	}
	
	
	
	//Parrying
	
	private void _on_knife_area_area_entered(Area2D area)
	{
		if (isParriable && area == this)
		{
			isParried = true;
			currentSpeed *= 2;
			parryDir = (GetGlobalMousePosition() - Position).Normalized();
			
			EmitSignal(SignalName.BulletParried);
		}
	}
	
	private void MoveAway()
	{
		Position += parryDir * currentSpeed * (float)GetProcessDeltaTime();
	}

	[Signal]
	public delegate void BulletParriedEventHandler();
}
