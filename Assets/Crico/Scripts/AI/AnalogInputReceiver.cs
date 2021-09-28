using UnityEngine;

namespace Crico
{
    public class AnalogInputReceiver : MonoBehaviour
    {
        public float normalizedMag { get; private set; } = 0f;
        public Vector2 inputDir { get; private set; } = new Vector2();

        public void OnAnalogInput(float normalizedMag, Vector2 inputDir)
        {
            this.normalizedMag = normalizedMag;
            this.inputDir = inputDir;
        }

    }

}
