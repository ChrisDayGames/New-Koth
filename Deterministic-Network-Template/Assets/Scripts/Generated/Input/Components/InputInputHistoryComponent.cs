//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputContext {

    public InputEntity inputHistoryEntity { get { return GetGroup(InputMatcher.InputHistory).GetSingleEntity(); } }
    public InputHistory inputHistory { get { return inputHistoryEntity.inputHistory; } }
    public bool hasInputHistory { get { return inputHistoryEntity != null; } }

    public InputEntity SetInputHistory(System.Collections.Generic.List<StoredInput> newSnapshots) {
        if (hasInputHistory) {
            throw new Entitas.EntitasException("Could not set InputHistory!\n" + this + " already has an entity with InputHistory!",
                "You should check if the context already has a inputHistoryEntity before setting it or use context.ReplaceInputHistory().");
        }
        var entity = CreateEntity();
        entity.AddInputHistory(newSnapshots);
        return entity;
    }

    public void ReplaceInputHistory(System.Collections.Generic.List<StoredInput> newSnapshots) {
        var entity = inputHistoryEntity;
        if (entity == null) {
            entity = SetInputHistory(newSnapshots);
        } else {
            entity.ReplaceInputHistory(newSnapshots);
        }
    }

    public void RemoveInputHistory() {
        inputHistoryEntity.Destroy();
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
public partial class InputEntity {

    public InputHistory inputHistory { get { return (InputHistory)GetComponent(InputComponentsLookup.InputHistory); } }
    public bool hasInputHistory { get { return HasComponent(InputComponentsLookup.InputHistory); } }

    public void AddInputHistory(System.Collections.Generic.List<StoredInput> newSnapshots) {
        var index = InputComponentsLookup.InputHistory;
        var component = CreateComponent<InputHistory>(index);
        component.snapshots = newSnapshots;
        AddComponent(index, component);
    }

    public void ReplaceInputHistory(System.Collections.Generic.List<StoredInput> newSnapshots) {
        var index = InputComponentsLookup.InputHistory;
        var component = CreateComponent<InputHistory>(index);
        component.snapshots = newSnapshots;
        ReplaceComponent(index, component);
    }

    public void RemoveInputHistory() {
        RemoveComponent(InputComponentsLookup.InputHistory);
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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherInputHistory;

    public static Entitas.IMatcher<InputEntity> InputHistory {
        get {
            if (_matcherInputHistory == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.InputHistory);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherInputHistory = matcher;
            }

            return _matcherInputHistory;
        }
    }
}
