//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public ThrowTimerComponent throwTimer { get { return (ThrowTimerComponent)GetComponent(LogicComponentsLookup.ThrowTimer); } }
    public bool hasThrowTimer { get { return HasComponent(LogicComponentsLookup.ThrowTimer); } }

    public void AddThrowTimer(long newTimeLeft) {
        var index = LogicComponentsLookup.ThrowTimer;
        var component = CreateComponent<ThrowTimerComponent>(index);
        component.timeLeft = newTimeLeft;
        AddComponent(index, component);
    }

    public void ReplaceThrowTimer(long newTimeLeft) {
        var index = LogicComponentsLookup.ThrowTimer;
        var component = CreateComponent<ThrowTimerComponent>(index);
        component.timeLeft = newTimeLeft;
        ReplaceComponent(index, component);
    }

    public void RemoveThrowTimer() {
        RemoveComponent(LogicComponentsLookup.ThrowTimer);
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

    static Entitas.IMatcher<LogicEntity> _matcherThrowTimer;

    public static Entitas.IMatcher<LogicEntity> ThrowTimer {
        get {
            if (_matcherThrowTimer == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.ThrowTimer);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherThrowTimer = matcher;
            }

            return _matcherThrowTimer;
        }
    }
}