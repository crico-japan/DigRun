using Crico.AI;
using Crico.AI.Behaviours;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeoutBehaviour : AgentBehaviour
{
    [SerializeField] SkinnedMeshRenderer meshRenderer = null;
    [SerializeField] float fadeSpeed = 1.0f;

    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        Material materials = meshRenderer.material;
    }

    public void FadeOut()
    {
        meshRenderer.material.DOFade(0.0f, 0.5f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
