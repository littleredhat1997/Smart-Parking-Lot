using UnityEngine;

public class Globals : UnitySingleton<Globals>
{
    [Header("输入名字")]
    public GameObject inputPanel;

    public string username { get; set; }
    public int level { get; set; }

    void Start()
    {
        inputPanel.SetActive(true);
    }
}
