using System.Collections;
using UnityEngine;

public class Data
{
    public int id;
    public string username;
    public int score;
}

public class ServerMgr : UnitySingleton<ServerMgr>
{

    // 向服务端上传数据
    public void Upload(string username, int level, int score)
    {
        object[] obj = { username, level, score };
        StartCoroutine("UploadAsnc", obj);
    }

    ////////////////////////////////////////

    // 上传数据
    private IEnumerator UploadAsnc(object[] obj)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", obj[0].ToString());
        form.AddField("level", obj[1].ToString());
        form.AddField("score", obj[2].ToString());

        WWW www = new WWW(Consts.URL_Upload, form);

        // 等待www响应
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError(www.error);
        }
    }
}
