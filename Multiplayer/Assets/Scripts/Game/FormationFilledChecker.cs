using UnityEngine;
using System.Collections;

/**
 * Checker to keep track of the formation in pre-planning state
 */
public class FormationFilledChecker : MonoBehaviour {

    [SerializeField]
    private GameMap GameMap;

    // Make sure messaging behaviour only happens once!
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
    }

    /**
     * Are -all- of our tiles filled?
     * @returns True if tiles are completely filled, otherwise false
     */
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