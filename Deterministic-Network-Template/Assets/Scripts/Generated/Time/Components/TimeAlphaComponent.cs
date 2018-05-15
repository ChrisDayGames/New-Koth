//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TimeContext {

    public TimeEntity alphaEntity { get { return GetGroup(TimeMatcher.Alpha).GetSingleEntity(); } }
    public AlphaComponent alpha { get { return alphaEntity.alpha; } }
    public bool hasAlpha { get { return alphaEntity != null; } }

    public TimeEntity SetAlpha(float newValue) {
        if (hasAlpha) {
            throw new Entitas.EntitasException("Could not set Alpha!\n" + this + " already has an entity with AlphaComponent!",
                "You should check if the context already has a alphaEntity before setting it or use context.ReplaceAlpha().");
        }
        var entity = CreateEntity();
        entity.AddAlpha(newValue);
        return entity;
    }

    public void ReplaceAlpha(float newValue) {
        var entity = alphaEntity;
        if (entity == null) {
            entity = SetAlpha(newValue);
        } else {
            entity.ReplaceAlpha(newValue);
        }
    }

    public void RemoveAlpha() {
        alphaEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TimeEntity {

    public AlphaComponent alpha { get { return (AlphaComponent)GetComponent(TimeComponentsLookup.Alpha); } }
    public bool hasAlpha { get { return HasComponent(TimeComponentsLookup.Alpha); } }

    public void AddAlpha(float newValue) {
        var index = TimeComponentsLookup.Alpha;
        var component = CreateComponent<AlphaComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAlpha(float newValue) {
        var index = TimeComponentsLookup.Alpha;
        var component = CreateComponent<AlphaComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAlpha() {
        RemoveComponent(TimeComponentsLookup.Alpha);
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
public sealed partial class TimeMatcher {

    static Entitas.IMatcher<TimeEntity> _matcherAlpha;

    public static Entitas.IMatcher<TimeEntity> Alpha {
        get {
            if (_matcherAlpha == null) {
                var matcher = (Entitas.Matcher<TimeEntity>)Entitas.Matcher<TimeEntity>.AllOf(TimeComponentsLookup.Alpha);
                matcher.componentNames = TimeComponentsLookup.componentNames;
                _matcherAlpha = matcher;
            }

            return _matcherAlpha;
        }
    }
}