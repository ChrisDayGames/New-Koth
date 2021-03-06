//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public ScaleComponent scale { get { return (ScaleComponent)GetComponent(LogicComponentsLookup.Scale); } }
    public bool hasScale { get { return HasComponent(LogicComponentsLookup.Scale); } }

    public void AddScale(Determinism.FixedVector2 newValue) {
        var index = LogicComponentsLookup.Scale;
        var component = CreateComponent<ScaleComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceScale(Determinism.FixedVector2 newValue) {
        var index = LogicComponentsLookup.Scale;
        var component = CreateComponent<ScaleComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveScale() {
        RemoveComponent(LogicComponentsLookup.Scale);
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

    static Entitas.IMatcher<LogicEntity> _matcherScale;

    public static Entitas.IMatcher<LogicEntity> Scale {
        get {
            if (_matcherScale == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.Scale);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherScale = matcher;
            }

            return _matcherScale;
        }
    }
}
