using UnityEngine;
using System.Collections;

public class FormationFilledChecker : MonoBehaviour {

    [SerializeField]
    private GameMap GameMap;

    private bool flagged;

    void Update()
    {
        if( TilesAreFilled() )
        {
            if( flagged )
                return;

            Messenger.Bus.Route( new ClientFormationSetupReady() );
            flagged = true;
        } 
        else if( flagged ) // Should never hit
        {
            Messenger.Bus.Route( new ClientFormationSetupUnready() );
            flagged = false;
        }
    }

    bool TilesAreFilled()
    {
        foreach( var tile in GameMap.Tiles )
        {
            if( tile.Side == Side.RED )
                continue;

            if( tile.Contains == null )
                return false;
        }

        return true;
    }
}