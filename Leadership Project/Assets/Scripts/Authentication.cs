using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;

namespace Managers
{
    public class Authentication : MonoBehaviour
    {
        public DependencyStatus dependencyStatus;
        public FirebaseAuth auth;
        public FirebaseUser user;
        FirebaseFirestore db;

        public static bool signedIn;

        // //basically multitasking = Asynchronous
        void Start() 
        {
            while(!Center_Manager.Instance.setup) {}
            InitializeFirebase();
            // FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => 
            //     {
            //         dependencyStatus = task.Result;
            //         if (dependencyStatus == DependencyStatus.Available)
            //         {
            //             InitializeFirebase();
            //         }
            //         else
            //         {
            //             Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            //         }
            //     }
            // );
        }

        void InitializeFirebase()
        {
            db = FirebaseFirestore.DefaultInstance;
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
                    Center_Manager.Instance.DeactivateListener();
                    signedIn = false;
                }

                user = auth.CurrentUser;

                if (loggedIn)
                {
                    // login stuff here
                    Debug.Log("Signed in");
                    Center_Manager.Instance.SetupListener(user.UserId);
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
                Center_Manager.Instance.SetupListener(user.UserId);
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
                Debug.Log($"User {user.UserId} created successfully!");
                DocumentReference docRef = db.Collection("user_data").Document(user.UserId);

                Dictionary<string, object> initialUserData = new Dictionary<string, object>
                {
                    {"level", 1},
                    {"profile", "0 0 0 -1 -1 -1 -1 -1"}
                };

                docRef.SetAsync(initialUserData).ContinueWithOnMainThread(task => {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                });
            }
        }

        public void AddFriend(string friend_id)
        {
            DocumentReference docRef = db.Collection("user_data").Document(user.UserId);
            DocumentReference friendRef = db.Collection("user_data").Document(friend_id);

            Dictionary<string, object> myFriends = new Dictionary<string, object>
            {
                {"friends", new List<object>() {friend_id}},
            };

            Dictionary<string, object> theirFriends = new Dictionary<string, object>
            {
                {"friends", new List<object>() {user.UserId}},
            };

            docRef.UpdateAsync(myFriends).ContinueWithOnMainThread(task => {
                friendRef.UpdateAsync(theirFriends);
            });

        }

        public void Logout()
        {
            auth.SignOut();
            // signedIn = false;
            Center_Manager.Instance.DeactivateListener();
            auth.StateChanged -= AuthStateChanged;
            auth = null;
            signedIn = false;

            // Go back to manager's scene
            SceneManager.LoadScene(0);
        }
    }
}

