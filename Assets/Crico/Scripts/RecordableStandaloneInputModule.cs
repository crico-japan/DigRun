using UnityEngine;
using UnityEngine.EventSystems;

namespace Crico
{
    [RequireComponent(typeof(RecordableInput))]
    public class RecordableStandaloneInputModule : StandaloneInputModule
    {
        protected override void Awake()
        {
            base.Awake();

            m_InputOverride = GetComponent<RecordableInput>();
        }
    }

}
