using Godot;
using System;

public partial class BulletBase : Area2D
{
	public GunHandler gunHandler;
	public Bullet bullet;
	public int damage;
	public float speed;
	public float timeUntilDeath = 5f;
	public Vector2 target;
	
	public override void _Ready()
	{
		gunHandler = GetNode<GunHandler>("/root/root/Gun");
		bullet = gunHandler.bulletResource;
		damage = gunHandler.damage;
		speed = gunHandler.bulletSpeed;
		//target = GetGlobalMousePosition().Normalized();
		target = new Vector2(speed, 0);
	}
	
	public async void DeathTimer()
	{
		await ToSignal(GetTree().CreateTimer(timeUntilDeath), "timeout");
		QueueFree();
	}
	
	private void _on_area_entered(Area2D area)
	{
		QueueFree();
	}	
	
	private void _on_body_entered(Node2D body)
	{
		QueueFree();
	}
	
}
