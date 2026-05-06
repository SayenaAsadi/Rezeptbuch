using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class BarcodeScannerTest : MonoBehaviour
{
    public RawImage cameraPreview;

    private WebCamTexture camTexture;
    private BarcodeReader reader;
    private float timer;

    void Start()
{
    WebCamDevice[] devices = WebCamTexture.devices;

    if (devices.Length == 0)
    {
        Debug.Log("Keine Kamera gefunden");
        return;
    }

    string camName = devices[0].name;
    Debug.Log("Nutze Kamera: " + camName);

    camTexture = new WebCamTexture(camName);

    cameraPreview.texture = camTexture;

    camTexture.Play();

    reader = new BarcodeReader();
}

    void Update()
    {
        if (camTexture == null || camTexture.width < 100) return;

        timer += Time.deltaTime;
        if (timer < 0.3f) return;
        timer = 0f;

        var result = reader.Decode(
            camTexture.GetPixels32(),
            camTexture.width,
            camTexture.height
        );

        if (result != null)
        {
            Debug.Log("BARCODE ERKANNT: " + result.Text);
        }
    }
}   