//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public MinJumpVelocity minJumpVelocity { get { return (MinJumpVelocity)GetComponent(LogicComponentsLookup.MinJumpVelocity); } }
    public bool hasMinJumpVelocity { get { return HasComponent(LogicComponentsLookup.MinJumpVelocity); } }

    public void AddMinJumpVelocity(long newValue) {
        var index = LogicComponentsLookup.MinJumpVelocity;
        var component = CreateComponent<MinJumpVelocity>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceMinJumpVelocity(long newValue) {
        var index = LogicComponentsLookup.MinJumpVelocity;
        var component = CreateComponent<MinJumpVelocity>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveMinJumpVelocity() {
        RemoveComponent(LogicComponentsLookup.MinJumpVelocity);
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

    static Entitas.IMatcher<LogicEntity> _matcherMinJumpVelocity;

    public static Entitas.IMatcher<LogicEntity> MinJumpVelocity {
        get {
            if (_matcherMinJumpVelocity == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.MinJumpVelocity);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherMinJumpVelocity = matcher;
            }

            return _matcherMinJumpVelocity;
        }
    }
}