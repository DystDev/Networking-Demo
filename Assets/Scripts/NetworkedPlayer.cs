using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NetworkedPlayer : NetworkBehaviour
{

    [SerializeField] private TMP_Text dispNameText = null;
    [SerializeField] private Renderer dispColorRenderer = null;


    [SyncVar]
    [SerializeField]
    private string dispName = "Missing";

    [SyncVar(hook = nameof(HandlePlayerColorChange))]
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
    
    private void HandlePlayerColorChange(Color oldColor, Color newColor)
    {
        dispColorRenderer.material.SetColor("_BaseColor", newColor);
    }
}
