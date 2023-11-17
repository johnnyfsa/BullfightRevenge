using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OptionMenuController : MonoBehaviour
{
    private class SelectableElement
    {
        public Slider slider;
        public Button button;

        public SelectableElement(Slider slider)
        {
            this.slider = slider;
        }
        public SelectableElement(Button button)
        {
            this.button = button;
        }

        public void Focus()
        {
            if (button == null)
            {
                slider.Focus();
            }
            else
            {
                button.Focus();
            }
        }
    }

    VisualElement root;
    private int index;

    private List<SelectableElement> selectableElements;

    void OnEnable()
    {
        index = 0;
        selectableElements = new List<SelectableElement>();
        root = GetComponent<UIDocument>().rootVisualElement;
        Button confirmButton = root.Q<Button>("confirmBtn");
        confirmButton.Focus();
        VisualElement optionBox = root.Q<VisualElement>("OptionBox");
        List<Slider> sliders = new List<Slider>();
        sliders = optionBox.Query<Slider>().ToList();
        foreach (Slider slider in sliders)
        {
            selectableElements.Add(new SelectableElement(slider));
        }
        foreach (Button button in optionBox.Query<Button>().ToList())
        {
            selectableElements.Add(new SelectableElement(button));
        }
        root.RegisterCallback<KeyDownEvent>(OnNavigateUI, TrickleDown.TrickleDown);
    }

    protected void ConfirmAction(KeyDownEvent evt)
    {
        if (evt.keyCode == KeyCode.Space || evt.keyCode == KeyCode.Return)
        {

        }
    }

    protected void OnNavigateUI(KeyDownEvent evt)
    {
        KeyCode keyPressed = evt.keyCode;
        switch (keyPressed)
        {
            case KeyCode.UpArrow:
                index--;
                if (index < 0)
                {
                    index = selectableElements.Count - 1;
                }
                break;
            case KeyCode.DownArrow:
                index++;
                if (index > selectableElements.Count - 1)
                {
                    index = 0;
                }
                break;
            case KeyCode.W:
                index--;
                if (index < 0)
                {
                    index = selectableElements.Count - 1;
                }
                break;
            case KeyCode.S:
                index++;
                if (index > selectableElements.Count - 1)
                {
                    index = 0;
                }
                break;
        }
        selectableElements[index].Focus();
    }
}
