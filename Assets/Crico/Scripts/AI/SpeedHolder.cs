using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.AI
{
    public class SpeedHolder : MonoBehaviour
    {
        [SerializeField] float _speed;
        [SerializeField] float _rotationSpeed;

        public float speed { get => _speed; }
        public float rotationSpeed { get => _rotationSpeed; }

    }

}
