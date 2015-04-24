using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuUI : MonoBehaviour {
    public static MainMenuUI SINGLETON;

    public InputField Host;
    public Text LocalAddress;

    void Awake()
    {
        SINGLETON = this;
    }

    public void StartHosting()
    {
        NetConnector.SINGLETON.Host();
    }

    public void Connect()
    {
        NetConnector.SINGLETON.Connect(Host.text);
    }

    public void ShowLocalAddress(string localAddress)
    {
        LocalAddress.text = localAddress;
    }
}
