using UnityEngine;

namespace Crico.AI
{
    public class TargetHolder : MonoBehaviour
    {
        [SerializeField] GameObject _target;

        public GameObject target { get => _target; set => _target = value; }

        public void SetTarget(GameObject target)
        {
            this._target = target;
        }

    }
}
