using Godot;
using System;

[GlobalClass]
public partial class Bullet : Resource
{
	[ExportCategory("General")]
	[Export] public string _bulletName;
	[Export] public float _bulletLifetime;
	[Export] public Texture2D _bulletSprite;
}
