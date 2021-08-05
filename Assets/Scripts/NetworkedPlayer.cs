using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NetworkedPlayer : NetworkBehaviour
{

    [SerializeField] private TMP_Text dispNameText = null;
    [SerializeField] private Renderer dispColorRenderer = null;


    [SyncVar(hook = nameof(HandlePlayerNameChange))]
    [SerializeField]
    private string dispName = "Missing";

    [SyncVar(hook = nameof(HandlePlayerColorChange))]
    [SerializeField]
    private Color dispColor = Color.black;

    #region Server

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

    [Server]
    public bool IsSuitable(string word)
    {
        
        foreach (string badWord in ProfanityWords.profanityArray)
        {
            if (word.Contains(badWord) == true)
            {
                return false;
            }
        }
        foreach (char letter in word)
        {
            foreach (char letterInAllowed in AllowedLetters.allowedChars)
            {
                if ((letter == letterInAllowed) == false)
                {
                    return false;
                }
            }
        }
        if (word.Contains(" ") == true)
        {
            return false;
        }
        else if (word.Length < 3 || word.Length > 10)
        {
            return false;
        }
        else { return true; }
    }

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        
        if (IsSuitable(newDisplayName) == false)
        {
            return;
        }
        SetDispName(newDisplayName);
        RpcNotifyDisplayName(newDisplayName);

    }

    [ClientRpc]
    private void RpcNotifyDisplayName(string newName)
    {
        Debug.Log($"New name: {newName}");
    }

    #endregion

    #region Client


    private void HandlePlayerColorChange(Color oldColor, Color newColor)
    {
        dispColorRenderer.material.SetColor("_BaseColor", newColor);
    }

    private void HandlePlayerNameChange(string oldName, string newName) 
    {
        dispNameText.text = newName;
    }
    [ContextMenu("Set Name")]
    public void ClientSetName()
    {
        CmdSetDisplayName("Name has been set from here"); 
    }

    #endregion
}
