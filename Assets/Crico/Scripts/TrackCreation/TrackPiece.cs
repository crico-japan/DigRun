using PathCreation;
using PathCreation.Examples;
using UnityEngine;

namespace Crico.TrackCreation
{
    [RequireComponent(typeof(PathCreator))]
    [RequireComponent(typeof(RoadMeshCreator))]
    [RequireComponent(typeof(CollisionMeshCreator))]
    public class TrackPiece : MonoBehaviour
    {
        private VertexPath VertexPath { get => GetComponent<PathCreator>().path; }
        public float Length { get => VertexPath.length; }
        public Vector3 EndPoint { get => VertexPath.GetPointAtTime(1f, EndOfPathInstruction.Stop); }

        public void UpdateMesh()
        {
            GetComponent<RoadMeshCreator>().TriggerUpdate();
            GetComponent<CollisionMeshCreator>().CreateMeshAndCollider();
        }

        public Vector3 GetPoint(float t)
        {
            return VertexPath.GetPointAtTime(t, EndOfPathInstruction.Stop);
        }

        public Vector3 GetDirection(float t)
        {
            return VertexPath.GetDirection(t, EndOfPathInstruction.Stop);
        }

        public Vector3 GetNormal(float t)
        {
            return VertexPath.GetNormal(t, EndOfPathInstruction.Stop);
        }

        public Vector3 GetClosestPointOnPath(Vector3 input)
        {
            return VertexPath.GetClosestPointOnPath(input);
        }

        public float GetClosestDistanceAlongPath(Vector3 input)
        {
            return VertexPath.GetClosestDistanceAlongPath(input);
        }


    }

}
