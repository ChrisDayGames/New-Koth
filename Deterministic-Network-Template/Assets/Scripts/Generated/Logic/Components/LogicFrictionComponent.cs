//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public FrictionComponent friction { get { return (FrictionComponent)GetComponent(LogicComponentsLookup.Friction); } }
    public bool hasFriction { get { return HasComponent(LogicComponentsLookup.Friction); } }

    public void AddFriction(long newFriction, long newDangerousFriction) {
        var index = LogicComponentsLookup.Friction;
        var component = CreateComponent<FrictionComponent>(index);
        component.friction = newFriction;
        component.dangerousFriction = newDangerousFriction;
        AddComponent(index, component);
    }

    public void ReplaceFriction(long newFriction, long newDangerousFriction) {
        var index = LogicComponentsLookup.Friction;
        var component = CreateComponent<FrictionComponent>(index);
        component.friction = newFriction;
        component.dangerousFriction = newDangerousFriction;
        ReplaceComponent(index, component);
    }

    public void RemoveFriction() {
        RemoveComponent(LogicComponentsLookup.Friction);
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

    static Entitas.IMatcher<LogicEntity> _matcherFriction;

    public static Entitas.IMatcher<LogicEntity> Friction {
        get {
            if (_matcherFriction == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.Friction);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherFriction = matcher;
            }

            return _matcherFriction;
        }
    }
}
