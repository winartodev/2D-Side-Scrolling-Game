using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
    GameObject _inventoryPanel;
    GameObject _mainMenuPanel;
    GameObject _shortcutPanel;

    private void Start() {
        _inventoryPanel = GameObject.Find("InventoryPanel");
        _mainMenuPanel = GameObject.Find("MainMenuPanel");
        _shortcutPanel = GameObject.Find("ShortcutPanel");

        if (_inventoryPanel.activeSelf) { _inventoryPanel.SetActive(false); }
        if (_mainMenuPanel.activeSelf) { _mainMenuPanel.SetActive(false); }
        if (_shortcutPanel.activeSelf) { _shortcutPanel.SetActive(false); }
    }

    void LateUpdate() {
        if (Input.GetKeyDown(KeyCode.I)) {
            ShowPanel(_inventoryPanel);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            ShowPanel(_mainMenuPanel);
        }
    }

    public void ShowPanel(GameObject panel) {
        if (panel.activeSelf) {
            HidePanel(panel);
            return;
        }

        panel.SetActive(true);
    }

    public void HidePanel(GameObject Panel) {
        Panel.SetActive(false);
    }
}
