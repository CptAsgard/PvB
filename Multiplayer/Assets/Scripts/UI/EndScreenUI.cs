using UnityEngine;
using System.Collections;

/**
 * Simple handler for the ending screen
 */
public class EndScreenUI : MonoBehaviour, MessageReceiver<EndGame> {

    void Awake()
    {
        this.Subscribe<EndGame>(Messenger.Bus);
    }

    public void HandleMessage(EndGame msg)
    {
        iTween.MoveTo(gameObject, gameObject.transform.position + new Vector3(0, 800, 0), 2);
    }

    public void MainMenuButtonClicked()
    {
        Application.LoadLevel(0);
    }
}
