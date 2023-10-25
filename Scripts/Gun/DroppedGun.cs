using Godot;
using System;
using GunResource;

public partial class DroppedGun : Area2D
{
	[Export] public Gun gunIdentity;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_body_entered(Node2D body)
	{
		if (body is PlayerMovement)
		{
			GD.Print("Interacting with dropped gun!");
			GetNode<GunHandler>("/root/root/Gun").PickUpGun(gunIdentity);
		}
	}
}
