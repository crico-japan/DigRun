using Crico.AI.States;
using UnityEngine;

namespace Crico.AI
{
    public class StateTransitionOnGameOver : StateTransition
    {
        [SerializeField] bool stageWin = false;

        public override bool CheckTransitionCondition(Agent agent)
        {
            StageHolder stageHolder = agent.GetComponent<StageHolder>();

            Stage stage = stageHolder.stage;

            bool result = stage.IsGameOver() && stage.IsStageWon() == stageWin;

            return result;
        }
    }

}
