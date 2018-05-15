//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public StunTimeRemovedListenerComponent stunTimeRemovedListener { get { return (StunTimeRemovedListenerComponent)GetComponent(LogicComponentsLookup.StunTimeRemovedListener); } }
    public bool hasStunTimeRemovedListener { get { return HasComponent(LogicComponentsLookup.StunTimeRemovedListener); } }

    public void AddStunTimeRemovedListener(System.Collections.Generic.List<IStunTimeRemovedListener> newValue) {
        var index = LogicComponentsLookup.StunTimeRemovedListener;
        var component = CreateComponent<StunTimeRemovedListenerComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceStunTimeRemovedListener(System.Collections.Generic.List<IStunTimeRemovedListener> newValue) {
        var index = LogicComponentsLookup.StunTimeRemovedListener;
        var component = CreateComponent<StunTimeRemovedListenerComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveStunTimeRemovedListener() {
        RemoveComponent(LogicComponentsLookup.StunTimeRemovedListener);
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

    static Entitas.IMatcher<LogicEntity> _matcherStunTimeRemovedListener;

    public static Entitas.IMatcher<LogicEntity> StunTimeRemovedListener {
        get {
            if (_matcherStunTimeRemovedListener == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.StunTimeRemovedListener);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherStunTimeRemovedListener = matcher;
            }

            return _matcherStunTimeRemovedListener;
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

    public void AddStunTimeRemovedListener(IStunTimeRemovedListener value) {
        var listeners = hasStunTimeRemovedListener
            ? stunTimeRemovedListener.value
            : new System.Collections.Generic.List<IStunTimeRemovedListener>();
        listeners.Add(value);
        ReplaceStunTimeRemovedListener(listeners);
    }

    public void RemoveStunTimeRemovedListener(IStunTimeRemovedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = stunTimeRemovedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveStunTimeRemovedListener();
        } else {
            ReplaceStunTimeRemovedListener(listeners);
        }
    }
}