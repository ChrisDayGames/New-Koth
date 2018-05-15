//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public DashTimerComponent dashTimer { get { return (DashTimerComponent)GetComponent(LogicComponentsLookup.DashTimer); } }
    public bool hasDashTimer { get { return HasComponent(LogicComponentsLookup.DashTimer); } }

    public void AddDashTimer(long newTimeLeft) {
        var index = LogicComponentsLookup.DashTimer;
        var component = CreateComponent<DashTimerComponent>(index);
        component.timeLeft = newTimeLeft;
        AddComponent(index, component);
    }

    public void ReplaceDashTimer(long newTimeLeft) {
        var index = LogicComponentsLookup.DashTimer;
        var component = CreateComponent<DashTimerComponent>(index);
        component.timeLeft = newTimeLeft;
        ReplaceComponent(index, component);
    }

    public void RemoveDashTimer() {
        RemoveComponent(LogicComponentsLookup.DashTimer);
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

    static Entitas.IMatcher<LogicEntity> _matcherDashTimer;

    public static Entitas.IMatcher<LogicEntity> DashTimer {
        get {
            if (_matcherDashTimer == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.DashTimer);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherDashTimer = matcher;
            }

            return _matcherDashTimer;
        }
    }
}