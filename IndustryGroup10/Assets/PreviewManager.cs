using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class PreviewManager : MonoBehaviour
{
    public string token =
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJodHRwczovL2FwcHNlbWJsZS5hcHAiLCJpYXQiOjE2NTM0ODA0MTQsImlzcyI6Imh0dHBzOi8vYXBwc2VtYmxlLmFwcCIsInN1YiI6ImNmZTZmNDMzLWI2M2ItNGQ0Yy04ZDcwLTFiZWQwMDQwYjBiOSIsImV4cCI6MTY1MzQ4NDAxNH0.8Ukokk89KFxQE-yd9dh1wfU_CCxj7VlwEhkKkvHV4Y0";

    public RawImage loadingTexture;
    private RawImage image;

    private string uri =
        "https://shot.screenshotapi.net/screenshot?&url=https%3A%2F%2Faaaa.aaaa.appsemble.app%2Fen%2Fexample-page-a&width=360&height=600&output=image&file_type=png&wait_for_event=load";

    void Awake()
    {
        image = GetComponentInChildren<RawImage>();
        image.color = new Color(1, 1, 1);
        //loadingTexture.enabled = false;
        StartCoroutine(RefreshPreview(uri));
        /*StartCoroutine(PatchCode(
            @"name: aaaa
description: Hello whats up
defaultPage: Example Page A

pages:
  - name: Example Page A
    blocks:
      - type: tiles
        version: 0.20.5
        parameters:
          text: Hello, World!
          icon: arrow-right",
            "body { background-color: violet; }",
            " "
        ));*/
    }

    public IEnumerator PatchCode(string code, string coreStyle, string sharedStyle)
    {
        Debug.Log("Patching new code");
        var data = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("yaml", code),
            new MultipartFormDataSection("coreStyle", coreStyle),
            new MultipartFormDataSection("sharedStyle", sharedStyle)
        };

        var boundary = UnityWebRequest.GenerateBoundary();
        var formSections = UnityWebRequest.SerializeFormSections(data, boundary);

        using UnityWebRequest request = new UnityWebRequest("https://appsemble.app/api/apps/295", "PATCH");
        request.SetRequestHeader("Content-Type",
            "multipart/form-data; boundary=\"" + System.Text.Encoding.UTF8.GetString(boundary) + "\"");
        request.SetRequestHeader("Authorization",
            $"Bearer {token}");

        request.uploadHandler = new UploadHandlerRaw(formSections);
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        switch (request.result)
        {
            case UnityWebRequest.Result.Success:
                Debug.Log("Patched new code! Refreshing preview.");
                StartCoroutine(RefreshPreview(uri));
                break;
            default:
                Debug.Log(request.error);
                Debug.Log(request.downloadHandler.error);
                break;
        }
    }

    private IEnumerator RefreshPreview(string uri)
    {
        loadingTexture.enabled = true;
        using UnityWebRequest request = UnityWebRequestTexture.GetTexture(uri);
        yield return request.SendWebRequest();

        loadingTexture.enabled = false;
        switch (request.result)
        {
            case UnityWebRequest.Result.Success:
                var texture = DownloadHandlerTexture.GetContent(request);
                if (texture == null)
                    throw new InvalidOperationException();

                image.texture = texture;
                Debug.Log("Updated preview!");
                break;
            default:
                Debug.Log(request.error);
                Debug.Log(request.downloadHandler.error);
                throw new ArgumentOutOfRangeException();
        }
    }
}