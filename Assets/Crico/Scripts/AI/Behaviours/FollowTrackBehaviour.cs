using UnityEngine;

namespace Crico.AI.Behaviours
{
    public class FollowTrackBehaviour : AgentBehaviour
    {
        [SerializeField] float speed = 20f;

        float distance;

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            distance = agent.GetComponent<TrackHolder>().GetApproxDistanceAlongTrack(agent.Position);
        }

        public override void Process(Agent agent)
        {
            base.Process(agent);

            distance += speed * Time.deltaTime;

            Vector3 direction;
            Vector3 normal;
            Vector3 pos = agent.GetComponent<TrackHolder>().GetPositionAndOrientation(distance, out direction, out normal);
            Vector3 up = Vector3.Cross(direction, normal).normalized;

            Transform agentTransform = agent.transform;

            agentTransform.position = pos;
            agentTransform.LookAt(pos + direction, up);
        }

    }

}
