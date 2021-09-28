using UnityEngine;

namespace Crico
{
    public class IDHolder : MonoBehaviour
    {
        static private int s_currentId = 0;
        private int id = -1;

        public int GetId()
        {
            if (id < 0)
            {
                id = s_currentId++;
            }

            return id;
        }

    }

}
