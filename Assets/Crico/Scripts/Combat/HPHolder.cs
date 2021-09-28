using UnityEngine;
using UnityEngine.Events;

namespace Crico.Combat
{
    public class HPHolder : MonoBehaviour
    {
        [System.Serializable]
        public class HPHolderTakeDamageEvent : UnityEvent<int> { }

        [SerializeField] private int _maxHp = 0;
        [SerializeField] private int _hp = 0;
        [SerializeField] private HPHolderTakeDamageEvent onTakeDamage = null;

        public int maxHp { get => _maxHp; }
        public int hp { get => _hp; }

        public bool IsDead { get => hp <= 0; }

        public void SetHp(int amount)
        {
            _hp = Mathf.Clamp(amount, 0, _maxHp);
        }

        public int TakeDamage(int amount)
        {
            int damageTaken = amount;

            int newHp = _hp - amount;
            if (newHp < 0)
            {
                damageTaken += newHp;
            }

            SetHp(newHp);
            onTakeDamage.Invoke(_hp);

            return damageTaken;
        }

        public void AddListener(UnityAction<int> action)
        {
            onTakeDamage.AddListener(action);
        }
    }

}
