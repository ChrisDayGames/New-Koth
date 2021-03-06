//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicContext {

    public LogicEntity spawnPointsEntity { get { return GetGroup(LogicMatcher.SpawnPoints).GetSingleEntity(); } }
    public SpawnPointsComponent spawnPoints { get { return spawnPointsEntity.spawnPoints; } }
    public bool hasSpawnPoints { get { return spawnPointsEntity != null; } }

    public LogicEntity SetSpawnPoints(Determinism.FixedVector2[] newList) {
        if (hasSpawnPoints) {
            throw new Entitas.EntitasException("Could not set SpawnPoints!\n" + this + " already has an entity with SpawnPointsComponent!",
                "You should check if the context already has a spawnPointsEntity before setting it or use context.ReplaceSpawnPoints().");
        }
        var entity = CreateEntity();
        entity.AddSpawnPoints(newList);
        return entity;
    }

    public void ReplaceSpawnPoints(Determinism.FixedVector2[] newList) {
        var entity = spawnPointsEntity;
        if (entity == null) {
            entity = SetSpawnPoints(newList);
        } else {
            entity.ReplaceSpawnPoints(newList);
        }
    }

    public void RemoveSpawnPoints() {
        spawnPointsEntity.Destroy();
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
public partial class LogicEntity {

    public SpawnPointsComponent spawnPoints { get { return (SpawnPointsComponent)GetComponent(LogicComponentsLookup.SpawnPoints); } }
    public bool hasSpawnPoints { get { return HasComponent(LogicComponentsLookup.SpawnPoints); } }

    public void AddSpawnPoints(Determinism.FixedVector2[] newList) {
        var index = LogicComponentsLookup.SpawnPoints;
        var component = CreateComponent<SpawnPointsComponent>(index);
        component.list = newList;
        AddComponent(index, component);
    }

    public void ReplaceSpawnPoints(Determinism.FixedVector2[] newList) {
        var index = LogicComponentsLookup.SpawnPoints;
        var component = CreateComponent<SpawnPointsComponent>(index);
        component.list = newList;
        ReplaceComponent(index, component);
    }

    public void RemoveSpawnPoints() {
        RemoveComponent(LogicComponentsLookup.SpawnPoints);
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

    static Entitas.IMatcher<LogicEntity> _matcherSpawnPoints;

    public static Entitas.IMatcher<LogicEntity> SpawnPoints {
        get {
            if (_matcherSpawnPoints == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.SpawnPoints);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherSpawnPoints = matcher;
            }

            return _matcherSpawnPoints;
        }
    }
}
