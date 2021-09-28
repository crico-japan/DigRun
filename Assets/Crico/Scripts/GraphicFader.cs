using UnityEngine;
using UnityEngine.UI;

namespace Crico
{
    [RequireComponent(typeof(Graphic))]
    public class GraphicFader : MonoBehaviour
    {
        [SerializeField] float fadeTime = 1.6f;
        bool running;
        bool fadeOut;
        float time;

        public void DoFadeIn()
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
            time = 0f;
            running = true;
            fadeOut = false;
            SetAlpha(0f);
        }

        public void DoFadeOut()
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
            time = 0f;
            running = true;
            fadeOut = true;
            SetAlpha(1f);
        }

        private void Update()
        {
            if (!running)
                return;

            time += Time.deltaTime;
            if (time >= fadeTime)
            {
                time = fadeTime;
                running = false;
                if (fadeOut)
                    gameObject.SetActive(false);
            }

            float t = time / fadeTime;
            float adjustedT = EaseFuncs.OutCubic(t);

            if (fadeOut)
                adjustedT = 1f - adjustedT;

            SetAlpha(adjustedT);
        }

        private void SetAlpha(float alpha)
        {
            Graphic graphic = GetComponent<Graphic>();
            Color color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }
    }
}
