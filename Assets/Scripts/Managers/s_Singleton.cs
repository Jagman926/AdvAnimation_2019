using UnityEngine;

namespace Managers
{
    public class s_Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<T>();

                else if (instance != FindObjectOfType<T>())
                    Destroy(FindObjectOfType<T>());

                return instance;
            }
        }
    }
}
