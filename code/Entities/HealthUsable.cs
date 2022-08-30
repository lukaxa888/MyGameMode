using Sandbox;
using System.Runtime.CompilerServices;

[Library("ent_healthusable")]
public partial class HealthUsable : Prop, IUse
{
	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/rust_props/barrels/fuel_barrel.vmdl" );
		SetupPhysicsFromModel( PhysicsMotionType.Static, false );

	}

	public bool OnUse(Entity user )
	{
		if ( user is not Player ply ) return false;
		ply.Health += 50f;
		Delete();
		return false;
	}

	public bool IsUsable(Entity user )
	{
		
		return user is  Player ply && ply.Health < 100;
	}
}
