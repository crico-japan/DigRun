using System.Collections.Generic;
using UnityEngine;

namespace Crico.AI
{
    public class WaypointList : MonoBehaviour
    {
        [SerializeField] Waypoint[] waypoints;

        private void Reset()
        {
            Transform parent = transform;
            while (parent != null)
            {
                if (parent.parent != null)
                    parent = parent.parent;
                else
                    break;
            }

            List<Waypoint> temp = new List<Waypoint>();

            AddWaypointToList(temp, parent);

            waypoints = temp.ToArray();
        }

        private void AddWaypointToList(List<Waypoint> input, Transform node)
        {
            if (!node.gameObject.activeSelf)
                return;

            if (node.childCount == 0)
            {
                Waypoint waypoint = node.GetComponent<Waypoint>();
                if (waypoint != null)
                    input.Add(waypoint);
            }
            else
            {
                foreach (Transform child in node.transform)
                {
                    AddWaypointToList(input, child);
                }
            }
        }

        public int numWaypoints
        {
            get
            {
                int result = 0;
                if (waypoints != null)
                    result = waypoints.Length;
                return result;
            }
        }

        public Vector3 GetWaypointPos(int index)
        {
            if (waypoints == null || waypoints.Length == 0)
                return Vector3.zero;

            index = Mathf.Clamp(index, 0, waypoints.Length - 1);

            return waypoints[index].transform.position;
        }
    }

}
