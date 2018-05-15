//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public BButtonDownComponent bButtonDown { get { return (BButtonDownComponent)GetComponent(LogicComponentsLookup.BButtonDown); } }
    public bool hasBButtonDown { get { return HasComponent(LogicComponentsLookup.BButtonDown); } }

    public void AddBButtonDown(System.Type newCommand) {
        var index = LogicComponentsLookup.BButtonDown;
        var component = CreateComponent<BButtonDownComponent>(index);
        component.command = newCommand;
        AddComponent(index, component);
    }

    public void ReplaceBButtonDown(System.Type newCommand) {
        var index = LogicComponentsLookup.BButtonDown;
        var component = CreateComponent<BButtonDownComponent>(index);
        component.command = newCommand;
        ReplaceComponent(index, component);
    }

    public void RemoveBButtonDown() {
        RemoveComponent(LogicComponentsLookup.BButtonDown);
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

    static Entitas.IMatcher<LogicEntity> _matcherBButtonDown;

    public static Entitas.IMatcher<LogicEntity> BButtonDown {
        get {
            if (_matcherBButtonDown == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.BButtonDown);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherBButtonDown = matcher;
            }

            return _matcherBButtonDown;
        }
    }
}