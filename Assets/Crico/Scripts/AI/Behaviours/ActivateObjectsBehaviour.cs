using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.AI.Behaviours
{
    public class ActivateObjectsBehaviour : AgentBehaviour
    {
        [SerializeField] GameObject[] objectsToActivate;
        [SerializeField] bool deactivateOnStop = false;

        private void Awake()
        {
            Assert.IsNotNull(objectsToActivate);
            foreach (GameObject objectToActivate in objectsToActivate)
                Assert.IsNotNull(objectToActivate);
        }

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            foreach (GameObject objectToActivate in objectsToActivate)
                objectToActivate.SetActive(true);
        }

        public override void StopRunning()
        {
            base.StopRunning();

            if (deactivateOnStop)
                foreach (GameObject objectToActivate in objectsToActivate)
                    objectToActivate.SetActive(false);

        }
    }

}
