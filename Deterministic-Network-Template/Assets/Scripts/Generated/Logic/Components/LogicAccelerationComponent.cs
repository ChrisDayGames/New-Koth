//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public AccelerationComponent acceleration { get { return (AccelerationComponent)GetComponent(LogicComponentsLookup.Acceleration); } }
    public bool hasAcceleration { get { return HasComponent(LogicComponentsLookup.Acceleration); } }

    public void AddAcceleration(Determinism.FixedVector2 newValue) {
        var index = LogicComponentsLookup.Acceleration;
        var component = CreateComponent<AccelerationComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAcceleration(Determinism.FixedVector2 newValue) {
        var index = LogicComponentsLookup.Acceleration;
        var component = CreateComponent<AccelerationComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAcceleration() {
        RemoveComponent(LogicComponentsLookup.Acceleration);
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

    static Entitas.IMatcher<LogicEntity> _matcherAcceleration;

    public static Entitas.IMatcher<LogicEntity> Acceleration {
        get {
            if (_matcherAcceleration == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.Acceleration);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherAcceleration = matcher;
            }

            return _matcherAcceleration;
        }
    }
}
