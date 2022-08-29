using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;

using System.Threading.Tasks;
using System.Linq;
using System.Numerics;
//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace Sandbox;

/// <summary>
/// This is your game class. This is an entity that is created serverside when
/// the game starts, and is replicated to the client. 
/// 
/// You can use this to create things like HUDs and declare which player class
/// to use for spawned players.
/// </summary>
public partial class MyGame : Sandbox.Game
{
	public MyHud MyHUD;


	public MyGame()
	{
		if ( IsClient ) MyHUD = new MyHud();
	}

	[Event.Hotload]
	public void HotloadUpdate()
	{
		if ( !IsClient ) return;
		MyHUD?.Delete();
		MyHUD = new MyHud();

	}

	/// <summary>
	/// A client has joined the server. Make them a pawn to play with
	/// </summary>
	public override void ClientJoined( Client client )
	{
		base.ClientJoined( client );
		// Crear un Pawn
		var pawn = new PrimerPlayer();
		client.Pawn = pawn;
		pawn.Respawn();

	}

	[ConCmd.Server( "killeveryone" )]
	public static void killEveryone()
	{

		foreach ( Player player in All.OfType<Player>() )
		{
			player.TakeDamage( DamageInfo.Generic( 100f ) );
		}
	}

	[ConCmd.Server( "sethealth" )]
	public static void setHealth( int health )
	{
		var caller = ConsoleSystem.Caller.Pawn;
		if ( caller == null ) return;

		caller.Health = health;
	}


	[ConCmd.Server]
	public static void damageTarget(float damage)
	{
		Log.Info( $"{damage}" );
		var caller = ConsoleSystem.Caller.Pawn;
		if ( caller == null ) return;
		
		var tr = Trace.Ray( caller.EyePosition, caller.EyePosition + caller.EyeRotation.Forward * 5000 )
			.UseHitboxes()
			.Ignore( caller )
			.Run();
		DebugOverlay.TraceResult( tr, 50);
		if ( tr.Entity is Player victim && victim.IsValid() )
		{
			victim.TakeDamage( DamageInfo.Generic( damage ) );
			caller.TakeDamage( DamageInfo.Generic( damage ) );
			Log.Info( $"{victim.Client.Name} -- {victim.Health}" );
		}


	}
}
