public struct ClientFormationSetupReady
{ }

public struct ClientFormationSetupUnready
{ }

public struct FormationReceived
{
    public Side Side;
    public string Formation;
}

public struct TurnStateChange
{
    public Side Side; // Who's turn it is now
}

public struct StartGame
{ }

public struct EndGame
{
    public Side winner;
}

public struct NetworkServerInitialized
{ }

public struct NetworkClientInitialized
{ }