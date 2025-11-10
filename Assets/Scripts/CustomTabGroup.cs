using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CustomTabGroup : MonoBehaviour
{
    [SerializeField]
    private GameObject rootGameObject;
    [SerializeField]
    private List<CustomToogle> tabs = new List<CustomToogle>();

    [SerializeField] private CustomToogle selectedTab;
    [SerializeField] private int selectedTabId = 0;
    
    public IntEvent onTabChanged;

    private void Awake()
    {
        if (rootGameObject == null) return;
        
        if (tabs == null || tabs.Count == 0)
        {
            tabs = new List<CustomToogle>(rootGameObject.GetComponentsInChildren<CustomToogle>(true));
        }
    }

    private void Start()
    {
        if (tabs != null)
        {
            var currentId = 0;
            foreach (var tab in tabs)
            {
                tab.id = currentId++;
                var capturedToggle = tab;
                tab.onClick.AddListener(() => OnTabClicked(capturedToggle));
            }
        }
    }
    
    public int GetSelectedTabId() => selectedTabId;

    private void OnTabClicked(CustomToogle _tab)
    {
        this.selectedTab = _tab;
        selectedTabId = _tab.id;
        onTabChanged.Invoke(_tab.id);
        
        foreach (var tab in tabs)
        {
            if (tab != this.selectedTab)
            {
                tab.Unselect();
            } 
        }
    }

    public void SelectTab(int id)
    {
        if (tabs != null && id >= 0 && id < tabs.Count)
        {
            tabs[id].Select();
            onTabChanged.Invoke(id);
        }
    }
}
