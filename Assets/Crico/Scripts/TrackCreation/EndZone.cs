using UnityEngine;

namespace Crico.TrackCreation
{
    public class EndZone : MonoBehaviour
    {
        [SerializeField] private string playerTag = "Player";

        public bool playerEntered { get; private set; } = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == playerTag)
                playerEntered = true;
        }
    }

}
