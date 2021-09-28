using Crico.TrackCreation;
using UnityEngine;

namespace Crico.AI
{
    public class TrackHolder : MonoBehaviour
    {
        [SerializeField] Track track;


        public Vector3 GetPositionAndOrientation(float distMovedFromOrigin, out Vector3 direction, out Vector3 normal)
        {
            return track.GetPositionAndOrientation(distMovedFromOrigin, out direction, out normal);
        }

        public float GetApproxDistanceAlongTrack(Vector3 currentPos)
        {
            return track.GetApproxDistanceAlongTrack(currentPos);
        }

        public float CalcTrackLength()
        {
            return track.CalcLength();
        }

        public Vector3 GetClosestPointTo(Vector3 pos)
        {
            int ignore = 0;
            return track.GetClosestPointTo(pos, out ignore);
        }

    }

}
