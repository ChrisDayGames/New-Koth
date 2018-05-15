//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public DashMovementComponent dashMovement { get { return (DashMovementComponent)GetComponent(LogicComponentsLookup.DashMovement); } }
    public bool hasDashMovement { get { return HasComponent(LogicComponentsLookup.DashMovement); } }

    public void AddDashMovement(long newTargetSpeed, long newAccelerationTime, long newLength) {
        var index = LogicComponentsLookup.DashMovement;
        var component = CreateComponent<DashMovementComponent>(index);
        component.targetSpeed = newTargetSpeed;
        component.accelerationTime = newAccelerationTime;
        component.length = newLength;
        AddComponent(index, component);
    }

    public void ReplaceDashMovement(long newTargetSpeed, long newAccelerationTime, long newLength) {
        var index = LogicComponentsLookup.DashMovement;
        var component = CreateComponent<DashMovementComponent>(index);
        component.targetSpeed = newTargetSpeed;
        component.accelerationTime = newAccelerationTime;
        component.length = newLength;
        ReplaceComponent(index, component);
    }

    public void RemoveDashMovement() {
        RemoveComponent(LogicComponentsLookup.DashMovement);
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

    static Entitas.IMatcher<LogicEntity> _matcherDashMovement;

    public static Entitas.IMatcher<LogicEntity> DashMovement {
        get {
            if (_matcherDashMovement == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.DashMovement);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherDashMovement = matcher;
            }

            return _matcherDashMovement;
        }
    }
}