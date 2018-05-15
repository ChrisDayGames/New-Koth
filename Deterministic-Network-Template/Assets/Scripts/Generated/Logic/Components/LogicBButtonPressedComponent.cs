//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public BButtonPressedComponent bButtonPressed { get { return (BButtonPressedComponent)GetComponent(LogicComponentsLookup.BButtonPressed); } }
    public bool hasBButtonPressed { get { return HasComponent(LogicComponentsLookup.BButtonPressed); } }

    public void AddBButtonPressed(System.Type newCommand) {
        var index = LogicComponentsLookup.BButtonPressed;
        var component = CreateComponent<BButtonPressedComponent>(index);
        component.command = newCommand;
        AddComponent(index, component);
    }

    public void ReplaceBButtonPressed(System.Type newCommand) {
        var index = LogicComponentsLookup.BButtonPressed;
        var component = CreateComponent<BButtonPressedComponent>(index);
        component.command = newCommand;
        ReplaceComponent(index, component);
    }

    public void RemoveBButtonPressed() {
        RemoveComponent(LogicComponentsLookup.BButtonPressed);
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

    static Entitas.IMatcher<LogicEntity> _matcherBButtonPressed;

    public static Entitas.IMatcher<LogicEntity> BButtonPressed {
        get {
            if (_matcherBButtonPressed == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.BButtonPressed);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherBButtonPressed = matcher;
            }

            return _matcherBButtonPressed;
        }
    }
}
