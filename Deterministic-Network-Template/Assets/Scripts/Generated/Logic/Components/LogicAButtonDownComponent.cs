//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public AButtonDownComponent aButtonDown { get { return (AButtonDownComponent)GetComponent(LogicComponentsLookup.AButtonDown); } }
    public bool hasAButtonDown { get { return HasComponent(LogicComponentsLookup.AButtonDown); } }

    public void AddAButtonDown(System.Type newCommand) {
        var index = LogicComponentsLookup.AButtonDown;
        var component = CreateComponent<AButtonDownComponent>(index);
        component.command = newCommand;
        AddComponent(index, component);
    }

    public void ReplaceAButtonDown(System.Type newCommand) {
        var index = LogicComponentsLookup.AButtonDown;
        var component = CreateComponent<AButtonDownComponent>(index);
        component.command = newCommand;
        ReplaceComponent(index, component);
    }

    public void RemoveAButtonDown() {
        RemoveComponent(LogicComponentsLookup.AButtonDown);
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

    static Entitas.IMatcher<LogicEntity> _matcherAButtonDown;

    public static Entitas.IMatcher<LogicEntity> AButtonDown {
        get {
            if (_matcherAButtonDown == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.AButtonDown);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherAButtonDown = matcher;
            }

            return _matcherAButtonDown;
        }
    }
}
