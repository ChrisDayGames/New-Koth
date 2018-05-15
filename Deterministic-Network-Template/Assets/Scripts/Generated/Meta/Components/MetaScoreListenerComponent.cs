//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class MetaEntity {

    public ScoreListenerComponent scoreListener { get { return (ScoreListenerComponent)GetComponent(MetaComponentsLookup.ScoreListener); } }
    public bool hasScoreListener { get { return HasComponent(MetaComponentsLookup.ScoreListener); } }

    public void AddScoreListener(System.Collections.Generic.List<IScoreListener> newValue) {
        var index = MetaComponentsLookup.ScoreListener;
        var component = CreateComponent<ScoreListenerComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceScoreListener(System.Collections.Generic.List<IScoreListener> newValue) {
        var index = MetaComponentsLookup.ScoreListener;
        var component = CreateComponent<ScoreListenerComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveScoreListener() {
        RemoveComponent(MetaComponentsLookup.ScoreListener);
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
public sealed partial class MetaMatcher {

    static Entitas.IMatcher<MetaEntity> _matcherScoreListener;

    public static Entitas.IMatcher<MetaEntity> ScoreListener {
        get {
            if (_matcherScoreListener == null) {
                var matcher = (Entitas.Matcher<MetaEntity>)Entitas.Matcher<MetaEntity>.AllOf(MetaComponentsLookup.ScoreListener);
                matcher.componentNames = MetaComponentsLookup.componentNames;
                _matcherScoreListener = matcher;
            }

            return _matcherScoreListener;
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
public partial class MetaEntity {

    public void AddScoreListener(IScoreListener value) {
        var listeners = hasScoreListener
            ? scoreListener.value
            : new System.Collections.Generic.List<IScoreListener>();
        listeners.Add(value);
        ReplaceScoreListener(listeners);
    }

    public void RemoveScoreListener(IScoreListener value, bool removeComponentWhenEmpty = true) {
        var listeners = scoreListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveScoreListener();
        } else {
            ReplaceScoreListener(listeners);
        }
    }
}