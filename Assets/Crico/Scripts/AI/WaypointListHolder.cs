using UnityEngine;

namespace Crico.AI
{
    public class WaypointListHolder : MonoBehaviour
    {
        [SerializeField] WaypointList waypointList;

        public int GetNumWaypoints()
        {
            return waypointList.numWaypoints;
        }

        public Vector3 GetWaypointPos(int index)
        {
            return waypointList.GetWaypointPos(index);
        }

        public void SetWaypointList(WaypointList waypointList)
        {
            this.waypointList = waypointList;
        }

    }

}
