using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.TrackCreation
{
    public class Track : MonoBehaviour
    {
        [SerializeField] private EndZone endZone;
        [SerializeField] private List<TrackPiece> pieces;

        private void AssertInspectorVars()
        {
            Assert.IsNotNull(pieces);
            foreach (TrackPiece piece in pieces)
                Assert.IsNotNull(piece);
        }

        private void Awake()
        {
            AssertInspectorVars();
        }

        public bool IsPlayerInEndZone()
        {
            return endZone.playerEntered;
        }

        public void SetEndZone(EndZone endZone)
        {
            this.endZone = endZone;
        }

        public void SetPieces(List<TrackPiece> pieces)
        {
            this.pieces = pieces;
        }

        public float CalcLength()
        {
            float length = 0f;
            for (int i = 0; i < pieces.Count; ++i)
            {
                length += pieces[i].Length;
            }

            return length;
        }

        public Vector3 GetClosestPointTo(Vector3 pos, out int indexOfPiece)
        {
            float dist = float.MaxValue;
            indexOfPiece = -1;
            Vector3 closestPointOnTrack = new Vector3();
            for (int i = 0; i < pieces.Count; ++i)
            {
                TrackPiece piece = pieces[i];
                Vector3 closestPoint = piece.GetClosestPointOnPath(pos);
                float currentDist = (closestPoint - pos).magnitude;
                if (currentDist < dist)
                {
                    dist = currentDist;
                    indexOfPiece = i;
                    closestPointOnTrack = closestPoint;
                }
            }

            return closestPointOnTrack;
        }

        public float GetApproxDistanceAlongTrack(Vector3 currentPos)
        {
            float result = 0f;

            int indexOfPiece = -1;
            Vector3 closestPointOnTrack = GetClosestPointTo(currentPos, out indexOfPiece);

            for (int i = 0; i < indexOfPiece; ++i)
                result += pieces[i].Length;

            result += pieces[indexOfPiece].GetClosestDistanceAlongPath(closestPointOnTrack);

            return result;
        }

        public Vector3 GetPositionAndOrientation(float distMovedFromOrigin, out Vector3 direction, out Vector3 normal)
        {
            Vector3 newPos = new Vector3();

            int indexOfCurrent = -1;
            float t = 0f;
            for (int i = 0; i < pieces.Count; ++i)
            {
                TrackPiece piece = pieces[i];
                float length = piece.Length;
                if (distMovedFromOrigin > length)
                {
                    distMovedFromOrigin -= length;
                }
                else
                {
                    indexOfCurrent = i;
                    t = distMovedFromOrigin / length;
                    break;
                }
            }

            if (indexOfCurrent == -1)
            {
                indexOfCurrent = pieces.Count - 1;
                t = 1f;
            }

            TrackPiece current = pieces[indexOfCurrent];
            newPos = current.GetPoint(t);

            direction = current.GetDirection(t);
            normal = current.GetNormal(t);

            return newPos;
        }

    }

}
