using Crico.AI.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnimationEvent : MonoBehaviour
{
    [SerializeField] Collider meleeCollider = null;
    [SerializeField] AnimalAttackBehaviour animalAttackBehaviour = null;
    public void ActivateMeleeCollider()
    {
        meleeCollider.gameObject.SetActive(true);
    }

    public void DeactivateMeleeCollider()
    {
        meleeCollider.gameObject.SetActive(false);
        animalAttackBehaviour.SetSuccess();
    }
}
