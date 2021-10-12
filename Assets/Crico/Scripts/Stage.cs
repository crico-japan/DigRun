using Crico.AI;
using Crico.AI.States;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Crico
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] UnityEvent startEvent = new UnityEvent();
        [SerializeField] UnityEvent winEvent = new UnityEvent();
        [SerializeField] UnityEvent loseEvent = new UnityEvent();

        [SerializeField]
        DestructibleTerrain destructibleTerrain = null;

        [SerializeField]
        RuntimeCircleClipper runtimeCircleClipper = null;

        [SerializeField]
        FragmentGenerator fragmentGenerator = null;

        [SerializeField]
        StateTransitionOnTapInput transitionOnTapInput = null;

        [SerializeField]
        Obi.ObiParticleRenderer[] particleRenderers;

        [SerializeField]
        AgentStatus player = null;

        Trigger mainCameraStartStopTrigger = null;
        TargetHolder mainCameraTargetHolder = null;
        TouchSensor touchSensor = null;

        bool playing = false;
        bool gameOver = false;
        bool stageWon = false;

        bool playerCharaceterArrived = false;

        [SerializeField]
        private GameObject cameraFollowTarget;
        private void AssertInspectorVars()
        {
            Assert.IsNotNull(player);
            Assert.IsNotNull(runtimeCircleClipper);
            Assert.IsNotNull(fragmentGenerator);
            Assert.IsNotNull(cameraFollowTarget);
            Assert.IsNotNull(transitionOnTapInput);
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

            if(particleRenderers != null)
            {
                camera.GetComponent<Obi.ObiFluidRenderer>().particleRenderers = particleRenderers;
            }

            touchSensor.onPointerUp.AddListener(transitionOnTapInput.OnTap);
        }

        private bool CheckStageWon()
        {
            bool result = playerCharaceterArrived;
            
            return result;
        }

        private bool CheckStageLost()
        {
            bool result = !player.IsAgentActive();

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
            if(stageLost)
            {
                loseEvent.Invoke();
            }

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

            //player.GetComponent<GravityFreeAgent>().isRunning = true;
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

        public void SetPlayerCharaceterArrived()
        {
            playerCharaceterArrived = true;
        }

        public void EnableScroll()
        {
            cameraFollowTarget.SetActive(true);
        }

        public void DisableScroll()
        {
            cameraFollowTarget.SetActive(false);
        }
    }

}
