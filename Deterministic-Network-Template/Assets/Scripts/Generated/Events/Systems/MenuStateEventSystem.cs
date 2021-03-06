//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class MenuStateEventSystem : Entitas.ReactiveSystem<MetaEntity> {

    readonly Entitas.IGroup<MetaEntity> _listeners;

    public MenuStateEventSystem(Contexts contexts) : base(contexts.meta) {
        _listeners = contexts.meta.GetGroup(MetaMatcher.MenuStateListener);
    }

    protected override Entitas.ICollector<MetaEntity> GetTrigger(Entitas.IContext<MetaEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(MetaMatcher.MenuState)
        );
    }

    protected override bool Filter(MetaEntity entity) {
        return entity.hasMenuState;
    }

    protected override void Execute(System.Collections.Generic.List<MetaEntity> entities) {
        foreach (var e in entities) {
            var component = e.menuState;
            foreach (var listenerEntity in _listeners) {
                foreach (var listener in listenerEntity.menuStateListener.value) {
                    listener.OnMenuState(e, component.value);
                }
            }
        }
    }
}
