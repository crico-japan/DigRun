using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.AI.Behaviours
{
    public class SetMeshRendererMaterial : AgentBehaviour
    {
        [SerializeField] new Renderer renderer;
        [SerializeField] Material matToSet;
        [SerializeField] int indexOfMaterial = 0;

        private void Awake()
        {
            Assert.IsNotNull(renderer);
            Assert.IsNotNull(matToSet);
        }

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            Material[] materials = renderer.materials;
            materials[indexOfMaterial] = matToSet;
            renderer.materials = materials;
        }
    }

}
