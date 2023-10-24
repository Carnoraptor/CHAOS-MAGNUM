using Godot;
using System;

public partial class ShootNode : Node2D
{
	bool isOnCooldown = false;
	GunHandler gunHandler;
	PackedScene bullet;
	Node bulletParent;
	Cam camera;
	
	public override void _Ready()
	{
		if (IsInsideTree() == false)
		{
			GD.Print("fuck");
		}
		gunHandler = GetParent<GunHandler>();
		bullet = gunHandler.bullet;
		bulletParent = GetNode("/root/root/PlayerBullets");
		camera = GetNode<Cam>("/root/root/MainCamera");
	}
	
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("Fire") && !isOnCooldown)
		{
			Shoot();
			GunCooldown();
		}
	}
	
	public virtual void Shoot()
	{
		//Effects
		EmitSignal(SignalName.OnShoot);
		camera.Shake(gunHandler.shakeMag, gunHandler.shakeDur);
		
		//loop for shotguns
		Area2D bulletInstance = (Area2D)bullet.Instantiate();
		bulletParent.AddChild(bulletInstance);
		bulletInstance.GlobalPosition = gunHandler.bulletOrigin.GlobalPosition;
		bulletInstance.Rotation = gunHandler.Rotation;
		float tempInacc = (float)GD.RandRange((double)-gunHandler.inaccuracy, (double)gunHandler.inaccuracy);
		bulletInstance.Rotation += tempInacc;
	}
	
	public async void GunCooldown()
	{
		isOnCooldown = true;
		await ToSignal(GetTree().CreateTimer(GetParent<GunHandler>().fireRate), "timeout");
		isOnCooldown = false;
	}
	
	
	//Signals
	[Signal]
	public delegate void OnShootEventHandler();
}
