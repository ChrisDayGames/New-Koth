//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public FreezeRemovedListenerComponent freezeRemovedListener { get { return (FreezeRemovedListenerComponent)GetComponent(LogicComponentsLookup.FreezeRemovedListener); } }
    public bool hasFreezeRemovedListener { get { return HasComponent(LogicComponentsLookup.FreezeRemovedListener); } }

    public void AddFreezeRemovedListener(System.Collections.Generic.List<IFreezeRemovedListener> newValue) {
        var index = LogicComponentsLookup.FreezeRemovedListener;
        var component = CreateComponent<FreezeRemovedListenerComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceFreezeRemovedListener(System.Collections.Generic.List<IFreezeRemovedListener> newValue) {
        var index = LogicComponentsLookup.FreezeRemovedListener;
        var component = CreateComponent<FreezeRemovedListenerComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveFreezeRemovedListener() {
        RemoveComponent(LogicComponentsLookup.FreezeRemovedListener);
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

    static Entitas.IMatcher<LogicEntity> _matcherFreezeRemovedListener;

    public static Entitas.IMatcher<LogicEntity> FreezeRemovedListener {
        get {
            if (_matcherFreezeRemovedListener == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.FreezeRemovedListener);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherFreezeRemovedListener = matcher;
            }

            return _matcherFreezeRemovedListener;
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

    public void AddFreezeRemovedListener(IFreezeRemovedListener value) {
        var listeners = hasFreezeRemovedListener
            ? freezeRemovedListener.value
            : new System.Collections.Generic.List<IFreezeRemovedListener>();
        listeners.Add(value);
        ReplaceFreezeRemovedListener(listeners);
    }

    public void RemoveFreezeRemovedListener(IFreezeRemovedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = freezeRemovedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveFreezeRemovedListener();
        } else {
            ReplaceFreezeRemovedListener(listeners);
        }
    }
}
