//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public WallJumpTimerComponent wallJumpTimer { get { return (WallJumpTimerComponent)GetComponent(LogicComponentsLookup.WallJumpTimer); } }
    public bool hasWallJumpTimer { get { return HasComponent(LogicComponentsLookup.WallJumpTimer); } }

    public void AddWallJumpTimer(long newTimeLeft) {
        var index = LogicComponentsLookup.WallJumpTimer;
        var component = CreateComponent<WallJumpTimerComponent>(index);
        component.timeLeft = newTimeLeft;
        AddComponent(index, component);
    }

    public void ReplaceWallJumpTimer(long newTimeLeft) {
        var index = LogicComponentsLookup.WallJumpTimer;
        var component = CreateComponent<WallJumpTimerComponent>(index);
        component.timeLeft = newTimeLeft;
        ReplaceComponent(index, component);
    }

    public void RemoveWallJumpTimer() {
        RemoveComponent(LogicComponentsLookup.WallJumpTimer);
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

    static Entitas.IMatcher<LogicEntity> _matcherWallJumpTimer;

    public static Entitas.IMatcher<LogicEntity> WallJumpTimer {
        get {
            if (_matcherWallJumpTimer == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.WallJumpTimer);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherWallJumpTimer = matcher;
            }

            return _matcherWallJumpTimer;
        }
    }
}
