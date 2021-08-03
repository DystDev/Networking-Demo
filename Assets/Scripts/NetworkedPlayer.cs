using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedPlayer : NetworkBehaviour
{
    [SyncVar]
    [SerializeField]
    private string dispName = "Missing";

    [SyncVar]
    [SerializeField]
    private Color dispColor = Color.black;


    [Server]
    public void SetDispName(string newDispName)
    {
        dispName = newDispName;
    }

    [Server]
    public void SetDispColor(Color newDispColor)
    {
        dispColor = newDispColor;
    }
}
