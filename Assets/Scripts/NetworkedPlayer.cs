using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedPlayer : NetworkBehaviour
{
    [SyncVar]
    [SerializeField]
    private string dispName = "Missing";


    [Server]
    public void SetDispName(string newDispName)
    {
        dispName = newDispName;
    }
}
