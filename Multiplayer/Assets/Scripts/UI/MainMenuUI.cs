using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Simple class to facilitate connecting and hosting games in the main menu
 */
public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI SINGLETON;

    public InputField Host;
    public Text LocalAddress;

    void Awake()
    {
        SINGLETON = this;
    }

    public void StartHosting()
    {
        Messenger.Bus.Route(new PlaySound() {
            audioclip = 2
        });
        NetConnector.SINGLETON.Host();
    }

    public void Connect()
    {
        NetConnector.SINGLETON.Connect( Host.text );
    }

    public void ShowLocalAddress( string localAddress )
    {
        LocalAddress.text = localAddress;
    }
}
