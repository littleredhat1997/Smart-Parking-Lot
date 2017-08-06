using UnityEngine;

public class House : MonoBehaviour
{
    [Header("房子数组")]
    public GameObject[] houses;
    [Header("路面长度")]
    public int length;
    [Header("最大数量")]
    public int num;

    void Awake()
    {
        // 防止交界重叠
        length -= 1;
    }

    void Start()
    {
        // 随机数量
        int randomNum = Random.Range(1, num);
        int perLength = length / randomNum;
        for (int i = 0; i < randomNum; ++i)
        {
            // 随机位置
            int randomX = Random.Range(i * perLength, (i + 1) * perLength) - (length / 2);

            // 随机颜色
            int randomColor = Random.Range(0, houses.Length);

            // 生成房子
            GameObject house = Instantiate(houses[randomColor]);
            house.transform.SetParent(this.transform);
            house.transform.localPosition = new Vector3(randomX, 0, -4);
            house.transform.localEulerAngles = Vector3.zero;
            house.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }
    }
}
