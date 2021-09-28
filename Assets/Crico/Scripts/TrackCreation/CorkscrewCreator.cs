using PathCreation;
using UnityEditor;
using UnityEngine;

namespace Crico.TrackCreation
{
    [RequireComponent(typeof(PathCreator))]
    public class CorkscrewCreator : MonoBehaviour
    {
        [SerializeField] private int turns = 4;
        [SerializeField] private float radius = 1f;
        [SerializeField] private float fullRotation = 155f;

        public void Create()
        {
            PathCreator pathCreator = GetComponent<PathCreator>();

            BezierPath bezierPath = pathCreator.bezierPath;
            for (int i = bezierPath.NumSegments - 1; i >= 0; --i)
            {
                bezierPath.DeleteSegment(i);
            }

            float arcAngle = 90f;
            float arc = arcAngle * Mathf.Deg2Rad;
            float halfArc = arc / 2f;

            for (int i = 0; i < turns - 1; ++i)
            {
                bezierPath.AddSegmentToEnd(Vector3.zero);
            }

            Vector3 b3 = new Vector3(radius * Mathf.Cos(halfArc), radius * Mathf.Sin(halfArc), 0f);
            Vector3 b0 = new Vector3(b3.x, -b3.y, 0f);
            Vector2 b2 = new Vector3((4f * radius - b3.x) / 3f, ((radius - b3.x) * (3f * radius - b3.x)) / (3f * b3.y), 0f);
            Vector2 b1 = new Vector3(b2.x, -b2.y, 0f);

            Vector3 offset = -b0 + new Vector3(0f, 0f, 1.5f);

            Vector3 zStep = new Vector3(0f, 0f, 0.25f);

            int[] indices = new int[4];
            for (int i = 0; i < turns; ++i)
            {
                int initialIndex = i * 4 - i;
                Quaternion rotate = Quaternion.Euler(0f, 0f, (float)i * arcAngle);

                int index = 0;
                indices[0] = initialIndex + index++;
                indices[1] = initialIndex + index++;
                indices[2] = initialIndex + index++;
                indices[3] = initialIndex + index++;

                if (i == 0)
                    bezierPath.SetPoint(indices[0], offset + rotate * b0 + indices[0] * zStep, true);

                bezierPath.SetPoint(indices[1], offset + rotate * b1 + indices[1] * zStep, true);
                bezierPath.SetPoint(indices[2], offset + rotate * b2 + indices[2] * zStep, true);
                bezierPath.SetPoint(indices[3], offset + rotate * b3 + indices[3] * zStep, true);
            }

            for (int i = 0; i < bezierPath.NumAnchorPoints; ++i)
            {
                float angle = (float)i * (fullRotation / 4f);
                bezierPath.SetAnchorNormalAngle(i, angle);
            }

            bezierPath.AddSegmentToStart(new Vector3(0f, 0f, 0f));
            bezierPath.SetPoint(1, new Vector3(0f, 0f, 0.5f));

            Vector3 endPoint = new Vector3(0f, 0f, (float)(bezierPath.NumSegments) + 1f);
            bezierPath.AddSegmentToEnd(endPoint);
            bezierPath.SetPoint(bezierPath.NumPoints - 1, endPoint);
            bezierPath.SetPoint(bezierPath.NumPoints - 2, endPoint - new Vector3(0f, 0f, 0.5f));
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(CorkscrewCreator))]
        public class CorkscrewCreatorEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                if (GUILayout.Button("Create Corkscrew"))
                {
                    CorkscrewCreator myScript = (CorkscrewCreator)target;
                    myScript.Create();
                }
            }
        }
#endif
    }
}

