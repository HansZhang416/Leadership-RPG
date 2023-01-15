using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

            if (loginTask.Exception == null)
            {
                user = loginTask.Result;
            }
        }

        public IEnumerator Register(string username, string email, string password)
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);
        
            if (registerTask.Exception == null)
            {   
                user = registerTask.Result;
            }
        }

        public void Logout()
        {
            auth.SignOut();
            // signedIn = false;
            auth.StateChanged -= AuthStateChanged;
            auth = null;

            // Go back to manager's scene
        }
    }
}

