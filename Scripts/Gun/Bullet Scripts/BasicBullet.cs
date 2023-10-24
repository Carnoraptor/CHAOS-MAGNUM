using Godot;
using System;

public partial class BasicBullet : BulletBase
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 movement = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation));

		Position += movement * speed * (float)delta;
	}
}
