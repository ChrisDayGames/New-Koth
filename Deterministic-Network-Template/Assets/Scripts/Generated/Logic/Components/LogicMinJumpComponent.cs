//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public MinJump minJump { get { return (MinJump)GetComponent(LogicComponentsLookup.MinJump); } }
    public bool hasMinJump { get { return HasComponent(LogicComponentsLookup.MinJump); } }

    public void AddMinJump(long newValue) {
        var index = LogicComponentsLookup.MinJump;
        var component = CreateComponent<MinJump>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceMinJump(long newValue) {
        var index = LogicComponentsLookup.MinJump;
        var component = CreateComponent<MinJump>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveMinJump() {
        RemoveComponent(LogicComponentsLookup.MinJump);
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

    static Entitas.IMatcher<LogicEntity> _matcherMinJump;

    public static Entitas.IMatcher<LogicEntity> MinJump {
        get {
            if (_matcherMinJump == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.MinJump);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherMinJump = matcher;
            }

            return _matcherMinJump;
        }
    }
}
