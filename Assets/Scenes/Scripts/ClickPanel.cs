using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickPanel : MonoBehaviour
{
    // Referenzen für das Info-Panel und die dazugehörigen UI-Komponenten
    public GameObject infoPanel; // Das UI-Panel, das ein- und ausgeblendet wird
    public Text infoText;        // Der Text innerhalb des Panels
    public Image infoImage;      // Das Bild innerhalb des Panels

    // Beispielwerte für die Anzeige
    [TextArea]
    public string information = "Dies ist ein Beispieltext mit Informationen.";
    public Sprite imageSprite; // Bild, das angezeigt werden soll

    // Zustand des Info-Panels
    private bool isPanelActive = false;

    private void Start()
    {
        // Sicherstellen, dass das Info-Panel initial deaktiviert ist
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        // Zustand umschalten
        isPanelActive = !isPanelActive;

        if (infoPanel != null)
        {
            infoPanel.SetActive(isPanelActive);

            // Inhalte aktualisieren, falls aktiv
            if (isPanelActive)
            {
                if (infoText != null)
                {
                    infoText.text = information;
                }

                if (infoImage != null && imageSprite != null)
                {
                    infoImage.sprite = imageSprite;
                }
            }
        }
    }
}
