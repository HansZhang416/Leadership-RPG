using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

namespace Managers
{
    public class SaveLoadManager : MonoBehaviour
    {
        FirebaseFirestore db;
        DocumentReference docRef;
        ListenerRegistration listener;
        public Dictionary<string, object> currentUserData;
        public Dictionary<string, Item> itemDict;
        public ItemList itemList;
        void Start()
        {
            // db = FirebaseFirestore.DefaultInstance;
            // docRef = db.Collection("user_data").Document(Center_Manager.Instance.authManager.user.UserId);
            // docRef.Listen(snapshot => {
            //     Debug.Log($"New user data received: {snapshot}"); //inserts a variable into the code using $ and {}
            //     Dictionary<string, object> user_data = snapshot.ToDictionary();
            //     Debug.Log($"Here is the play's level: {user_data["level"]}");
            // });

            // load items
            itemDict = new Dictionary<string, Item>();
            foreach (Item item in itemList.items)
            {
                itemDict.Add(item.name, item);
            }
        }

        public void ListenForUserData()
        {
            if (listener != null)
            {
                StopListening();
            }

            Authentication auth = Center_Manager.Instance.authManager;
            db = FirebaseFirestore.DefaultInstance;
            docRef = db.Collection("user_data").Document(auth.user.UserId);
            listener = docRef.Listen( snapshot => {
                Debug.Log($"Document data for {snapshot.Id} document: ");
                Dictionary<string, object> userData = snapshot.ToDictionary();
                currentUserData = userData;

                foreach (KeyValuePair<string, object> pair in currentUserData) {
                    Debug.Log($"{pair.Key}: {pair.Value}");
                }
            });

            
        }

        public void StopListening()
        {
            listener.Stop();
        }
    }
}