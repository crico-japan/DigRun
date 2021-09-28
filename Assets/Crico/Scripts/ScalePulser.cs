using DG.Tweening;
using UnityEngine;

namespace Crico
{
    public class ScalePulser : MonoBehaviour
    {
        [SerializeField] private float pulseScale = 1.2f;
        [SerializeField] private float period = 1.2f;

        Sequence seq;
        private void OnDisable()
        {
            if (seq != null)
            {
                seq.Kill();
                seq = null;
            }
        }

        private void OnEnable()
        {
            seq = DOTween.Sequence();

            float scale = transform.localScale.x;
            float destScale = scale * pulseScale;

            seq.Append(transform.DOScale(destScale, period / 2f).SetEase(Ease.OutCubic));
            seq.Append(transform.DOScale(scale, period / 2f).SetEase(Ease.InCubic));

            seq.SetLoops(-1);
            seq.Play();

        }

    }

}
