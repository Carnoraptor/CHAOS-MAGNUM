using Godot;
using System;

public partial class EnemyHitbox : Area2D
{

	public override void _Ready()
	{
		this.AreaEntered += Hit;
	}

	public override void _Process(double delta)
	{
	}
	
	public void Hit(Area2D area)
	{
		if (area is BulletBase bullet)
		{
			EmitSignal(SignalName.OnHit, bullet, bullet.damage);
		}
	}
	
	[Signal]
	public delegate void OnHitEventHandler(Area2D area, int damage);
}
