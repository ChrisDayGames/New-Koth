//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public DirectionComponent direction { get { return (DirectionComponent)GetComponent(LogicComponentsLookup.Direction); } }
    public bool hasDirection { get { return HasComponent(LogicComponentsLookup.Direction); } }

    public void AddDirection(int newValue) {
        var index = LogicComponentsLookup.Direction;
        var component = CreateComponent<DirectionComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceDirection(int newValue) {
        var index = LogicComponentsLookup.Direction;
        var component = CreateComponent<DirectionComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveDirection() {
        RemoveComponent(LogicComponentsLookup.Direction);
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

    static Entitas.IMatcher<LogicEntity> _matcherDirection;

    public static Entitas.IMatcher<LogicEntity> Direction {
        get {
            if (_matcherDirection == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.Direction);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherDirection = matcher;
            }

            return _matcherDirection;
        }
    }
}