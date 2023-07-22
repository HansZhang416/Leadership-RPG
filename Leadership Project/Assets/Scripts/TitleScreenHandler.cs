using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using TMPro;

public class TitleScreenHandler : MonoBehaviour
{
    public TMP_InputField friend_search;

    void Start()
    {
        Center_Manager.Instance.saveLoadManager.ListenForUserData();
        Debug.Log(Center_Manager.Instance);
    }

    public void LogoutButton()
    {
        Center_Manager.Instance.authManager.Logout();
    }

    public void AddFriend() // sends a friend request
    {

    }

    public void AddFriendDebug() // automatically add to friend list
    {
        Center_Manager.Instance.authManager.AddFriend(friend_search.text);
    }
}
