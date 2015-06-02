using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Temp class to show who is allowed to make a move
 */
public class ViewTurnUI : MonoBehaviour
{
    public Text text;

    // Update is called once per frame
    void Update()
    {
        if( GameState.CurrentPlayerTurn == Side.BLUE )
        {
            text.text = "YOUR TURN";
        } else
        {
            text.text = "THEIR TURN";
        }
    }
}
