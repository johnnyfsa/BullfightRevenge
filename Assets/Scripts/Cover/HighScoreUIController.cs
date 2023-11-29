using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class HighScoreUIController : MonoBehaviour
{
    ListView list;
    [SerializeField] VisualTreeAsset listEntryTemplate;
    void OnEnable()
    {


        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

    }
}

