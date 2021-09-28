using Crico.AI;
using UnityEngine;
using UnityEngine.Assertions;

namespace Crico
{
    public class StageLoader : MonoBehaviour
    {
        [SerializeField] StageHolder holderToSetStageTo = null;
        [SerializeField] string folderName = "Stages";
        [SerializeField] Trigger mainCameraTrigger = null;
        [SerializeField] Camera mainCamera = null;
        [SerializeField] TargetHolder mainCameraTargetHolder = null;
        [SerializeField] TouchSensor touchSensor = null;

        private void AssertInspectorVars()
        {
            Assert.IsNotNull(holderToSetStageTo);
            Assert.IsNotNull(mainCameraTrigger);
            Assert.IsNotNull(mainCamera);
            Assert.IsNotNull(mainCameraTargetHolder);
            Assert.IsNotNull(touchSensor);
        }

        private void Awake()
        {
            AssertInspectorVars();
        }
        private void Start()
        {
            LoadStage();
        }

        private void LoadStage()
        {
            int stageIndex = GameData.instance.saveData.stageIndex;

            string stageName = stageIndex.ToString("D3");
            string stagePath = folderName + "/" + stageName;

            Stage stage = GameObject.FindObjectOfType<Stage>();
            if (stage == null)
            {
                Stage stagePrefab = Resources.Load<Stage>(stagePath);

                Assert.IsNotNull(stagePrefab);

                stage = Instantiate(stagePrefab);

            }

            stage.Init(mainCameraTrigger, mainCamera, mainCameraTargetHolder, touchSensor);

            holderToSetStageTo.stage = stage;
        }


    }

}
