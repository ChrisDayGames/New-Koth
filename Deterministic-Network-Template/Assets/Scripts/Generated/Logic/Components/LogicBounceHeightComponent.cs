//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public BounceHeightComponent bounceHeight { get { return (BounceHeightComponent)GetComponent(LogicComponentsLookup.BounceHeight); } }
    public bool hasBounceHeight { get { return HasComponent(LogicComponentsLookup.BounceHeight); } }

    public void AddBounceHeight(long newValue) {
        var index = LogicComponentsLookup.BounceHeight;
        var component = CreateComponent<BounceHeightComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceBounceHeight(long newValue) {
        var index = LogicComponentsLookup.BounceHeight;
        var component = CreateComponent<BounceHeightComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveBounceHeight() {
        RemoveComponent(LogicComponentsLookup.BounceHeight);
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

    static Entitas.IMatcher<LogicEntity> _matcherBounceHeight;

    public static Entitas.IMatcher<LogicEntity> BounceHeight {
        get {
            if (_matcherBounceHeight == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.BounceHeight);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherBounceHeight = matcher;
            }

            return _matcherBounceHeight;
        }
    }
}
