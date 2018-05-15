//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    static readonly RespawnComponent respawnComponent = new RespawnComponent();

    public bool isRespawn {
        get { return HasComponent(LogicComponentsLookup.Respawn); }
        set {
            if (value != isRespawn) {
                var index = LogicComponentsLookup.Respawn;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : respawnComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<LogicEntity> _matcherRespawn;

    public static Entitas.IMatcher<LogicEntity> Respawn {
        get {
            if (_matcherRespawn == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.Respawn);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherRespawn = matcher;
            }

            return _matcherRespawn;
        }
    }
}
