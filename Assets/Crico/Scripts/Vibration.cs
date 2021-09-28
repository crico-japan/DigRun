using UnityEngine;

namespace Crico
{
    public class Vibration : MonoBehaviour
    {
        [SerializeField] bool doOnStart = false;

        private void Start()
        {
            if (doOnStart)
                DoVibrate();
        }

        public void DoVibrate()
        {
            VibrationManager.instance.DoVibrate();
        }
    }

}
