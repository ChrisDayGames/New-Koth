//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TimeContext {

    public TimeEntity jumpInTimeEntity { get { return GetGroup(TimeMatcher.JumpInTime).GetSingleEntity(); } }
    public JumpInTimeComponent jumpInTime { get { return jumpInTimeEntity.jumpInTime; } }
    public bool hasJumpInTime { get { return jumpInTimeEntity != null; } }

    public TimeEntity SetJumpInTime(int newTargetTick) {
        if (hasJumpInTime) {
            throw new Entitas.EntitasException("Could not set JumpInTime!\n" + this + " already has an entity with JumpInTimeComponent!",
                "You should check if the context already has a jumpInTimeEntity before setting it or use context.ReplaceJumpInTime().");
        }
        var entity = CreateEntity();
        entity.AddJumpInTime(newTargetTick);
        return entity;
    }

    public void ReplaceJumpInTime(int newTargetTick) {
        var entity = jumpInTimeEntity;
        if (entity == null) {
            entity = SetJumpInTime(newTargetTick);
        } else {
            entity.ReplaceJumpInTime(newTargetTick);
        }
    }

    public void RemoveJumpInTime() {
        jumpInTimeEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TimeEntity {

    public JumpInTimeComponent jumpInTime { get { return (JumpInTimeComponent)GetComponent(TimeComponentsLookup.JumpInTime); } }
    public bool hasJumpInTime { get { return HasComponent(TimeComponentsLookup.JumpInTime); } }

    public void AddJumpInTime(int newTargetTick) {
        var index = TimeComponentsLookup.JumpInTime;
        var component = CreateComponent<JumpInTimeComponent>(index);
        component.targetTick = newTargetTick;
        AddComponent(index, component);
    }

    public void ReplaceJumpInTime(int newTargetTick) {
        var index = TimeComponentsLookup.JumpInTime;
        var component = CreateComponent<JumpInTimeComponent>(index);
        component.targetTick = newTargetTick;
        ReplaceComponent(index, component);
    }

    public void RemoveJumpInTime() {
        RemoveComponent(TimeComponentsLookup.JumpInTime);
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
public sealed partial class TimeMatcher {

    static Entitas.IMatcher<TimeEntity> _matcherJumpInTime;

    public static Entitas.IMatcher<TimeEntity> JumpInTime {
        get {
            if (_matcherJumpInTime == null) {
                var matcher = (Entitas.Matcher<TimeEntity>)Entitas.Matcher<TimeEntity>.AllOf(TimeComponentsLookup.JumpInTime);
                matcher.componentNames = TimeComponentsLookup.componentNames;
                _matcherJumpInTime = matcher;
            }

            return _matcherJumpInTime;
        }
    }
}
