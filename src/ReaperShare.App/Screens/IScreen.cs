
public interface IScreen
{
    string DrawMessage();

    string GetInput(string lastInput, State state);

    IEnumerable<State> GetNextState(State lastState);

    State ProcessInput(string input, State current);
}

