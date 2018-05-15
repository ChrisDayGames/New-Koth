//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public StunTimerComponent stunTimer { get { return (StunTimerComponent)GetComponent(LogicComponentsLookup.StunTimer); } }
    public bool hasStunTimer { get { return HasComponent(LogicComponentsLookup.StunTimer); } }

    public void AddStunTimer(long newTimeLeft) {
        var index = LogicComponentsLookup.StunTimer;
        var component = CreateComponent<StunTimerComponent>(index);
        component.timeLeft = newTimeLeft;
        AddComponent(index, component);
    }

    public void ReplaceStunTimer(long newTimeLeft) {
        var index = LogicComponentsLookup.StunTimer;
        var component = CreateComponent<StunTimerComponent>(index);
        component.timeLeft = newTimeLeft;
        ReplaceComponent(index, component);
    }

    public void RemoveStunTimer() {
        RemoveComponent(LogicComponentsLookup.StunTimer);
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

    static Entitas.IMatcher<LogicEntity> _matcherStunTimer;

    public static Entitas.IMatcher<LogicEntity> StunTimer {
        get {
            if (_matcherStunTimer == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.StunTimer);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherStunTimer = matcher;
            }

            return _matcherStunTimer;
        }
    }
}