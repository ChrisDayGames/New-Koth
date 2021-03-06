//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    static readonly AssignedToPlayer assignedToPlayerComponent = new AssignedToPlayer();

    public bool isAssignedToPlayer {
        get { return HasComponent(InputComponentsLookup.AssignedToPlayer); }
        set {
            if (value != isAssignedToPlayer) {
                var index = InputComponentsLookup.AssignedToPlayer;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : assignedToPlayerComponent;

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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherAssignedToPlayer;

    public static Entitas.IMatcher<InputEntity> AssignedToPlayer {
        get {
            if (_matcherAssignedToPlayer == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.AssignedToPlayer);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherAssignedToPlayer = matcher;
            }

            return _matcherAssignedToPlayer;
        }
    }
}
