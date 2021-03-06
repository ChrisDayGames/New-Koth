//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public StunnedRemovedListenerComponent stunnedRemovedListener { get { return (StunnedRemovedListenerComponent)GetComponent(LogicComponentsLookup.StunnedRemovedListener); } }
    public bool hasStunnedRemovedListener { get { return HasComponent(LogicComponentsLookup.StunnedRemovedListener); } }

    public void AddStunnedRemovedListener(System.Collections.Generic.List<IStunnedRemovedListener> newValue) {
        var index = LogicComponentsLookup.StunnedRemovedListener;
        var component = CreateComponent<StunnedRemovedListenerComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceStunnedRemovedListener(System.Collections.Generic.List<IStunnedRemovedListener> newValue) {
        var index = LogicComponentsLookup.StunnedRemovedListener;
        var component = CreateComponent<StunnedRemovedListenerComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveStunnedRemovedListener() {
        RemoveComponent(LogicComponentsLookup.StunnedRemovedListener);
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

    static Entitas.IMatcher<LogicEntity> _matcherStunnedRemovedListener;

    public static Entitas.IMatcher<LogicEntity> StunnedRemovedListener {
        get {
            if (_matcherStunnedRemovedListener == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.StunnedRemovedListener);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherStunnedRemovedListener = matcher;
            }

            return _matcherStunnedRemovedListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public void AddStunnedRemovedListener(IStunnedRemovedListener value) {
        var listeners = hasStunnedRemovedListener
            ? stunnedRemovedListener.value
            : new System.Collections.Generic.List<IStunnedRemovedListener>();
        listeners.Add(value);
        ReplaceStunnedRemovedListener(listeners);
    }

    public void RemoveStunnedRemovedListener(IStunnedRemovedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = stunnedRemovedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveStunnedRemovedListener();
        } else {
            ReplaceStunnedRemovedListener(listeners);
        }
    }
}
