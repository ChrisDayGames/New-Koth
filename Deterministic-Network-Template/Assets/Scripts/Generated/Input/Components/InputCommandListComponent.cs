//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputContext {

    public InputEntity commandListEntity { get { return GetGroup(InputMatcher.CommandList).GetSingleEntity(); } }
    public CommandListComponent commandList { get { return commandListEntity.commandList; } }
    public bool hasCommandList { get { return commandListEntity != null; } }

    public InputEntity SetCommandList(System.Collections.Generic.List<CommandInput.Command> newCommands) {
        if (hasCommandList) {
            throw new Entitas.EntitasException("Could not set CommandList!\n" + this + " already has an entity with CommandListComponent!",
                "You should check if the context already has a commandListEntity before setting it or use context.ReplaceCommandList().");
        }
        var entity = CreateEntity();
        entity.AddCommandList(newCommands);
        return entity;
    }

    public void ReplaceCommandList(System.Collections.Generic.List<CommandInput.Command> newCommands) {
        var entity = commandListEntity;
        if (entity == null) {
            entity = SetCommandList(newCommands);
        } else {
            entity.ReplaceCommandList(newCommands);
        }
    }

    public void RemoveCommandList() {
        commandListEntity.Destroy();
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

    public CommandListComponent commandList { get { return (CommandListComponent)GetComponent(InputComponentsLookup.CommandList); } }
    public bool hasCommandList { get { return HasComponent(InputComponentsLookup.CommandList); } }

    public void AddCommandList(System.Collections.Generic.List<CommandInput.Command> newCommands) {
        var index = InputComponentsLookup.CommandList;
        var component = CreateComponent<CommandListComponent>(index);
        component.commands = newCommands;
        AddComponent(index, component);
    }

    public void ReplaceCommandList(System.Collections.Generic.List<CommandInput.Command> newCommands) {
        var index = InputComponentsLookup.CommandList;
        var component = CreateComponent<CommandListComponent>(index);
        component.commands = newCommands;
        ReplaceComponent(index, component);
    }

    public void RemoveCommandList() {
        RemoveComponent(InputComponentsLookup.CommandList);
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

    static Entitas.IMatcher<InputEntity> _matcherCommandList;

    public static Entitas.IMatcher<InputEntity> CommandList {
        get {
            if (_matcherCommandList == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.CommandList);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherCommandList = matcher;
            }

            return _matcherCommandList;
        }
    }
}
