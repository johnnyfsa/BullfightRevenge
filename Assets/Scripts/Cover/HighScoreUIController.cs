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
    List<PlayerData> highScores;
    VisualElement root;
    [SerializeField] VisualTreeAsset listEntryTemplate;

    // private int selectedItemIndex = -1;
    void OnEnable()
    {
        highScores = GameManager.Instance.GetTopScores();

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
            (item.userData as ListEntryController).SetEntryName(highScores[index].PlayerName);
            (item.userData as ListEntryController).SetEntryScore(highScores[index].Score.ToString());
        };

        list.itemsSource = highScores;

        root.RegisterCallback<KeyDownEvent>(ConfirmAction, TrickleDown.TrickleDown);
        button.RegisterCallback<ClickEvent>(OnClick);
        root.RegisterCallback<KeyDownEvent>(NavigateUI, TrickleDown.TrickleDown);
        button.RegisterCallback<KeyDownEvent>(FocusList, TrickleDown.TrickleDown);
        list.Focus();
        list.selectedIndex = 0;
    }

    private void ConfirmAction(KeyDownEvent evt)
    {
        KeyCode keyPressed = evt.keyCode;
        if (IsFocused(button))
        {
            if (keyPressed == KeyCode.Return || keyPressed == KeyCode.Space)
            {
                OnScreenClosed?.Invoke();
            }
        }
    }

    private void FocusList(KeyDownEvent evt)
    {
        KeyCode keyPressed = evt.keyCode;
        if (keyPressed == KeyCode.UpArrow || keyPressed == KeyCode.W)
        {
            list.Focus();
            list.selectedIndex = highScores.Count - 1;
        }
    }

    private void NavigateUI(KeyDownEvent evt)
    {
        KeyCode keyPressed = evt.keyCode;
        if (IsFocused(list))
        {
            switch (keyPressed)
            {
                case KeyCode.UpArrow:
                    if (list.selectedIndex > 0)
                    {
                        list.selectedIndex--;
                    }
                    break;
                case KeyCode.DownArrow:
                    if (list.selectedIndex < highScores.Count - 1)
                    {
                        list.selectedIndex++;
                    }
                    else
                    {
                        button.Focus();
                    }
                    break;
                case KeyCode.W:
                    if (list.selectedIndex > 0)
                    {
                        list.selectedIndex--;
                    }
                    break;
                case KeyCode.S:
                    if (list.selectedIndex < highScores.Count - 1)
                    {
                        list.selectedIndex++;
                    }
                    else
                    {
                        button.Focus();
                    }
                    break;
            }
        }
    }

    private void OnClick(ClickEvent evt)
    {
        OnScreenClosed?.Invoke();
    }


    bool IsFocused(VisualElement e)
    {
        if (e.panel != null && e.panel.focusController.focusedElement == e)
        {
            return true;
        }
        return false;
    }
}

