// Steuert Kamera-Barcode-Scan, prüft erkannte EAN-13 Codes und löst UI-Panels aus.

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ZXing;
using ZXing.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Android;

public class BarcodeScannerManager : MonoBehaviour
{
    public RawImage cameraView;
    public TextMeshProUGUI resultText;

    private WebCamTexture webcamTexture;
    private BarcodeReader barcodeReader;

    void Start()
    {
        resultText.text = "";

        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }

        StartCamera();
    }

    void StartCamera()
    {
        if (WebCamTexture.devices.Length == 0)
        {
            resultText.text = "Keine Kamera gefunden";
            Debug.LogError("Keine Kamera gefunden");
            return;
        }

        string cameraName = WebCamTexture.devices[0].name;
        webcamTexture = new WebCamTexture(cameraName, 1280, 720);
        if (cameraView != null)
            {
                cameraView.texture = webcamTexture;
            }
        webcamTexture.Play();

        Debug.Log("Kamera gestartet: " + cameraName);

        barcodeReader = new BarcodeReader
        {
            AutoRotate = true,
            Options = new DecodingOptions
            {
                TryHarder = true,
                PossibleFormats = new List<BarcodeFormat>
                {
                    BarcodeFormat.EAN_13
                }
            }
        };

        StartCoroutine(ScanLoop());
    }

    IEnumerator ScanLoop()
    {
        while (true)
        {
            if (webcamTexture != null && webcamTexture.width > 100)
            {
                var results = barcodeReader.DecodeMultiple(
                    webcamTexture.GetPixels32(),
                    webcamTexture.width,
                    webcamTexture.height
                );

                if (results != null)
                {
                    foreach (var r in results)
                    {
                        string barcode = r.Text;

                        if (barcode.Length != 13)
                            continue;

                        Debug.Log("EAN-13 ERKANNT: " + barcode);

                        if (FoodDatabase.Instance == null)
                        {
                            Debug.LogWarning("FoodDatabase fehlt in Szene");
                            continue;
                        }

                        FoodItem food = FoodDatabase.Instance.GetFoodById(barcode);
                        Debug.Log("Food gefunden? " + (food != null));

                        if (food != null)
                        {
                            FindObjectOfType<PanelManager>().ShowPanel(barcode, food);
                        }
                        else
                        {
                            Debug.Log("Barcode nicht in foods.json: " + barcode);
                            resultText.text = "Produkt nicht erkannt:\n" + barcode;
                            StartCoroutine(ClearResultTextAfterDelay(3f));
                        }
                    }
                }
            }

            yield return new WaitForSeconds(0.4f);
        }
    }

    private IEnumerator ClearResultTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        resultText.text = "";
    }

}