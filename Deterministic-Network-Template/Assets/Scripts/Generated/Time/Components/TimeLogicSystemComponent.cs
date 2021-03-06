//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TimeContext {

    public TimeEntity logicSystemEntity { get { return GetGroup(TimeMatcher.LogicSystem).GetSingleEntity(); } }
    public LogicSystemComponent logicSystem { get { return logicSystemEntity.logicSystem; } }
    public bool hasLogicSystem { get { return logicSystemEntity != null; } }

    public TimeEntity SetLogicSystem(Entitas.Systems newSystem) {
        if (hasLogicSystem) {
            throw new Entitas.EntitasException("Could not set LogicSystem!\n" + this + " already has an entity with LogicSystemComponent!",
                "You should check if the context already has a logicSystemEntity before setting it or use context.ReplaceLogicSystem().");
        }
        var entity = CreateEntity();
        entity.AddLogicSystem(newSystem);
        return entity;
    }

    public void ReplaceLogicSystem(Entitas.Systems newSystem) {
        var entity = logicSystemEntity;
        if (entity == null) {
            entity = SetLogicSystem(newSystem);
        } else {
            entity.ReplaceLogicSystem(newSystem);
        }
    }

    public void RemoveLogicSystem() {
        logicSystemEntity.Destroy();
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

    public LogicSystemComponent logicSystem { get { return (LogicSystemComponent)GetComponent(TimeComponentsLookup.LogicSystem); } }
    public bool hasLogicSystem { get { return HasComponent(TimeComponentsLookup.LogicSystem); } }

    public void AddLogicSystem(Entitas.Systems newSystem) {
        var index = TimeComponentsLookup.LogicSystem;
        var component = CreateComponent<LogicSystemComponent>(index);
        component.system = newSystem;
        AddComponent(index, component);
    }

    public void ReplaceLogicSystem(Entitas.Systems newSystem) {
        var index = TimeComponentsLookup.LogicSystem;
        var component = CreateComponent<LogicSystemComponent>(index);
        component.system = newSystem;
        ReplaceComponent(index, component);
    }

    public void RemoveLogicSystem() {
        RemoveComponent(TimeComponentsLookup.LogicSystem);
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

    static Entitas.IMatcher<TimeEntity> _matcherLogicSystem;

    public static Entitas.IMatcher<TimeEntity> LogicSystem {
        get {
            if (_matcherLogicSystem == null) {
                var matcher = (Entitas.Matcher<TimeEntity>)Entitas.Matcher<TimeEntity>.AllOf(TimeComponentsLookup.LogicSystem);
                matcher.componentNames = TimeComponentsLookup.componentNames;
                _matcherLogicSystem = matcher;
            }

            return _matcherLogicSystem;
        }
    }
}
