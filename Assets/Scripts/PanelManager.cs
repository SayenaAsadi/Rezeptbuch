// Erstellt, aktualisiert, markiert und entfernt InfoPanels für erkannte Produkte.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject infoPanelPrefab;
    public Transform panelParent;

    private Dictionary<string, GameObject> activePanels = new Dictionary<string, GameObject>();

    public void ShowPanel(string barcode, FoodItem food)
    {

        barcode = barcode.Trim();

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

        StartCoroutine(AutoHidePanel(barcode, 5f));

        PanelUpdater newUpdater = newPanel.GetComponent<PanelUpdater>();

        if (newUpdater == null)
        {
            Debug.LogError("PanelUpdater fehlt auf InfoPanel Prefab!");
            return;
        }

        newUpdater.UpdatePanel(food);
        activePanels.Add(barcode, newPanel);

        HighlightTopProtein();
    }

    private IEnumerator AutoHidePanel(string barcode, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (activePanels.TryGetValue(barcode, out GameObject panel))
        {
            Destroy(panel);
            activePanels.Remove(barcode);

            Debug.Log("Panel automatisch entfernt: " + barcode);
        }
    }

    private void HighlightTopProtein()
    {
        float bestProtein = -1;
        GameObject bestPanel = null;

        foreach (var pair in activePanels)
        {
            PanelUpdater updater = pair.Value.GetComponent<PanelUpdater>();

            if (updater == null || updater.CurrentFood == null)
                continue;

            if (updater.CurrentFood.proteinPerPortion > bestProtein)
            {
                bestProtein = updater.CurrentFood.proteinPerPortion;
                bestPanel = pair.Value;
            }
        }

        foreach (var panel in activePanels.Values)
        {
            Image image = panel.GetComponent<Image>();

            if (image != null)
            {
                image.color = new Color32(230, 230, 230, 255);
            }
        }

        if (bestPanel != null)
        {
            Image image = bestPanel.GetComponent<Image>();

            if (image != null)
            {
                image.color = new Color32(180, 255, 180, 255);
            }
        }
    }


}