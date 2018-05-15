//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class StunTimeEventSystem : Entitas.ReactiveSystem<LogicEntity> {

    public StunTimeEventSystem(Contexts contexts) : base(contexts.logic) {
    }

    protected override Entitas.ICollector<LogicEntity> GetTrigger(Entitas.IContext<LogicEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(LogicMatcher.StunTime)
        );
    }

    protected override bool Filter(LogicEntity entity) {
        return entity.hasStunTime && entity.hasStunTimeListener;
    }

    protected override void Execute(System.Collections.Generic.List<LogicEntity> entities) {
        foreach (var e in entities) {
            var component = e.stunTime;
            foreach (var listener in e.stunTimeListener.value) {
                listener.OnStunTime(e, component.value);
            }
        }
    }
}
