using UnityEngine;
using System.Collections;
using System.Net;

public class NetConnector : MonoBehaviour 
{
    public static NetConnector SINGLETON;

    public string LocalAddress;
    public bool Connected;
    private string host;
    private const int PORT = 42594;


    void Awake()
    {
        SINGLETON = this;
    }

	void Start () 
    {
        //Make sure the NetConnector exists regardless of the scene.
        DontDestroyOnLoad(gameObject);

        //Get local address.
        IPAddress[] addr = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList;
        LocalAddress = addr[addr.Length - 1].ToString();
        MainMenuUI.SINGLETON.ShowLocalAddress(LocalAddress);
	}

    /**
     * Start hosting a game.
     */
    public void Host()
    {
        Debug.Log("Setting up server...");
        Network.InitializeServer(2, PORT, true);
    }

    /**
     * Connect to an existing game.
     */
    public void Connect(string host)
    {
        Debug.Log("Connecting to " + host + "...");
        this.host = host;
        Network.Connect(this.host, PORT);
    }

    /**
     * Retry connecting after waiting for 1 second.
     */
    IEnumerator Retry()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Retrying to connect to " + this.host + "...");
        Network.Connect(this.host, PORT);
        yield return null;
    }

    void OnFailedToConnect()
    {
        Debug.Log("Failed to connect.");
        /*if(Network.peerType != NetworkPeerType.Server)
        StartCoroutine(Retry());*/
    }

    void OnServerInitialized()
    {
        Debug.Log("Hosting on " + LocalAddress + ".");
    }

    void OnConnectedToServer()
    {
        Connected = true;
        Debug.Log("Connected to " + host + ".");
    }

    void OnDisconnectedFromServer()
    {
        Connected = false;
        Debug.Log("Disconnected from " + host + ".");
    }

    /**
     * Player connected to server.
     */
    void OnPlayerConnected(NetworkPlayer player)
    {
        Connected = true;
        Debug.Log("Player connected from " + player.ipAddress + ":" + player.port);
    }

    /**
     * Player disconnected from server.
     */
    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Connected = false;
        Debug.Log("Player disconnected from " + player.ipAddress + ":" + player.port);
    }
}
