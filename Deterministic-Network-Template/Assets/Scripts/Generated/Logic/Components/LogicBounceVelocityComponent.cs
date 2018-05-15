//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public BounceVelocityComponent bounceVelocity { get { return (BounceVelocityComponent)GetComponent(LogicComponentsLookup.BounceVelocity); } }
    public bool hasBounceVelocity { get { return HasComponent(LogicComponentsLookup.BounceVelocity); } }

    public void AddBounceVelocity(long newValue) {
        var index = LogicComponentsLookup.BounceVelocity;
        var component = CreateComponent<BounceVelocityComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceBounceVelocity(long newValue) {
        var index = LogicComponentsLookup.BounceVelocity;
        var component = CreateComponent<BounceVelocityComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveBounceVelocity() {
        RemoveComponent(LogicComponentsLookup.BounceVelocity);
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

    static Entitas.IMatcher<LogicEntity> _matcherBounceVelocity;

    public static Entitas.IMatcher<LogicEntity> BounceVelocity {
        get {
            if (_matcherBounceVelocity == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.BounceVelocity);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherBounceVelocity = matcher;
            }

            return _matcherBounceVelocity;
        }
    }
}
