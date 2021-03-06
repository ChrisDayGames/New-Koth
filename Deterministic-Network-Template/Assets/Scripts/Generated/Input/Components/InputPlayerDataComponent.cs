//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public PlayerDataComponent playerData { get { return (PlayerDataComponent)GetComponent(InputComponentsLookup.PlayerData); } }
    public bool hasPlayerData { get { return HasComponent(InputComponentsLookup.PlayerData); } }

    public void AddPlayerData(PlayerData newInfo) {
        var index = InputComponentsLookup.PlayerData;
        var component = CreateComponent<PlayerDataComponent>(index);
        component.info = newInfo;
        AddComponent(index, component);
    }

    public void ReplacePlayerData(PlayerData newInfo) {
        var index = InputComponentsLookup.PlayerData;
        var component = CreateComponent<PlayerDataComponent>(index);
        component.info = newInfo;
        ReplaceComponent(index, component);
    }

    public void RemovePlayerData() {
        RemoveComponent(InputComponentsLookup.PlayerData);
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

    static Entitas.IMatcher<InputEntity> _matcherPlayerData;

    public static Entitas.IMatcher<InputEntity> PlayerData {
        get {
            if (_matcherPlayerData == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.PlayerData);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherPlayerData = matcher;
            }

            return _matcherPlayerData;
        }
    }
}
