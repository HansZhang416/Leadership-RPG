using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Managers;
using UnityEngine.SceneManagement;

//managers do backend things (things cannot be seen), handlers serves as a bridge between user and manager.

public class AuthHandler : MonoBehaviour
{
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;

    [Header("Register")]
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField confirmPasswordRegisterField;


    void Update()
    {
        if (Authentication.signedIn && SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void LoginButton()
    {
        Authentication auth = Center_Manager.Instance.authManager;
        StartCoroutine(auth.Login(emailLoginField.text, passwordLoginField.text));
    }

    public void SignupButton()
    {
        if (passwordRegisterField.text == confirmPasswordRegisterField.text)
        {
            Debug.Log("Attempting to create user...");
            Authentication auth = Center_Manager.Instance.authManager;
            StartCoroutine(auth.Register(emailRegisterField.text, passwordRegisterField.text));
        }
        else
        {
            Debug.Log("Passwords do not match!");
        }
        
    }
}
