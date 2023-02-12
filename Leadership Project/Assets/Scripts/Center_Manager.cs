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
        public Dictionary<string, object> userData;
        ListenerRegistration userDataListener;
        FirebaseFirestore db;

        private void Awake() {
            Instance = this;
            db = FirebaseFirestore.DefaultInstance;
            authManager = GetComponent<Authentication>();

        }

        public void SetupListener(string uid)
        {
            DocumentReference docRef = db.Collection("user_data").Document(uid);
            userDataListener = docRef.Listen(snapshot => {
                Debug.Log("Callback received document snapshot.");
                Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
                userData = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in userData) {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }
            });
        }

        public void DeactivateListener()
        {
            userDataListener.Stop();
        }
    }
}
