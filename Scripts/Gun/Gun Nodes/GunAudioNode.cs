using Godot;
using System;
using Utility;

public partial class GunAudioNode : AudioStreamPlayer2D
{
	public override void _Ready()
	{
		if (IsInsideTree() == false)
		{
			GD.Print("fuck");
		}
		NodeUtils nodeUtils = new NodeUtils();
		ShootNode shootNode = nodeUtils.FindNode<ShootNode>(GetParent());
		shootNode.OnShoot += PlaySound;
		
		this.Stream = GetParent<GunHandler>().gunSFX;
	}
	
	public virtual void PlaySound()
	{
		this.Play();
	}
	
}
