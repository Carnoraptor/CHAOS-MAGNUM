using Godot;
using System;

namespace GunResource
{
	[GlobalClass]
	public partial class Gun : Resource
	{
		//General Variables
		[ExportCategory("General")]
		[Export] public string _gunName;
		public enum GunType
		{
			Pistol,
			AssaultRifle,
			MachineGun,
			SubmachineGun,
			Shotgun,
			DMR,
			Explosive,
			Unique
		}
		[Export] public GunType _gunType;
		[Export] public int _gunID;
		[Export] public string _gunDesc;
		//Stats
		[ExportCategory("Stats")]
		[Export] public int _damage;
		[Export] public float _fireRate;
		[Export] public float _inaccuracy;
		[Export] public int _armorPen;
		[Export] public float _bulletSpeed;
		[Export] public int _bulletsAtOnce;
		[Export] public PackedScene _bullet;
		[Export] public Bullet _bulletResource;
		
		//Cosmetics
		[ExportCategory("Cosmetics")]
		[Export] public float _radius;
		[Export] public Vector2 _bulletOffset;
		[Export] public Vector2 _leftArmPos;
		[Export] public Vector2 _rightArmPos;
		[Export] public Texture _gunSprite;
		[Export] public AudioStream _gunSFX;
		[Export] public float _shakeMag;
		[Export] public float _shakeDur;
		[ExportCategory("Attachments")]
		[Export] public bool _hasMuzzleSlot;
		[Export] public bool _hasOpticSlot;
		[Export] public bool _hasLowerRailSlot;
		[Export] public bool _hasSideRailSlot;
		[Export] public bool _hasRoundsSlot;

		[Export] public Vector2 _muzzleLoc;
		[Export] public Vector2 _opticLoc;
		[Export] public Vector2 _lowerRailLoc;
		[Export] public Vector2 _sideRailLoc;

		[ExportCategory("Nodes")]
		[Export] public Godot.Collections.Array<PackedScene> gunNodes = new Godot.Collections.Array<PackedScene>();
	}
	
}
