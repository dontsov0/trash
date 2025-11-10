using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIGameObjectSwitcher : MonoBehaviour
{
    [SerializeField] private List<UIGameObjectSwitcherChild> gameObjects;
    [SerializeField] private GameObject root;
    [SerializeField] private UIGameObjectSwitcherChild selected;
    [SerializeField] private int currentGameObjectId;

    private void Awake()
    {
        if  (gameObjects == null || root == null) return;

        gameObjects = new  List<UIGameObjectSwitcherChild>(root.GetComponentsInChildren<UIGameObjectSwitcherChild>());
    }

    [ContextMenu("SetDefaultGameObject")]
    public void SetDefaultGameObject()
    {
        SwitchGameObject(currentGameObjectId);
    }

    public void SwitchGameObject(int id)
    {
        if (gameObjects == null) return;
        
        currentGameObjectId = id;

        if (currentGameObjectId >= 0 && currentGameObjectId < gameObjects.Count)
        {
            selected = gameObjects[currentGameObjectId];
        }
        else
        {
            selected = gameObjects.Last();
        }
        
        foreach (var go in gameObjects)
        {
            go.gameObject.SetActive(go == selected);
            go.SetActive(go == selected);
        }
    }
}
