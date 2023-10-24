using Godot;
using System;
using GunResource;

public partial class GunHandler : Node2D
{
		//General Variables
		[ExportCategory("General")]
		[Export] public string gunName;
		//[Export] public Gun.GunType _gunType;
		[Export] public int gunID;
		[Export] public string gunDesc;
		//Stats
		[ExportCategory("Stats")]
		[Export] public int damage;
		[Export] public float fireRate;
		[Export] public float inaccuracy;
		[Export] public int armorPen;
		[Export] public float bulletSpeed;
		[Export] public int bulletsAtOnce;
		[Export] public PackedScene bullet;
		[Export] public Bullet bulletResource;
		//Cosmetics
		[ExportCategory("Cosmetics")]
		[Export] public float radius = 6;
		[Export] public Vector2 bulletOffset;
		[Export] public Vector2 leftArmPos;
		[Export] public Vector2 rightArmPos;
		[Export] public Texture gunSprite;
		[Export] public AudioStream gunSFX;
		[Export] public float shakeMag;
		[Export] public float shakeDur;
		[ExportCategory("Attachments")]
		[Export] public bool hasMuzzleSlot;
		[Export] public bool hasOpticSlot;
		[Export] public bool hasLowerRailSlot;
		[Export] public bool hasSideRailSlot;
		[Export] public bool hasRoundsSlot;

		[Export] public Vector2 muzzleLoc;
		[Export] public Vector2 opticLoc;
		[Export] public Vector2 lowerRailLoc;
		[Export] public Vector2 sideRailLoc;
	
	//References
	PlayerMovement player;
	public Sprite2D gunSpriteNode;
	public Node2D bulletOrigin;
	public bool facingLeft;
	
	[Export] public Gun currentGun;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<PlayerMovement>("/root/root/Player");
		gunSpriteNode = GetNode<Sprite2D>("/root/root/Gun/GunSprite");
		bulletOrigin = GetNode<Node2D>("/root/root/Gun/GunSprite/BulletOrigin");
		currentGun = (Gun)GD.Load("res://Data/Guns/M416.tres");
		
		//Load the current gun
		LoadGun(currentGun);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		LookAtCursor();
		GunPositioning();
		Flip();
	}
	
	void LookAtCursor()
	{
		LookAt(GetGlobalMousePosition());
	}
	
	void Flip()
	{
		if (GetGlobalMousePosition().X < this.GlobalPosition.X)
		{
			facingLeft = true;
			Scale = new Vector2(1, -1);
		}
		else
		{
			facingLeft = false;
			Scale = new Vector2(1, 1);
		}
		
		//gunSpriteNode.FlipV = facingLeft;
	}

	void GunPositioning()
	{
		Vector2 globalDir = GetGlobalMousePosition() - player.GlobalPosition;
		if (globalDir.Length() > radius)
		{
			globalDir = radius * globalDir.Normalized();
		}
		Vector2 newPos = player.GlobalPosition + globalDir;
		newPos.Y -= 6f;
		this.GlobalPosition = newPos;
	}
	
	//GENERAL IMPORTANT FUNCTIONS
	public void LoadGun(Gun gun)
	{
		currentGun = gun;
		
		//General Variables
		gunName = currentGun._gunName;
		//[Export] public Gun.GunType _gunType;
		gunID = currentGun._gunID;
		gunDesc = currentGun._gunDesc;
		
		//Stats
		damage = currentGun._damage;
		fireRate = currentGun._fireRate;
		inaccuracy = currentGun._inaccuracy;
		armorPen = currentGun._armorPen;
		bulletSpeed = currentGun._bulletSpeed;
		bulletsAtOnce = currentGun._bulletsAtOnce;
		bullet = currentGun._bullet;
		bulletResource = currentGun._bulletResource;
		
		//Cosmetics
		radius = currentGun._radius;
		bulletOffset = currentGun._bulletOffset;
		leftArmPos = currentGun._leftArmPos;
		rightArmPos = currentGun._rightArmPos;
		gunSprite = currentGun._gunSprite;
		gunSFX = currentGun._gunSFX;
		shakeMag = currentGun._shakeMag;
		shakeDur = currentGun._shakeDur;
		
		//Attachments
		hasMuzzleSlot = currentGun._hasMuzzleSlot;
		hasOpticSlot = currentGun._hasOpticSlot;
		hasLowerRailSlot = currentGun._hasLowerRailSlot;
		hasSideRailSlot = currentGun._hasSideRailSlot;
		hasRoundsSlot = currentGun._hasRoundsSlot;

		muzzleLoc = currentGun._muzzleLoc;
		opticLoc = currentGun._opticLoc;
		lowerRailLoc = currentGun._lowerRailLoc;
		sideRailLoc = currentGun._sideRailLoc;
		
		gunSpriteNode.Texture = (Texture2D)gunSprite;
		
		//Initialize
		foreach(PackedScene scene in currentGun.gunNodes)
		{
			Node nodeInstance = (Node)scene.Instantiate();
			AddChild(nodeInstance);
		}
	}
}
