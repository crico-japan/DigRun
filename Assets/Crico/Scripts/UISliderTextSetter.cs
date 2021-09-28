using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Crico
{
    [RequireComponent(typeof(Slider))]
    public class UISliderTextSetter : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textToSet = null;
        [SerializeField] string format = "{0}";

        private void Awake()
        {
            Assert.IsNotNull(textToSet);

            Slider slider = GetComponent<Slider>();
            Assert.IsNotNull(slider);

            slider.onValueChanged.AddListener((a) => RefreshText()) ;
        }

        private void RefreshText()
        {
            Slider slider = GetComponent<Slider>();
            textToSet.SetText(string.Format(format, slider.value));
        }

        private void Start()
        {
            RefreshText();
        }
    }

}
