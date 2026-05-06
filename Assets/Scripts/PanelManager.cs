using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject infoPanelPrefab;
    public Transform panelParent;

    private Dictionary<string, GameObject> activePanels = new Dictionary<string, GameObject>();

    public void ShowPanel(string barcode, FoodItem food)
    {
        Debug.Log("ShowPanel wurde aufgerufen: " + barcode);

        if (infoPanelPrefab == null)
        {
            Debug.LogError("InfoPanel Prefab fehlt im PanelManager!");
            return;
        }

        if (panelParent == null)
        {
            Debug.LogError("Panel Parent fehlt im PanelManager!");
            return;
        }

        if (food == null)
        {
            Debug.LogError("Food ist null!");
            return;
        }

        if (activePanels.ContainsKey(barcode))
        {
            PanelUpdater updater = activePanels[barcode].GetComponent<PanelUpdater>();

            if (updater == null)
            {
                Debug.LogError("PanelUpdater fehlt auf bestehendem Panel!");
                return;
            }

            updater.UpdatePanel(food);
            return;
        }

        GameObject newPanel = Instantiate(infoPanelPrefab, panelParent);
        Debug.Log("Neues Panel erzeugt: " + newPanel.name);

        PanelUpdater newUpdater = newPanel.GetComponent<PanelUpdater>();

        if (newUpdater == null)
        {
            Debug.LogError("PanelUpdater fehlt auf InfoPanel Prefab!");
            return;
        }

        newUpdater.UpdatePanel(food);
        activePanels.Add(barcode, newPanel);
    }
}