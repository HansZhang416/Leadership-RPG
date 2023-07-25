using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

namespace Managers
{
    public class Center_Manager : MonoBehaviour
    {
        public static Center_Manager Instance;
        public Authentication authManager;
        public SaveLoadManager saveLoadManager;
        public Dictionary<string, object> userData;
        ListenerRegistration userDataListener;
        FirebaseFirestore db;
        [HideInInspector] public bool setup;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
                return;
            }
            db = FirebaseFirestore.DefaultInstance;
            authManager = GetComponent<Authentication>();
            saveLoadManager = GetComponent<SaveLoadManager>();

            setup = true;
        }

        public void SetupListener(string uid)
        {
            // DocumentReference docRef = db.Collection("user_data").Document(uid);
            // userDataListener = docRef.Listen(snapshot => {
            //     Debug.Log("Callback received document snapshot.");
            //     Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
            //     userData = snapshot.ToDictionary();
            //     foreach (KeyValuePair<string, object> pair in userData) {
            //         Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
            //     }
            // });

            if (userDataListener != null)
            {
                DeactivateListener();
            }

            Authentication auth = Center_Manager.Instance.authManager;
            db = FirebaseFirestore.DefaultInstance;
            DocumentReference docRef = db.Collection("user_data").Document(auth.user.UserId);
            userDataListener = docRef.Listen( snapshot => {
                Debug.Log($"Document data for {snapshot.Id} document: ");
                Dictionary<string, object> userData = snapshot.ToDictionary();
                saveLoadManager.currentUserData = userData;

                foreach (KeyValuePair<string, object> pair in saveLoadManager.currentUserData) {
                    Debug.Log($"{pair.Key}: {pair.Value}");
                }
            });

        }

        public void DeactivateListener()
        {
            userDataListener.Stop();
        }

        void OnDestory()
        {
            userDataListener.Stop();
        }

        void OnApplicationQuit()
        {
            userDataListener.Stop();
        }
    }
}
