//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public FreezeComponent freeze { get { return (FreezeComponent)GetComponent(LogicComponentsLookup.Freeze); } }
    public bool hasFreeze { get { return HasComponent(LogicComponentsLookup.Freeze); } }

    public void AddFreeze(int newFrames) {
        var index = LogicComponentsLookup.Freeze;
        var component = CreateComponent<FreezeComponent>(index);
        component.frames = newFrames;
        AddComponent(index, component);
    }

    public void ReplaceFreeze(int newFrames) {
        var index = LogicComponentsLookup.Freeze;
        var component = CreateComponent<FreezeComponent>(index);
        component.frames = newFrames;
        ReplaceComponent(index, component);
    }

    public void RemoveFreeze() {
        RemoveComponent(LogicComponentsLookup.Freeze);
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

    static Entitas.IMatcher<LogicEntity> _matcherFreeze;

    public static Entitas.IMatcher<LogicEntity> Freeze {
        get {
            if (_matcherFreeze == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.Freeze);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherFreeze = matcher;
            }

            return _matcherFreeze;
        }
    }
}
