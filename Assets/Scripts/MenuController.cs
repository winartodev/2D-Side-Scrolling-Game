using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    const string inventoryPanel = "MenuController/InventoryPanel";
    const string mainMenuPanel = "MenuController/MainMenuPanel";


    GameObject _inventoryPanel;
    GameObject _mainmenuPanel;
    // Start is called before the first frame update
    void Start()
    {
        _inventoryPanel = GameObject.Find(inventoryPanel);
        _mainmenuPanel = GameObject.Find(mainMenuPanel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.I)) {
            ShowPanel(_inventoryPanel);
        }

        if (Input.GetKey(KeyCode.Escape)) {
            ShowPanel(_mainmenuPanel);
        }
    }

    public void ShowPanel(GameObject panel) {
        if (panel.activeSelf) {
            HidePanel(panel);
        }
        panel.SetActive(true);
    }

    public void HidePanel(GameObject Panel) {
        Panel.SetActive(false);
    }

    public void x() {

    }
}
