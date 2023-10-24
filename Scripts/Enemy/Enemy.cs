using Godot;
using System;

[GlobalClass]
public partial class Enemy : Resource
{
	[ExportCategory("Main")]
	[Export] public string _enemyName;
	public enum EnemyType
	{
		None,
		Pursuer,
		Turret,
		Sniper,
		Charger,
		Custom
	}
	[Export] public EnemyType _enemyType;
	[Export] public int _enemyID;

	[ExportCategory("Stats")]
	[Export] public int _enemyHP;
	[Export] public int _enemyArmor;
	[Export] public int _enemyDamage;
	[Export] public int _enemyArmorPierce;
	[Export] public float _enemySpeed;
	[Export] public float _enemyAttackRate;

	[ExportCategory("Behavior")]
	[Export] public bool _doesContactDamage;
	[Export] public int _contactDamage;
	[Export] public int _contactAP;

	[ExportCategory("Graphics and Prefabs")]
	[Export] public Texture _enemySprite;
	
	[ExportCategory("Nodes")]
	[Export] public Godot.Collections.Array<PackedScene> _enemyNodes = new Godot.Collections.Array<PackedScene>();
}
