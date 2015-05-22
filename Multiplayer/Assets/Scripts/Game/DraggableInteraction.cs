using UnityEngine;
using System.Collections;

public class DraggableInteraction : MonoBehaviour {
    public static DraggableInteraction SINGLETON;

    void Awake()
    {
        SINGLETON = this;
    }

    public void HandleDraggableInteraction(Draggable a, Draggable b)
    {
        if (!a || !b) //One or more objects are null.
            return;

        if (a.IsUIObject && b.IsUIObject) //Both objects are UI objects.
            return;

        Debug.Log("Dragged " + a.gameObject.name + " onto " + b.gameObject.name);

        if (a.IsUIObject) //Try spawning a unit.
        {
            Debug.Log("Spawn");
        }
        else //Try swapping.
        {
            Debug.Log("Swap");
        }
    }
}
