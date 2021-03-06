//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class MetaEntity {

    public ScoreComponent score { get { return (ScoreComponent)GetComponent(MetaComponentsLookup.Score); } }
    public bool hasScore { get { return HasComponent(MetaComponentsLookup.Score); } }

    public void AddScore(int newPlayerID, int newScore) {
        var index = MetaComponentsLookup.Score;
        var component = CreateComponent<ScoreComponent>(index);
        component.playerID = newPlayerID;
        component.score = newScore;
        AddComponent(index, component);
    }

    public void ReplaceScore(int newPlayerID, int newScore) {
        var index = MetaComponentsLookup.Score;
        var component = CreateComponent<ScoreComponent>(index);
        component.playerID = newPlayerID;
        component.score = newScore;
        ReplaceComponent(index, component);
    }

    public void RemoveScore() {
        RemoveComponent(MetaComponentsLookup.Score);
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

    static Entitas.IMatcher<MetaEntity> _matcherScore;

    public static Entitas.IMatcher<MetaEntity> Score {
        get {
            if (_matcherScore == null) {
                var matcher = (Entitas.Matcher<MetaEntity>)Entitas.Matcher<MetaEntity>.AllOf(MetaComponentsLookup.Score);
                matcher.componentNames = MetaComponentsLookup.componentNames;
                _matcherScore = matcher;
            }

            return _matcherScore;
        }
    }
}
