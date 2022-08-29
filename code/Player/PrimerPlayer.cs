using Sandbox;
using System;

public partial class PrimerPlayer : Player
{
	public PrimerPlayer(){
		Inventory = new BaseInventory( this );

	}

	public PrimerPlayer(Client cl) : this()
	{

	}

	public override void Respawn()
	{
		SetModel( "models/citizen/citizen.vmdl" );
		Controller = new WalkController();
		Animator = new StandardPlayerAnimator();
		CameraMode = new FirstPersonCamera();
		
		EnableAllCollisions = true;
		EnableDrawing = true;
		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;
		
		base.Respawn();
		Health = 100f;
	}
}
