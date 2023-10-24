using Godot;
using System;

public partial class DashSound : AudioStreamPlayer2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerMovement pMove = GetParent<PlayerMovement>();
		pMove.OnDash += PlayDashSFX;
	}

	private void PlayDashSFX()
	{
		this.Play();
	}
}
