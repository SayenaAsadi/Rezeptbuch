using UnityEngine;
using Vuforia;

public class TargetPanelToggle : MonoBehaviour
{
    public GameObject panel;
    private ObserverBehaviour observerBehaviour;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();

        if (panel != null)
            panel.SetActive(false);

        if (observerBehaviour != null)
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
    }

    void OnDestroy()
    {
        if (observerBehaviour != null)
            observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        bool visible = status.Status == Status.TRACKED;

        if (panel != null)
            panel.SetActive(visible);
    }
}