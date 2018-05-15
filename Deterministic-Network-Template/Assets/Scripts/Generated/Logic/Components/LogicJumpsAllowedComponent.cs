//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public JumpsAllowedComponent jumpsAllowed { get { return (JumpsAllowedComponent)GetComponent(LogicComponentsLookup.JumpsAllowed); } }
    public bool hasJumpsAllowed { get { return HasComponent(LogicComponentsLookup.JumpsAllowed); } }

    public void AddJumpsAllowed(long newValue) {
        var index = LogicComponentsLookup.JumpsAllowed;
        var component = CreateComponent<JumpsAllowedComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceJumpsAllowed(long newValue) {
        var index = LogicComponentsLookup.JumpsAllowed;
        var component = CreateComponent<JumpsAllowedComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveJumpsAllowed() {
        RemoveComponent(LogicComponentsLookup.JumpsAllowed);
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

    static Entitas.IMatcher<LogicEntity> _matcherJumpsAllowed;

    public static Entitas.IMatcher<LogicEntity> JumpsAllowed {
        get {
            if (_matcherJumpsAllowed == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.JumpsAllowed);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherJumpsAllowed = matcher;
            }

            return _matcherJumpsAllowed;
        }
    }
}
