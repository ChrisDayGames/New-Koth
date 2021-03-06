//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public ThrowMovementComponent throwMovement { get { return (ThrowMovementComponent)GetComponent(LogicComponentsLookup.ThrowMovement); } }
    public bool hasThrowMovement { get { return HasComponent(LogicComponentsLookup.ThrowMovement); } }

    public void AddThrowMovement(long newPower, long newThrowPositionY) {
        var index = LogicComponentsLookup.ThrowMovement;
        var component = CreateComponent<ThrowMovementComponent>(index);
        component.power = newPower;
        component.throwPositionY = newThrowPositionY;
        AddComponent(index, component);
    }

    public void ReplaceThrowMovement(long newPower, long newThrowPositionY) {
        var index = LogicComponentsLookup.ThrowMovement;
        var component = CreateComponent<ThrowMovementComponent>(index);
        component.power = newPower;
        component.throwPositionY = newThrowPositionY;
        ReplaceComponent(index, component);
    }

    public void RemoveThrowMovement() {
        RemoveComponent(LogicComponentsLookup.ThrowMovement);
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

    static Entitas.IMatcher<LogicEntity> _matcherThrowMovement;

    public static Entitas.IMatcher<LogicEntity> ThrowMovement {
        get {
            if (_matcherThrowMovement == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.ThrowMovement);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherThrowMovement = matcher;
            }

            return _matcherThrowMovement;
        }
    }
}
