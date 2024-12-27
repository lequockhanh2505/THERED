public class StateMachine
{
    public IState currentState;

    public void ChangeState(IState newState)
    {
        if (newState == null)
        {
            return;
        }
        if (currentState != null && currentState != newState)
        {
            currentState.OnExit();
        }

        currentState = newState;
        currentState.OnEnter();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }

    public void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.OnFixedUpdate();
        }
    }
}
