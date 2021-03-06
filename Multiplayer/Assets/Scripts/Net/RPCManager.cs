﻿using UnityEngine;
using System.Collections;

public class RPCManager : MonoBehaviour
{
    public static RPCManager SINGLETON;

    public NetworkView Network;

    void Awake()
    {
        SINGLETON = this;
    }

    void Update()
    {
        //Test packet.
        if( Input.GetKeyDown( KeyCode.Q ) && NetConnector.SINGLETON.Connected )
            SendMove( new Vector3( 1, 12, 111 ), new Vector3( 411, 2.8f, 22.2f ) );
    }

    public void SendMove( Vector3 from, Vector3 to )
    {
        Network.RPC( "RPC_Move", RPCMode.Others, new GridPosition( 9 - (int) from.x, (int) from.y ).ToVector3(), new GridPosition( 9 - (int) to.x, (int) to.y ).ToVector3() );

        if( GameState.CurrentState == EGameState.PLAY && GameState.CurrentPlayerTurn == Side.BLUE )
            GameState.NextPlayerTurn();
    }

    public void SendKill( Vector3 pos ) {
        Network.RPC( "RPC_Kill", RPCMode.Others, new GridPosition( 9 - (int) pos.x, (int) pos.y ).ToVector3() );

        if( GameState.CurrentState == EGameState.PLAY && GameState.CurrentPlayerTurn == Side.BLUE )
            GameState.NextPlayerTurn();
    }

    [RPC]
    public void RPC_Move( Vector3 from, Vector3 to, NetworkMessageInfo info )
    {
        Debug.Log( "Move received. from " + from + " to " + to + "." );
        DraggableInteraction.SINGLETON.HandleDraggableInteraction(
            GameMap.SINGLETON.GetTileAt( new GridPosition( from ) ).GetComponent<Draggable>(),
            GameMap.SINGLETON.GetTileAt( new GridPosition( to ) ).GetComponent<Draggable>(),
            false );

        if( GameState.CurrentState == EGameState.PLAY && GameState.CurrentPlayerTurn == Side.RED )
            GameState.NextPlayerTurn();
    }

    [RPC]
    public void RPC_Kill( Vector3 pos, NetworkMessageInfo info ) {
        GameMap.SINGLETON.GetTileAt( new GridPosition( pos ) ).Contains.Die( false );

        if( GameState.CurrentState == EGameState.PLAY && GameState.CurrentPlayerTurn == Side.RED )
            GameState.NextPlayerTurn();
    }

    public void SendFormation( string encodedFormation )
    {
        Network.RPC( "RPC_Formation", RPCMode.Others, encodedFormation );
    }

    [RPC]
    public void RPC_Formation( string encodedFormation, NetworkMessageInfo info )
    {
        Debug.Log( "Formation received: " + encodedFormation );
        GameMap.SINGLETON.DecodeFormation( encodedFormation, Side.RED );
    }
}
