using System;
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
    public event Action OnCloseOptions;
    VisualElement root;
    private int index;

    private List<SelectableElement> selectableElements;
    Slider musicSlider, sfxSlider;
    Button exitBtn;

    void OnEnable()
    {
        index = 0;
        selectableElements = new List<SelectableElement>();
        root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement optionBox = root.Q<VisualElement>("OptionBox");
        musicSlider = optionBox.Q<Slider>("musicSlider");
        sfxSlider = optionBox.Q<Slider>("sfxSlider");
        exitBtn = optionBox.Q<Button>("exitBtn");
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

        sfxSlider.value = AudioManager.Instance.GetSFXVolume() * 100;
        musicSlider.value = AudioManager.Instance.GetMusicVolume() * 100;
        selectableElements[index].Focus();

        root.RegisterCallback<KeyDownEvent>(OnNavigateUI, TrickleDown.TrickleDown);
        root.RegisterCallback<KeyDownEvent>(ConfirmAction, TrickleDown.TrickleDown);
        musicSlider.RegisterCallback<ChangeEvent<float>>(OnMusicSliderChange);
        sfxSlider.RegisterCallback<ChangeEvent<float>>(OnSFXSliderChange);
        exitBtn.RegisterCallback<ClickEvent>(ClickToClose);
    }

    private void ClickToClose(ClickEvent evt)
    {
        OnCloseOptions?.Invoke();
    }

    private void OnSFXSliderChange(ChangeEvent<float> evt)
    {
        float value = evt.newValue / 100;
        AudioManager.Instance.SetSFXVolume(value);
    }

    private void OnMusicSliderChange(ChangeEvent<float> evt)
    {
        float value = evt.newValue / 100;
        AudioManager.Instance.SetMusicVolume(value);
    }

    protected void ConfirmAction(KeyDownEvent evt)
    {
        if (evt.keyCode == KeyCode.Space || evt.keyCode == KeyCode.Return)
        {
            Button temp = FindFocusedButton();
            if (temp != null)
            {
                switch (temp.name)
                {
                    case "confirmBtn":
                        break;
                    case "exitBtn":
                        OnCloseOptions?.Invoke();
                        break;
                }
            }
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
            case KeyCode.A:
                Slider temp = FindFocusedSlider();
                if (temp != null)
                {
                    temp.value -= 0.5f;
                }
                break;
            case KeyCode.D:
                Slider temp1 = FindFocusedSlider();
                if (temp1 != null)
                {
                    temp1.value += 0.5f;
                }
                break;
            case KeyCode.LeftArrow:
                Slider temp2 = FindFocusedSlider();
                if (temp2 != null)
                {
                    temp2.value -= 0.5f;
                }
                break;
            case KeyCode.RightArrow:
                Slider temp3 = FindFocusedSlider();
                if (temp3 != null)
                {
                    temp3.value += 0.5f;
                }
                break;
        }
        selectableElements[index].Focus();
    }


    Button FindFocusedButton()
    {
        foreach (SelectableElement element in selectableElements)
        {
            if (element.button != null)
            {
                if (element.button.panel.focusController.focusedElement == element.button)
                {
                    return element.button;
                }
            }
        }
        return null;
    }

    Slider FindFocusedSlider()
    {
        foreach (SelectableElement element in selectableElements)
        {
            if (element.slider != null)
            {
                if (element.slider.panel.focusController.focusedElement == element.slider)
                {
                    return element.slider;
                }
            }
        }
        return null;
    }
}
