using System.Linq;
public class LoadingScreen : IScreen
{
    private readonly ConfigManager _configManager;
    private string message = "loading";

    public LoadingScreen(ConfigManager configManager)
    {
        _configManager = configManager;
    }
    public string DrawMessage()
    {
        this.Draw(message);

        return message;
    }

    public string GetInput(string lastInput, State state)
    {
        return "";
    }

    public IEnumerable<State> GetNextState(State lastState)
    {
        // kick off validating config
        var validate = Task.Run(() => _configManager.ValidateAndUpdateEnvironment());

        for (var i = 0; true; i = (i + 1) % 3)
        {
            if (!validate.IsFaulted && !_configManager.EnvironmentUpdated)
            {

                message = "loading" + String.Concat(Enumerable.Range(0, i).Select(_ => "."));
                yield return lastState;
            }
            else
            {
                yield break;
            }
        }
    }

    public State ProcessInput(string input, State current)
    {
        throw new NotImplementedException();
    }
}

