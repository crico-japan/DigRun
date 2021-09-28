using UnityEngine;
using UnityEngine.SceneManagement;

namespace Crico
{
    public class MainScene : MonoBehaviour
    {

        private float timeScale = 1f;

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void OnDestroy()
        {
            Time.timeScale = 1.0f;
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                ReloadScene();

            if (Input.GetKeyDown(KeyCode.S))
                Time.timeScale = 0.1f;
            if (Input.GetKeyUp(KeyCode.S))
                Time.timeScale = 1f;

        }
#endif
        public void LoadStageIndex(int index)
        {
            GameData.instance.saveData.stageIndex = index;
            ReloadScene();
        }


        public void IncrementStageIndexAndSave()
        {
            GameData gameData = GameData.instance;
            SaveData saveData = gameData.saveData;
            saveData.stageIndex++;
            if (saveData.stageIndex >= gameData.numStages)
                saveData.stageIndex = 0;

            gameData.SaveSaveData();
        }

        public void SetTimeScale(float value)
        {
            this.timeScale = value;
        }

        public void Pause()
        {
            Time.timeScale = 0f;
        }

        public void Unpause()
        {
            Time.timeScale = this.timeScale;
        }
    }

}
