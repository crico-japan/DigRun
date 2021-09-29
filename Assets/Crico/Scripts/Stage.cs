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
        FragmentGenerator fragmentGenerator = null;

        [SerializeField]
        GameObject player = null;

        Trigger mainCameraStartStopTrigger = null;
        TargetHolder mainCameraTargetHolder = null;
        TouchSensor touchSensor = null;

        bool playing = false;
        bool gameOver = false;
        bool stageWon = false;


        private void AssertInspectorVars()
        {
            Assert.IsNotNull(player);
            Assert.IsNotNull(runtimeCircleClipper);
            Assert.IsNotNull(fragmentGenerator);
        }

        private void Awake()
        {
            AssertInspectorVars();
        }

        public void Init(Trigger mainCameraStartStopTrigger, Camera camera, TargetHolder cameraTargetHolder, TouchSensor touchSensor)
        {
            this.mainCameraStartStopTrigger = mainCameraStartStopTrigger;
            this.mainCameraTargetHolder = cameraTargetHolder;
            this.touchSensor = touchSensor;
            runtimeCircleClipper.Init(camera);
            fragmentGenerator.Init(camera);
            InputReceiver inputReceiver = runtimeCircleClipper.GetComponent<InputReceiver>();
            touchSensor.onDragInCanvasUnits.AddListener(inputReceiver.OnDrag);
            touchSensor.onPointerDown.AddListener(inputReceiver.OnPointerDown);
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

            //
            mainCameraTargetHolder.SetTarget(player.gameObject);

            player.GetComponent<GravityFreeAgent>().isRunning = true;
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
