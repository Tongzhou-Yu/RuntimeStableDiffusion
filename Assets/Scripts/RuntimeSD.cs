using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class RuntimeSD : MonoBehaviour
{
    [Header("Basic Elements")]
    public InputField inputField;
    public RenderTexture m_renderTexture;
    public Button button;
    [Header("Stable Diffusion Settings")]
    public string m_url = "http://127.0.0.1:7860";
    public string m_lora = " ";
    public string m_steps = "20";
    public string m_width = "512";
    public string m_height = "512";
    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }
    public void OnButtonClick()
    {
        string prompt = inputField.text;
        StartCoroutine(SendPostRequest(prompt));
    }
    IEnumerator SendPostRequest(string prompt)
    {
        string url = m_url + "/sdapi/v1/txt2img";

        // Create a payload
        JObject payload = new JObject();
        payload["prompt"] = prompt + "," + m_lora;
        payload["steps"] = m_steps;
        payload["width"] = m_width;
        payload["height"] = m_height;

        // Create a request
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(payload.ToString());
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // Send the request
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Parse the response
            JObject response = JObject.Parse(www.downloadHandler.text);
            string base64Image = response["images"][0].ToString();

            // Convert base64 to Texture2D
            Texture2D texture = new Texture2D(2, 2);
            byte[] imageBytes = System.Convert.FromBase64String(base64Image);
            if (texture.LoadImage(imageBytes))
            {
                // Apply texture to RenderTexture
                Graphics.Blit(texture, m_renderTexture);
            }
        }
    }
}