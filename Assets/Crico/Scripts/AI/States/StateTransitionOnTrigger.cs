namespace Crico.AI.States
{
    public class StateTransitionOnTrigger : StateTransition
    {
        public override bool CheckTransitionCondition(Agent agent)
        {
            Trigger trigger = agent.GetComponent<Trigger>();
            
            bool makeTransition = trigger.triggerValue;
            trigger.ConsumeTrigger();

            return makeTransition;
        }
    }

}
