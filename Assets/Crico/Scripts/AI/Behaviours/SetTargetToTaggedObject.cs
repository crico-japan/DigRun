using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.AI.Behaviours
{
    public class SetTargetToTaggedObject : AgentBehaviour
    {
        [SerializeField] string objectTag = "Player";

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            TargetHolder targetHolder = agent.GetComponent<TargetHolder>();
            Assert.IsNotNull(targetHolder);

            GameObject objectWithTag = GameObject.FindGameObjectWithTag(objectTag);
            targetHolder.target = objectWithTag;
        }
    }

}
