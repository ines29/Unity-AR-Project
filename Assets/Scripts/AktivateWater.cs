using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class AktivateWater : MonoBehaviour
{
    private float nextActionTime = 0.0f;
    public float periodSeconds = 1f;
    public bool isWater = false;
    public UnityEvent onChange;
    bool flowerGrew = false;
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + periodSeconds;
            check();
        }
    }

    public void check()
    {
        StartCoroutine(getRequest("http://192.168.1.1:3000/api/water"));
    }

    IEnumerator getRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for result
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    bool newValue = Result.CreateFromJSON(webRequest.downloadHandler.text).isWatered;
                    isWater = newValue;
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    if (isWater) {
                        Debug.Log("Pflanze");
                        Debug.Log(flowerGrew);
                        // Add Script
                        if (!flowerGrew)
                        {
                            flowerGrew = true;
                            Debug.Log("aktivate water");

                            PlayerProgress.isWatered = true;
                        }
                    }
                    if (onChange != null && newValue != isWater)
                    {
                        
                        onChange.Invoke();
                        

                    }
                    break;
            }
        }
    }
}
[System.Serializable]
class Result
{
    public bool isWatered;

    public static Result CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Result>(jsonString);
    }
}
