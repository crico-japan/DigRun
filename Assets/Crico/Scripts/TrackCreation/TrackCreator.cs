using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.TrackCreation
{
    public class TrackCreator : MonoBehaviour
    {
        [System.Serializable]
        private class TrackPieceData
        {
            public char code = '\0';
            public TrackPiece prefab = null;
            public float angleChange = 0f;
            public bool placeObstacles = false;
            public Vector3 obstaclesOffset = new Vector3();
        }

        [SerializeField] private string trackCode = null;
        [SerializeField] private Track emptyTrackPrefab = null;
        [SerializeField] private Transform trackEndPrefab = null;
        [SerializeField] private Transform obstaclesPrefab = null;
        [SerializeField] private List<TrackPieceData> pieceDatas = null;

        private void AssertInspectorVars()
        {
            Assert.IsNotNull(emptyTrackPrefab);
            Assert.IsNotNull(trackEndPrefab);
            Assert.IsNotNull(obstaclesPrefab);
            Assert.IsNotNull(pieceDatas);
            foreach (TrackPieceData pieceData in pieceDatas)
                Assert.IsNotNull(pieceData);
        }

        private void Awake()
        {
            AssertInspectorVars();
        }

        private TrackPieceData FindTrackPieceData(char code)
        {
            TrackPieceData result = pieceDatas.Find(x => x.code == code || x.code == char.ToUpper(code));
            return result;
        }

        private void CreateTrack()
        {
            Track track = Instantiate(emptyTrackPrefab, transform);
            track.transform.localPosition = new Vector3();

            float currentAngle = 0f;
            TrackPiece last = null;

            Transform trackStart = Instantiate(trackEndPrefab, track.transform);
            trackStart.position = new Vector3();

            List<TrackPiece> pieces = new List<TrackPiece>();
            foreach (char code in trackCode)
            {
                TrackPieceData pieceData = FindTrackPieceData(code);
                if (pieceData == null)
                {
                    Debug.LogError("ERROR: No data for code: " + code);
                    break;
                }

                Vector3 startPoint = Vector3.zero;

                TrackPiece newPiece = Instantiate(pieceData.prefab, track.transform);

                if (last != null)
                {
                    startPoint = last.EndPoint;
                }

                Transform newPieceTrans = newPiece.transform;
                newPieceTrans.position = startPoint;
                newPieceTrans.localRotation = Quaternion.Euler(new Vector3(0f, currentAngle));
                currentAngle += pieceData.angleChange;

                last = newPiece;

                if (pieceData.placeObstacles)
                {
                    Transform obstacles = Instantiate(obstaclesPrefab, newPiece.transform);
                    obstacles.localPosition = pieceData.obstaclesOffset;
                }

                EndZone endZone = newPiece.gameObject.GetComponentInChildren<EndZone>();
                if (endZone != null)
                    track.SetEndZone(endZone);

                pieces.Add(newPiece);
            }

            foreach (TrackPiece piece in pieces)
            {
                piece.UpdateMesh();
            }

            track.SetPieces(pieces);

            Transform trackEnd = Instantiate(trackEndPrefab, track.transform);
            trackEnd.localRotation = Quaternion.Euler(new Vector3(0f, currentAngle + 180f));
            trackEnd.position = pieces[pieces.Count - 1].GetPoint(1f);
        }


#if UNITY_EDITOR
        [CustomEditor(typeof(TrackCreator))]
        public class TrackCreatorEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                if (GUILayout.Button("CreateFromCode"))
                {
                    TrackCreator myScript = (TrackCreator)target;
                    myScript.CreateTrack();
                }
            }
        }
#endif
    }
}
