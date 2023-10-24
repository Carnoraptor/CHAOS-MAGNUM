using Godot;
using System;

public partial class EntityBase : Node
{
	[Export] public Enemy enemy;
	
	public override void _Ready()
	{
		//Initialize
		foreach(PackedScene scene in enemy._enemyNodes)
		{
			Node nodeInstance = (Node)scene.Instantiate();
			AddChild(nodeInstance);
			GD.Print("added " + nodeInstance);
		}
	}

	public override void _Process(double delta)
	{
	}
}
