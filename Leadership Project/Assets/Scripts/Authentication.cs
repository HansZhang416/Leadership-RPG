using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;

namespace Managers
{
    public class Authentication : MonoBehaviour
    {
        public DependencyStatus dependencyStatus;
        public FirebaseAuth auth;
        public FirebaseUser user;

        public static bool signedIn;

        // //basically multitasking = Asynchronous
        void Awake() 
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => 
                {
                    dependencyStatus = task.Result;
                    if (dependencyStatus == DependencyStatus.Available)
                    {
                        InitializeFirebase();
                    }
                    else
                    {
                        Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
                    }
                }
            );
        }

        void InitializeFirebase()
        {
            auth = FirebaseAuth.DefaultInstance;
            auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
        }


        void AuthStateChanged(object sender, System.EventArgs eventArgs)
        {
            if (auth.CurrentUser != user)
            {
                bool loggedIn = (auth.CurrentUser != null);

                if (!loggedIn && user != null)
                {
                    Debug.Log("Signed out");
                    signedIn = false;
                }

                user = auth.CurrentUser;

                if (loggedIn)
                {
                    // login stuff here
                    signedIn = true;
                    
                }
            }
        }

        public IEnumerator Login(string email, string password)
        {
            Debug.Log("Login task pending...");
            var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

            if (loginTask.Exception == null)
            {
                user = loginTask.Result;
                Debug.Log("Login successful!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        public IEnumerator Register(string email, string password)
        {
            Debug.Log("Register task pending...");
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);
        
            if (registerTask.Exception == null)
            {   
                user = registerTask.Result;
                Debug.Log("User created successfully!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        public void Logout()
        {
            auth.SignOut();
            // signedIn = false;
            auth.StateChanged -= AuthStateChanged;
            auth = null;

            // Go back to manager's scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}

