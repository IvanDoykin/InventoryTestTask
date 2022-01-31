using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class ItemInfoSender : MonoBehaviour
{
    private const string url = @"https://dev4ops.elysium.today/inventory/status";
    private const string authToken = @"BMeHG5xqJeB4qCjpuJCTQLsqNGaqkfB6";

    private void Start()
    {
        BackpackPlace.BackpackPlaceSet.AddListener(SendItemInfo);
        BackpackPlace.BackpackPlaceRemove.AddListener(SendItemInfo);
    }

    private void OnDestroy()
    {
        BackpackPlace.BackpackPlaceSet.RemoveListener(SendItemInfo);
        BackpackPlace.BackpackPlaceRemove.RemoveListener(SendItemInfo);
    }

    // Каждый ивент складывания/доставания предмета из/в "рюкзак" отправляется запрос на сервер, с идентификатором предмета и его событием. -
    // - that sound one task. What's mean "с идентификатором предмета и его СОБЫТИЕМ" ?
    // I don't understand, so i send Id and Name of object.

    private void SendItemInfo(ItemInfo info)
    {
        StartCoroutine(PostRequest(info));
    }

    private IEnumerator PostRequest(ItemInfo info)
    {
        WWWForm form = new WWWForm();
        form.headers.Add("auth", authToken);
        form.AddField("Id", info.Id);
        form.AddField("Name", info.Name);

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + request.error);
        }
        else
        {
            Debug.Log("Received: " + request.downloadHandler.text);
        }
    }
}
