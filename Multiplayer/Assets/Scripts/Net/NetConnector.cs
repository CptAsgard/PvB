using UnityEngine;
using System.Collections;
using System.Net;

public class NetConnector : MonoBehaviour 
{
    public static NetConnector SINGLETON;

    public string LocalAddress;     //Local IP address.
    public bool Connected;          //Whether we're connected with an opponent.
    private string host;            //The host address.
    private const int PORT = 42594; //The host port.


    void Awake()
    {
        SINGLETON = this;
    }

	void Start () 
    {
        //Make sure the NetConnector exists regardless of what scene we're in.
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
        Network.InitializeServer(1, PORT, true);
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

    /**
     * Failed to connect to a server.
     */
    void OnFailedToConnect(NetworkConnectionError error)
    {
        if (Network.peerType != NetworkPeerType.Server)
            StartCoroutine(Retry());
    }

    /**
     * {SERVER}
     * Player started server.
     */
    void OnServerInitialized()
    {
        Debug.Log("Hosting on " + LocalAddress + ".");
        GameState.Initialize( Side.BLUE );
        Messenger.Bus.Route( new NetworkServerInitialized() );
    }


    /**
     * {CLIENT}
     * Player connected to server.
     */
    void OnConnectedToServer()
    {
        Connected = true;
        Debug.Log("Connected to " + host + ".");
        if (Application.loadedLevel == 0)
            Application.LoadLevel(1);

        Messenger.Bus.Route( new NetworkClientInitialized() );
        GameState.Initialize( Side.RED );
    }

    /**
     * {CLIENT}
     * Player disconnected from server.
     */
    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        Connected = false;
        Debug.Log("Disconnected from " + host + ".");
        if (Application.loadedLevel == 1)
            Application.LoadLevel(0);
    }

    /**
     * {SERVER}
     * Player connected to server.
     */
    void OnPlayerConnected(NetworkPlayer player)
    {
        Connected = true;
        Debug.Log("Player connected from " + player.ipAddress + ":" + player.port);
        if(Application.loadedLevel == 0)
            Application.LoadLevel(1);
    }

    /**
     * {SERVER}
     * Player disconnected from server.
     */
    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Connected = false;
        Debug.Log("Player disconnected from " + player.ipAddress + ":" + player.port);
        if (Application.loadedLevel == 1)
            Application.LoadLevel(0);
    }
}
