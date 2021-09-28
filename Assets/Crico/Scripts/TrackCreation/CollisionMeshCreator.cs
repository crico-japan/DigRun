using PathCreation;
using PathCreation.Examples;
using UnityEditor;
using UnityEngine;

namespace Crico.TrackCreation
{
    [RequireComponent(typeof(PathCreator))]
    [RequireComponent(typeof(RoadMeshCreator))]
    public class CollisionMeshCreator : MonoBehaviour
    {
        [SerializeField] private int numPathPoints = 2;

        private MeshCollider meshCollider;

        public void CreateMeshAndCollider()
        {
            PathCreator pathCreator = GetComponent<PathCreator>();

            Vector3[] verts = new Vector3[numPathPoints * 4];

            int[] triangles = new int[numPathPoints * 10 * 3 + 6];

            RoadMeshCreator roadMeshCreator = GetComponent<RoadMeshCreator>();
            float xAdjust = roadMeshCreator.roadWidth;
            float yAdjust = roadMeshCreator.thickness;

            Vector3 down = new Vector3();
            int baseIndex;
            Vector3 centrePoint;
            Vector3 right;
            Vector3 rightAdjust;
            Vector3 downAdjust;
            float t;
            float t2;
            Quaternion inverseRotation = Quaternion.Inverse(transform.rotation);
            for (int i = 0; i < numPathPoints - 1; ++i)
            {
                t = (float)i / (float)(numPathPoints - 1);
                t2 = (float)(i + 1) / (float)(numPathPoints - 1); 
                centrePoint = pathCreator.path.GetPointAtTime(t, EndOfPathInstruction.Stop);
                Vector3 nextPoint = pathCreator.path.GetPointAtTime(t2, EndOfPathInstruction.Stop);
                right = pathCreator.path.GetNormal(t, EndOfPathInstruction.Stop);

                centrePoint = transform.InverseTransformPoint(centrePoint);
                nextPoint = transform.InverseTransformPoint(nextPoint);

                right = inverseRotation * right;

                Vector3 forward = nextPoint - centrePoint;
                forward.Normalize();

                down = Vector3.Cross(right, forward);

                rightAdjust = xAdjust * right;
                downAdjust = yAdjust * down;

                baseIndex = i * 4;
                verts[baseIndex + 0] = centrePoint - rightAdjust;
                verts[baseIndex + 1] = centrePoint + rightAdjust;
                verts[baseIndex + 2] = centrePoint - rightAdjust + downAdjust;
                verts[baseIndex + 3] = centrePoint + rightAdjust + downAdjust;
            }

            int finalIndex = numPathPoints - 1;

            t = 1f;
            centrePoint = pathCreator.path.GetPointAtTime(t, EndOfPathInstruction.Stop);
            right = pathCreator.path.GetNormal(t, EndOfPathInstruction.Stop);

            centrePoint = transform.InverseTransformPoint(centrePoint);
            inverseRotation = Quaternion.Inverse(transform.rotation);
            right = inverseRotation * right;

            rightAdjust = xAdjust * right;
            downAdjust = yAdjust * down;

            baseIndex = finalIndex * 4;
            verts[baseIndex + 0] = centrePoint - rightAdjust;
            verts[baseIndex + 1] = centrePoint + rightAdjust;
            verts[baseIndex + 2] = centrePoint - rightAdjust + downAdjust;
            verts[baseIndex + 3] = centrePoint + rightAdjust + downAdjust;

            int indexIndex = 0;
            int baseVertIndex;
            for (int i = 0; i < numPathPoints - 1; ++i)
            {
                baseVertIndex = i * 4;
                int nextBaseVertIndex = (i + 1) * 4;

                // opening
                triangles[indexIndex++] = baseVertIndex + 0;
                triangles[indexIndex++] = baseVertIndex + 1;
                triangles[indexIndex++] = baseVertIndex + 2;
                triangles[indexIndex++] = baseVertIndex + 1;
                triangles[indexIndex++] = baseVertIndex + 3;
                triangles[indexIndex++] = baseVertIndex + 2;

                // top
                triangles[indexIndex++] = baseVertIndex + 0;
                triangles[indexIndex++] = nextBaseVertIndex + 0;
                triangles[indexIndex++] = nextBaseVertIndex + 1;
                triangles[indexIndex++] = nextBaseVertIndex + 1;
                triangles[indexIndex++] = baseVertIndex + 1;
                triangles[indexIndex++] = baseVertIndex + 0;

                // left
                triangles[indexIndex++] = nextBaseVertIndex + 0;
                triangles[indexIndex++] = baseVertIndex + 0;
                triangles[indexIndex++] = baseVertIndex + 2;
                triangles[indexIndex++] = nextBaseVertIndex + 0;
                triangles[indexIndex++] = baseVertIndex + 2;
                triangles[indexIndex++] = nextBaseVertIndex + 2;

                // right
                triangles[indexIndex++] = baseVertIndex + 1;
                triangles[indexIndex++] = nextBaseVertIndex + 1;
                triangles[indexIndex++] = baseVertIndex + 3;
                triangles[indexIndex++] = nextBaseVertIndex + 1;
                triangles[indexIndex++] = nextBaseVertIndex + 3;
                triangles[indexIndex++] = baseVertIndex + 3;

                // bottom
                triangles[indexIndex++] = baseVertIndex + 2;
                triangles[indexIndex++] = baseVertIndex + 3;
                triangles[indexIndex++] = nextBaseVertIndex + 2;
                triangles[indexIndex++] = baseVertIndex + 3;
                triangles[indexIndex++] = nextBaseVertIndex + 3;
                triangles[indexIndex++] = nextBaseVertIndex + 2;
            }

            // end
            baseVertIndex = (numPathPoints - 1) * 4;
            triangles[indexIndex++] = baseVertIndex + 1;
            triangles[indexIndex++] = baseVertIndex + 0;
            triangles[indexIndex++] = baseVertIndex + 3;
            triangles[indexIndex++] = baseVertIndex + 0;
            triangles[indexIndex++] = baseVertIndex + 2;
            triangles[indexIndex++] = baseVertIndex + 3;

            Vector2[] uvs = new Vector2[verts.Length];
            Vector3[] normals = new Vector3[verts.Length];

            if (meshCollider == null)
            {
                meshCollider = gameObject.AddComponent<MeshCollider>();
            }

            Mesh mesh = new Mesh();
            mesh.Clear();
            mesh.vertices = verts;
            mesh.uv = uvs;
            mesh.normals = normals;
            mesh.subMeshCount = 1;
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateBounds();

            //meshCollider.convex = true;
            meshCollider.sharedMesh = null;
            meshCollider.sharedMesh = mesh;
        }


#if UNITY_EDITOR
        [CustomEditor(typeof(CollisionMeshCreator))]
        public class CollisionMeshCreatorEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                if (GUILayout.Button("CreateMesh"))
                {
                    CollisionMeshCreator myScript = (CollisionMeshCreator)target;
                    myScript.CreateMeshAndCollider();
                }
            }
        }
#endif

    }
}
