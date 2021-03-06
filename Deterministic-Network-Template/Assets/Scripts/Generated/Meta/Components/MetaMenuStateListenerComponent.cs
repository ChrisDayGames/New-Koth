//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class MetaEntity {

    public MenuStateListenerComponent menuStateListener { get { return (MenuStateListenerComponent)GetComponent(MetaComponentsLookup.MenuStateListener); } }
    public bool hasMenuStateListener { get { return HasComponent(MetaComponentsLookup.MenuStateListener); } }

    public void AddMenuStateListener(System.Collections.Generic.List<IMenuStateListener> newValue) {
        var index = MetaComponentsLookup.MenuStateListener;
        var component = CreateComponent<MenuStateListenerComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceMenuStateListener(System.Collections.Generic.List<IMenuStateListener> newValue) {
        var index = MetaComponentsLookup.MenuStateListener;
        var component = CreateComponent<MenuStateListenerComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveMenuStateListener() {
        RemoveComponent(MetaComponentsLookup.MenuStateListener);
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

    static Entitas.IMatcher<MetaEntity> _matcherMenuStateListener;

    public static Entitas.IMatcher<MetaEntity> MenuStateListener {
        get {
            if (_matcherMenuStateListener == null) {
                var matcher = (Entitas.Matcher<MetaEntity>)Entitas.Matcher<MetaEntity>.AllOf(MetaComponentsLookup.MenuStateListener);
                matcher.componentNames = MetaComponentsLookup.componentNames;
                _matcherMenuStateListener = matcher;
            }

            return _matcherMenuStateListener;
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

    public void AddMenuStateListener(IMenuStateListener value) {
        var listeners = hasMenuStateListener
            ? menuStateListener.value
            : new System.Collections.Generic.List<IMenuStateListener>();
        listeners.Add(value);
        ReplaceMenuStateListener(listeners);
    }

    public void RemoveMenuStateListener(IMenuStateListener value, bool removeComponentWhenEmpty = true) {
        var listeners = menuStateListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveMenuStateListener();
        } else {
            ReplaceMenuStateListener(listeners);
        }
    }
}
