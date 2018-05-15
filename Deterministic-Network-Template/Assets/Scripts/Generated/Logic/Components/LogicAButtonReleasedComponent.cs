//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public AButtonReleasedComponent aButtonReleased { get { return (AButtonReleasedComponent)GetComponent(LogicComponentsLookup.AButtonReleased); } }
    public bool hasAButtonReleased { get { return HasComponent(LogicComponentsLookup.AButtonReleased); } }

    public void AddAButtonReleased(System.Type newCommand) {
        var index = LogicComponentsLookup.AButtonReleased;
        var component = CreateComponent<AButtonReleasedComponent>(index);
        component.command = newCommand;
        AddComponent(index, component);
    }

    public void ReplaceAButtonReleased(System.Type newCommand) {
        var index = LogicComponentsLookup.AButtonReleased;
        var component = CreateComponent<AButtonReleasedComponent>(index);
        component.command = newCommand;
        ReplaceComponent(index, component);
    }

    public void RemoveAButtonReleased() {
        RemoveComponent(LogicComponentsLookup.AButtonReleased);
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

    static Entitas.IMatcher<LogicEntity> _matcherAButtonReleased;

    public static Entitas.IMatcher<LogicEntity> AButtonReleased {
        get {
            if (_matcherAButtonReleased == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.AButtonReleased);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherAButtonReleased = matcher;
            }

            return _matcherAButtonReleased;
        }
    }
}
