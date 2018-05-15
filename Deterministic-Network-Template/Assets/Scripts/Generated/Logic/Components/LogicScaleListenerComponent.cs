//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LogicEntity {

    public ScaleListenerComponent scaleListener { get { return (ScaleListenerComponent)GetComponent(LogicComponentsLookup.ScaleListener); } }
    public bool hasScaleListener { get { return HasComponent(LogicComponentsLookup.ScaleListener); } }

    public void AddScaleListener(System.Collections.Generic.List<IScaleListener> newValue) {
        var index = LogicComponentsLookup.ScaleListener;
        var component = CreateComponent<ScaleListenerComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceScaleListener(System.Collections.Generic.List<IScaleListener> newValue) {
        var index = LogicComponentsLookup.ScaleListener;
        var component = CreateComponent<ScaleListenerComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveScaleListener() {
        RemoveComponent(LogicComponentsLookup.ScaleListener);
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

    static Entitas.IMatcher<LogicEntity> _matcherScaleListener;

    public static Entitas.IMatcher<LogicEntity> ScaleListener {
        get {
            if (_matcherScaleListener == null) {
                var matcher = (Entitas.Matcher<LogicEntity>)Entitas.Matcher<LogicEntity>.AllOf(LogicComponentsLookup.ScaleListener);
                matcher.componentNames = LogicComponentsLookup.componentNames;
                _matcherScaleListener = matcher;
            }

            return _matcherScaleListener;
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

    public void AddScaleListener(IScaleListener value) {
        var listeners = hasScaleListener
            ? scaleListener.value
            : new System.Collections.Generic.List<IScaleListener>();
        listeners.Add(value);
        ReplaceScaleListener(listeners);
    }

    public void RemoveScaleListener(IScaleListener value, bool removeComponentWhenEmpty = true) {
        var listeners = scaleListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveScaleListener();
        } else {
            ReplaceScaleListener(listeners);
        }
    }
}