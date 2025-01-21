using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Vuforia;

public class SceneScroller : MonoBehaviour
{
    [Header("Scene Elements")]
    public GameObject[] scenes; // Array mit den Szenen (Kinder des ImageTargets)

    [Header("UI Elements")]
    public TextMeshProUGUI sceneDescription; // TextMeshPro-Komponente für die Szenenbeschreibung
    public Button nextButton; // Button für die nächste Szene
    public Button previousButton; // Button für die vorherige Szene

    private int currentSceneIndex = 0; // Index der aktuellen Szene
    private bool isMarkerTracked = false; // Ob der Marker gerade getrackt wird

    void Start()
    {
        // Buttons initialisieren
        nextButton.onClick.AddListener(ShowNextScene);
        previousButton.onClick.AddListener(ShowPreviousScene);

        // Die erste Szene anzeigen
        UpdateScene();

        // Vuforia-Tracking-Ereignisse abonnieren
        var observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    void OnMarkerFound()
    {
        isMarkerTracked = true;
        UpdateScene();
    }
    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            OnMarkerFound();
        }
        else
        {
            OnMarkerLost();
        }
    }
    void OnMarkerLost()
    {
        isMarkerTracked = false;
        // Alle Szenen deaktivieren, wenn der Marker nicht getrackt wird
        foreach (var scene in scenes)
        {
            scene.SetActive(false);
        }
    }

    void UpdateScene()
    {
        if (!isMarkerTracked) return;

        // Alle Szenen deaktivieren
        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i].SetActive(false);
        }

        // Aktuelle Szene aktivieren
        scenes[currentSceneIndex].SetActive(true);

        // Szenenbeschreibung aktualisieren
        sceneDescription.text = $"Szene {currentSceneIndex + 1} von {scenes.Length}";

        // Buttons aktivieren/deaktivieren
        previousButton.interactable = currentSceneIndex > 0;
        nextButton.interactable = currentSceneIndex < scenes.Length - 1;
    }

    void ShowNextScene()
    {
        if (currentSceneIndex < scenes.Length - 1)
        {
            currentSceneIndex++;
            UpdateScene();
        }
    }

    void ShowPreviousScene()
    {
        if (currentSceneIndex > 0)
        {
            currentSceneIndex--;
            UpdateScene();
        }
    }
}