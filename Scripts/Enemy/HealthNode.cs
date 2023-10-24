using Godot;
using System;
using Utility;

public partial class HealthNode : Node
{
	[Export] public int currentHealth;
	[Export] public int maximumHealth;
	
	[Export] public EntityBase entityBase;
	private EnemyHitbox hitbox;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (IsInsideTree() == false)
		{
			GD.Print("fuck");
		}
		entityBase = GetParent<EntityBase>();
		NodeUtils nodeUtils = new NodeUtils();
		hitbox = nodeUtils.FindNode<EnemyHitbox>(GetParent());
		hitbox.OnHit += TakeDamage;
		
		maximumHealth = entityBase.enemy._enemyHP;
		currentHealth = maximumHealth;
		
		GD.Print("i am real");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void TakeDamage(Area2D area, int damage)
	{
		currentHealth -= damage;
		EmitSignal(SignalName.OnDamage);
	}
	
	
	[Signal]
	public delegate void OnDamageEventHandler();
}
