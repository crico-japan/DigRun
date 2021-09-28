using Crico.AI;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Crico
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] UnityEvent startEvent = new UnityEvent();
        [SerializeField] UnityEvent winEvent = new UnityEvent();

        [SerializeField]
        DestructibleTerrain destructibleTerrain = null;

        [SerializeField]
        RuntimeCircleClipper runtimeCircleClipper = null;

        [SerializeField]
        AgentStatus player = null;

        Trigger mainCameraStartStopTrigger = null;
        TargetHolder mainCameraTargetHolder = null;

        bool playing = false;
        bool gameOver = false;
        bool stageWon = false;


        private void AssertInspectorVars()
        {
            Assert.IsNotNull(player);
        }

        private void Awake()
        {
            AssertInspectorVars();
        }

        public void Init(Trigger mainCameraStartStopTrigger, Camera camera, TargetHolder cameraTargetHolder, TouchSensor touchSensor)
        {
            this.mainCameraStartStopTrigger = mainCameraStartStopTrigger;
            this.mainCameraTargetHolder = cameraTargetHolder;

            runtimeCircleClipper.Init(camera);
        }

        private bool CheckStageWon()
        {
            bool result = false;
            
            return result;
        }

        private bool CheckStageLost()
        {
            bool result = false;

            return result;
        }

        public void StopCamera()
        {
            mainCameraStartStopTrigger.SetTrigger();
        }

        private void Update()
        {
            if (!playing || gameOver)
                return;

            bool stageLost = CheckStageLost();

            gameOver = stageLost;

            if (!gameOver)
            {
                stageWon = CheckStageWon();
                if (stageWon)
                {
                    winEvent.Invoke();
                }

                gameOver = stageWon;
            }

        }

        public void StartPlaying()
        {
            playing = true;
            startEvent.Invoke();
            mainCameraStartStopTrigger.SetTrigger();
        }

        public void StopPlaying()
        {

        }

        public bool IsStageWon()
        {
            return stageWon;
        }

        public bool IsGameOver()
        {
            return gameOver;
        }

    }

}
