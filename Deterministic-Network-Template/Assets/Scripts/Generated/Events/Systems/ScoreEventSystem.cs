//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class ScoreEventSystem : Entitas.ReactiveSystem<MetaEntity> {

    readonly Entitas.IGroup<MetaEntity> _listeners;

    public ScoreEventSystem(Contexts contexts) : base(contexts.meta) {
        _listeners = contexts.meta.GetGroup(MetaMatcher.ScoreListener);
    }

    protected override Entitas.ICollector<MetaEntity> GetTrigger(Entitas.IContext<MetaEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(MetaMatcher.Score)
        );
    }

    protected override bool Filter(MetaEntity entity) {
        return entity.hasScore;
    }

    protected override void Execute(System.Collections.Generic.List<MetaEntity> entities) {
        foreach (var e in entities) {
            var component = e.score;
            foreach (var listenerEntity in _listeners) {
                foreach (var listener in listenerEntity.scoreListener.value) {
                    listener.OnScore(e, component.playerID, component.score);
                }
            }
        }
    }
}