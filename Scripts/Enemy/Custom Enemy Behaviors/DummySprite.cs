using Godot;
using System;
using Utility;

public partial class DummySprite : AnimatedSprite2D
{
	public override void _Ready()
	{
		AfterAwake();
	}
	
	private async void AfterAwake()
	{
		await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
		NodeUtils nodeUtils = new NodeUtils();
		nodeUtils.FindNode<EnemyHitbox>(GetParent()).OnHit += PlayAnim;
	}

	private void PlayAnim(Area2D area, int damage)
	{
		if (area.GlobalPosition.X > this.GlobalPosition.X)
		{
			Play("hitLeft");
		}
		else
		{
			Play("hitRight");
		}
	}
}
