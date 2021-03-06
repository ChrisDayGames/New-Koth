//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    static readonly ControllableComponent controllableComponent = new ControllableComponent();

    public bool isControllable {
        get { return HasComponent(LogicComponentsLookup.Controllable); }
        set {
            if (value != isControllable) {
                var index = LogicComponentsLookup.Controllable;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : controllableComponent;

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

    static Entitas.IMatcher<LogicEntity> _matcherControllable;

    public static Entitas.IMatcher<LogicEntity> Controllable {
        get {
            if (_matcherControllable == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.Controllable);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherControllable = matcher;
            }

            return _matcherControllable;
        }
    }
}
