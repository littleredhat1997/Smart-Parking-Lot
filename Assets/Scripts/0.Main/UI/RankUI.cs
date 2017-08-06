using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    [Header("按钮列表")]
    public Button[] rankBtns;
    [Header("排行列表")]
    public RankItemUI[] rankItemUI;

    void Start()
    {
        StartCoroutine("DownloadAsnc", 1);

        for (int i = 0; i < 9; ++i)
        {
            // 注册按钮事件 下载指定排行
            int index = i + 1;
            rankBtns[i].onClick.AddListener(delegate { StartCoroutine("DownloadAsnc", index); });
        }
    }

    // 下载排行
    private IEnumerator DownloadAsnc(int level)
    {
        WWWForm form = new WWWForm();
        form.AddField("level", level.ToString());

        WWW www = new WWW(Consts.URL_Download, form);

        // 等待www响应
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError(www.error);
        }
        else
        {
            // 反序列化
            var dict = MiniJSON.Json.Deserialize(www.text) as Dictionary<string, object>;

            // 数组下标
            int index = 0;

            // 防止排行榜为空报错
            if (dict != null)
            {
                foreach (object v in dict.Values)
                {
                    // 构造Data
                    Data data = new Data();
                    MiniJSON.Json.ToObject(data, v);

                    // Debug.Log(data.id + " " + data.username + " " + data.score);

                    // 显示UI
                    // rankItemUI[index].gameObject.SetActive(true);
                    rankItemUI[index].UpdateUI(data.username, data.score);
                    ++index;
                }
            }

            // 防止排行榜数量不足
            for (int i = index; i < rankItemUI.Length; ++i)
            {
                // rankItemUI[i].gameObject.SetActive(false);
                rankItemUI[i].UpdateUI("未知", 0);
            }
        }
    }
}
