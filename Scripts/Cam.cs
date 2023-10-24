using Godot;
using System;

public partial class Cam : Camera2D
{
	float scaleLean = 0.2f;
	float smoothLean = 10f;
	
	Node2D player;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode("../Player") as Node2D;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CamFollow(delta);
		CamToPlayer();
		
		if (isShaking && timeLeftShaking > 0)
		{
			Vector2 shakeVector = new Vector2((float)GD.RandRange(-magnitude, magnitude), (float)GD.RandRange(-magnitude, magnitude));
			this.GlobalPosition += shakeVector;
			timeLeftShaking -= delta;
		}
		else
		{
			isShaking = false;
		}
	}
	
	void CamFollow(double delta)
	{
		Vector2 mousePos = GetGlobalMousePosition();
		Vector2 mouseDir = (mousePos - this.GlobalPosition).Normalized();
		float mouseDistance = mousePos.DistanceTo(this.GlobalPosition);
		Vector2 lean = mouseDir * mouseDistance * scaleLean;
		
		Vector2 newOffset = this.Offset;
		
		float deltaF = Convert.ToSingle(delta);
		
		this.Offset = (this.Offset.Lerp(lean, deltaF * smoothLean));
	}
	
	void CamToPlayer()
	{
		this.GlobalPosition = this.GlobalPosition.Lerp(player.GlobalPosition, 0.3f);
	}
	
	//Public functions
	
	public bool isShaking = false;
	public float magnitude;
	public double timeLeftShaking;
	
	public void Shake(float _magnitude, double _duration)
	{
		if (!isShaking || isShaking && magnitude < _magnitude)
		{
			magnitude = _magnitude;
		}
		timeLeftShaking += _duration;
		isShaking = true;
	}
}
