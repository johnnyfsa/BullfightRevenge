using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class HighScoreUIController : MonoBehaviour
{
    public event Action OnScreenClosed;
    ListView list;
    private Button button;
    List<string> names;
    VisualElement root;
    [SerializeField] VisualTreeAsset listEntryTemplate;

    // private int selectedItemIndex = -1;
    void OnEnable()
    {
        names = new List<string>();
        for (int i = 0; i < 10; i++)
        {
            names.Add("Player " + (i + 1));
        }

        root = GetComponent<UIDocument>().rootVisualElement;
        button = root.Q<Button>("Button");
        list = root.Q<ListView>("HighScoresList");
        list.makeItem = () =>
        {
            var newListEntry = listEntryTemplate.Instantiate();
            var newListEntryLogic = new ListEntryController();
            newListEntry.userData = newListEntryLogic;
            newListEntryLogic.SetVisualElement(newListEntry);
            return newListEntry;
        };

        list.bindItem = (item, index) =>
        {
            (item.userData as ListEntryController).SetEntryName(names[index]);
            (item.userData as ListEntryController).SetEntryScore(index.ToString());
        };

        list.itemsSource = names;
        list.Focus();
        list.selectedIndex = 0;
        button.RegisterCallback<NavigationSubmitEvent>(OnNavSubmit);
        button.RegisterCallback<ClickEvent>(OnClick);
    }

    private void OnClick(ClickEvent evt)
    {
        OnScreenClosed?.Invoke();
    }

    private void OnNavSubmit(NavigationSubmitEvent evt)
    {
        OnScreenClosed?.Invoke();
    }
}

