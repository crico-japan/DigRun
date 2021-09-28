using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Crico
{
    public class VibrationManager : MonoBehaviour
    {
        static public VibrationManager instance { get; private set; } = null;

        [SerializeField] float timeBetweenVibrations = 1f;

        float currentTime = 0f;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (currentTime > 0f)
                currentTime -= Time.deltaTime;
        }

        public void DoVibrate()
        {
            if (currentTime > 0f)
                return;

            currentTime = timeBetweenVibrations;

#if UNITY_IOS || UNITY_ANDROID
            Handheld.Vibrate();
#endif
        }

    }

}
