//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public HatComponent hat { get { return (HatComponent)GetComponent(LogicComponentsLookup.Hat); } }
    public bool hasHat { get { return HasComponent(LogicComponentsLookup.Hat); } }

    public void AddHat(int newEntityID) {
        var index = LogicComponentsLookup.Hat;
        var component = CreateComponent<HatComponent>(index);
        component.entityID = newEntityID;
        AddComponent(index, component);
    }

    public void ReplaceHat(int newEntityID) {
        var index = LogicComponentsLookup.Hat;
        var component = CreateComponent<HatComponent>(index);
        component.entityID = newEntityID;
        ReplaceComponent(index, component);
    }

    public void RemoveHat() {
        RemoveComponent(LogicComponentsLookup.Hat);
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

    static Entitas.IMatcher<LogicEntity> _matcherHat;

    public static Entitas.IMatcher<LogicEntity> Hat {
        get {
            if (_matcherHat == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.Hat);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherHat = matcher;
            }

            return _matcherHat;
        }
    }
}
