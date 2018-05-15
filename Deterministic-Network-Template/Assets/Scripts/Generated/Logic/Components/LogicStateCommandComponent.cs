//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public StateCommandComponent stateCommand { get { return (StateCommandComponent)GetComponent(LogicComponentsLookup.StateCommand); } }
    public bool hasStateCommand { get { return HasComponent(LogicComponentsLookup.StateCommand); } }

    public void AddStateCommand(CommandInput.Command newCommand) {
        var index = LogicComponentsLookup.StateCommand;
        var component = CreateComponent<StateCommandComponent>(index);
        component.command = newCommand;
        AddComponent(index, component);
    }

    public void ReplaceStateCommand(CommandInput.Command newCommand) {
        var index = LogicComponentsLookup.StateCommand;
        var component = CreateComponent<StateCommandComponent>(index);
        component.command = newCommand;
        ReplaceComponent(index, component);
    }

    public void RemoveStateCommand() {
        RemoveComponent(LogicComponentsLookup.StateCommand);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class LogicMatcher {

    static Entitas.IMatcher<LogicEntity> _matcherStateCommand;

    public static Entitas.IMatcher<LogicEntity> StateCommand {
        get {
            if (_matcherStateCommand == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.StateCommand);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherStateCommand = matcher;
            }

            return _matcherStateCommand;
        }
    }
}
