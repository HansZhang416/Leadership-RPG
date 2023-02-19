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
        void Start()
        {
            db = FirebaseFirestore.DefaultInstance;
            docRef = db.Collection("user_data").Document(Center_Manager.Instance.authManager.user.UserId);
            docRef.Listen(snapshot => {
                Debug.Log($"New user data received: {snapshot}"); //inserts a variable into the code using $ and {}
                Dictionary<string, object> user_data = snapshot.ToDictionary();
                Debug.Log($"Here is the play's level: {user_data["level"]}");
            });
        }
    }
}