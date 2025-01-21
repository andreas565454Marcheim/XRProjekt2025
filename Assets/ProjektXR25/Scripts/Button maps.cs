using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Vuforia;
using System.Collections;

public class GoogleMapsButtonAR : MonoBehaviour
{
    public Button yourButton;
    public Text outputText;  // Optional, um die Route anzuzeigen
    public string apiKey = "DEIN_GOOGLE_API_SCHLÜSSEL";

    void Start()
    {
        // Button-Click-Event abonnieren
        yourButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        StartCoroutine(GetRoute());
    }

    // Coroutine, um die Google Maps API anzusprechen
    IEnumerator GetRoute()
    {
        string origin = "Startadresse"; // Ersetze dies mit der Startadresse
        string destination = "Zieladresse"; // Ersetze dies mit der Zieladresse

        // URL der Google Directions API
        string url = $"https://maps.googleapis.com/maps/api/directions/json?origin={origin}&destination={destination}&key={apiKey}";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                // Antwort verarbeiten (hier als Beispiel, nur die JSON-Daten ausgeben)
                string jsonResponse = webRequest.downloadHandler.text;
                outputText.text = jsonResponse; // Zeigt die Antwort im Textfeld an
                Debug.Log(jsonResponse);
            }
            else
            {
                // Fehlerbehandlung
                Debug.LogError("Fehler bei der Anfrage: " + webRequest.error);
            }
        }
    }
}
