using UnityEngine;
using System.Collections;

public class UnitRootMover : MonoBehaviour, MessageReceiver<StartGame>
{
    void Start() {
        this.Subscribe<StartGame>( Messenger.Bus );
    }

    public void HandleMessage( StartGame msg )
    {
        Side side = ( gameObject.name == "- UnitsBLUE" ? Side.BLUE : Side.RED );
        Vector3 moveTo = new Vector3( transform.position.x + ( side == Side.BLUE ? -0.391f : 0.391f ), transform.position.y, transform.position.z );
        iTween.MoveTo( gameObject, iTween.Hash( "delay", 4f, "time", 4, "position", moveTo, "easetype", iTween.EaseType.easeInOutQuad ) );
    }
}
