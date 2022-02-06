Console.WriteLine("Reaper Share -- do som art here");

var config = ConfigFactory.Build();
var lastState = new State(true, false, "");
var screen = new LoadingScreen(config);
var input = "";

while(true){
    var states = screen.GetNextState(lastState);
    foreach (var state in states)
    {
        ProcessConfig();

        PrintOptions(state);

        DoActions();

        lastState = state;

        await Task.Delay(200);
    }

    break;
}

void PrintOptions(State state)
{
    if (state.clear)
    {
        Console.Clear();
    }

    screen.DrawMessage();

    input = screen.GetInput(input, state);
}

void DoActions()
{

}

void ProcessConfig(){
    
}

void Print(string msg)
{
    Console.WriteLine(msg);
}

public record State(bool clear, bool WaitForInput, string lastInput);