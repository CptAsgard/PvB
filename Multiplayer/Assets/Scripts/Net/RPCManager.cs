﻿using UnityEngine;
using System.Collections;

public class RPCManager : MonoBehaviour {
    public static RPCManager SINGLETON;

    public NetworkView Network;

    void Awake()
    {
        SINGLETON = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && NetConnector.SINGLETON.Connected)
            SendMove(new Vector3(1, 12, 111), new Vector3(411, 2.8f, 22.2f));
    }

    public void SendMove(Vector3 from, Vector3 to)
    {
        Network.RPC("RPC_Move", RPCMode.Others, from, to);
    }

    [RPC]
    public void RPC_Move(Vector3 from, Vector3 to, NetworkMessageInfo info) 
    {
        Debug.Log("Move received. from " + from + " to " + to + ".");
    }
}