using Godot;
using System;

public partial class Knife : AnimatedSprite2D
{
	bool onKnifeCooldown = false;
	Vector2 slashDir = Vector2.Zero;
	
	float knifeTime = 0.35f;
	float knifeCooldown = 3f;
	
	CollisionShape2D knifeCollider;
	
	AudioStreamPlayer2D audioPlayer;
	AudioStreamPlayer2D parryPlayer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		knifeCollider = GetChild<Area2D>(0).GetChild<CollisionShape2D>(0);
		audioPlayer = GetChild<AudioStreamPlayer2D>(1);
		parryPlayer = GetChild<AudioStreamPlayer2D>(2);
		knifeCollider.Disabled = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Knife") && !onKnifeCooldown)
		{
			Slash();
		}	
	}
	
	async void Slash()
	{
		FlipV = GetParent().GetNode<PlayerSprite>("PlayerSprite").facingLeft;
		
		LookAt(GetGlobalMousePosition());
		
		this.Play("Slash");
		
		onKnifeCooldown = true;
		
		audioPlayer.Play();
		
		KnifeCollide();
		
		await ToSignal(GetTree().CreateTimer(knifeTime), "timeout");
		
		this.Play("default");
		
		KnifeCooldown();
	}
	
	async void KnifeCollide()
	{
		await ToSignal(GetTree().CreateTimer(0.05), "timeout");
		knifeCollider.Disabled = false;
		await ToSignal(GetTree().CreateTimer(0.2), "timeout");
		knifeCollider.Disabled = true;
	}
	
	async void KnifeCooldown()
	{
		await ToSignal(GetTree().CreateTimer(knifeCooldown), "timeout");
		onKnifeCooldown = false;
	}
	
	
	//Parrying
	
	private void _on_dummy_bullet_parried()
	{
		// Replace with function body.
		parryPlayer.Play();
		GetNode<Cam>("/root/root/MainCamera").Shake(5f, 0.2);
		FreezeFrame();
	}
	
	private async void FreezeFrame()
	{
		Engine.TimeScale = 0.01f;
		await ToSignal(GetTree().CreateTimer(0.01f), "timeout");
		Engine.TimeScale = 1f;
		
	}
}
