using UnityEngine;

namespace Crico.AI
{
    public class StageHolder : MonoBehaviour
    {
        [SerializeField] Stage _stage;

        public Stage stage { get => _stage; set => _stage = value; }

        public void StartStagePlaying()
        {
            stage.StartPlaying();
        }

        public void StopStagePlaying()
        {
            stage.StopPlaying();
        }

    }
}
