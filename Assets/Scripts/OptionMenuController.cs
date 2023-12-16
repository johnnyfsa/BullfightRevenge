using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OptionMenuController : MonoBehaviour
{
    public event Action OnCloseOptions;
    VisualElement root;
    private int index;
    private List<VisualElement> selectableItems;
    Slider musicSlider, sfxSlider;
    Button exitBtn;

    void OnEnable()
    {
        index = 0;
        root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement optionBox = root.Q<VisualElement>("OptionBox");
        musicSlider = optionBox.Q<Slider>("musicSlider");
        sfxSlider = optionBox.Q<Slider>("sfxSlider");
        exitBtn = optionBox.Q<Button>("exitBtn");
        List<Slider> sliders = new List<Slider>();
        sliders = optionBox.Query<Slider>().ToList();
        selectableItems = new List<VisualElement>();
        selectableItems = root.Query(className: "selectable").ToList();

        sfxSlider.value = AudioManager.Instance.GetSFXVolume() * 100;
        musicSlider.value = AudioManager.Instance.GetMusicVolume() * 100;
        selectableItems[index].Focus();

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
                    index = selectableItems.Count - 1;
                }
                break;
            case KeyCode.DownArrow:
                index++;
                if (index > selectableItems.Count - 1)
                {
                    index = 0;
                }
                break;
            case KeyCode.W:
                index--;
                if (index < 0)
                {
                    index = selectableItems.Count - 1;
                }
                break;
            case KeyCode.S:
                index++;
                if (index > selectableItems.Count - 1)
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
        selectableItems[index].Focus();
    }

    private VisualElement FindFocusedItem()
    {
        VisualElement element = selectableItems.Find(x => x == x.panel.focusController.focusedElement);
        return element;
    }

    private Slider FindFocusedSlider()
    {
        VisualElement element = FindFocusedItem();
        if (element is Slider) return element as Slider;
        return null;
    }

    private Button FindFocusedButton()
    {
        VisualElement element = FindFocusedItem();
        if (element is Button) return element as Button;
        return null;
    }
}
