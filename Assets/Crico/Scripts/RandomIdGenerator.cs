using UnityEngine;

namespace Crico
{
    public class RandomIdGenerator : MonoBehaviour
    {
        private const int MIN = 0;
        private const int MAX = int.MaxValue;

        public int lastId { get; private set; } = -1;

        public int GenerateId()
        {
            int id = Random.Range(MIN, MAX);
            lastId = id;
            return id;
        }



    }

}
