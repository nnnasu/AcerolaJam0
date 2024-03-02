using UnityEngine;
using UnityEditor;
using Core.AbilitySystem.Abilities;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Linq;
using System;

[CustomEditor(typeof(AbilityTemplate))]
public class AbilityTemplateEditor : Editor {
    public override VisualElement CreateInspectorGUI() {
        VisualElement root = new();
        InspectorElement.FillDefaultInspector(root, serializedObject, this);
        AbilityTemplate template = target as AbilityTemplate;

        // root.Add(new Label("Add Actions"));
        // ToolbarMenu menu = new();
        // root.Add(menu);

        // var types = TypeCache.GetTypesDerivedFrom<AbilityAction>().ToList();
        // types.ForEach(x => {
        //     menu.menu.AppendAction(x.Name, (action) => template.Actions.Add(Activator.CreateInstance(x) as AbilityAction));
        // });
        


        return root;
    }

}