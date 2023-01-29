using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class TitleScreenHandler : MonoBehaviour
{
    public void LogoutButton()
    {
        Center_Manager.Instance.authManager.Logout();
    }
}
