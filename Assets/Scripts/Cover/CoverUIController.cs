using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoverUIController : MonoBehaviour
{
    [SerializeField] CoverBull bull;
    [SerializeField] UIDocument coverScreen;
    void Awake()
    {
        bull.OnBullDestroyed += DisplayCoverScreen;
    }

    private void DisplayCoverScreen()
    {
        coverScreen.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
