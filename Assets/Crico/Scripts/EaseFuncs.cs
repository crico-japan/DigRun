using UnityEngine;

namespace Crico
{
    public class EaseFuncs
    {
        static public float OutCubic(float input)
        {
            return 1f - Mathf.Pow(1f - input, 3f);
        }
    }

}
