using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class Center_Manager : MonoBehaviour
    {
        public static Center_Manager Instance;
        public Authentication authManager;

        private void Awake() {
            Instance = this;

            authManager = GetComponent<Authentication>();

        }
    }
}
