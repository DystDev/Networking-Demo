using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            if (word.ToLower().Contains(badWord) == true)
            {
                Debug.Log("Profanity Detected - Username Denied");
                return false;
                
            }
        }
        char[] charWord = word.ToCharArray();
        char[] allowedLetters = AllowedLetters.allowedChars;
        foreach (char letter in charWord)
        {
            if (allowedLetters.Contains(letter) == false)
            {
                Debug.Log("Disallowed Letter Detected - Username Denied");
                return false;
            }
            
        }
        if (word.Contains(" ") == true)
        {
            Debug.Log("Whitespace Detected - Username Denied");
            return false;
        }
        else if (word.Length < 3 || word.Length > 10)
        {
            Debug.Log("Incorrect Length Detected - Username Denied");
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
        CmdSetDisplayName("dababy"); 
    }

    #endregion
}
