using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int correctNumber = 0;
    public int loopCount = 0;

    public int MAX_LOOP_COUNT = 100;
    // Start is called before the first frame update
    void Start()
    {
        MoveToNearestMapCell();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            loopCount = 0;
            SearchCorrectMapCell();
        }
    }

    // プレイヤの現在位置が正しいマップなのか確認する.
    // 正しくなかったら、次の位置に移動する.
    void SearchCorrectMapCell()
    {
        // 終了条件を設定する.
        // 終了条件:成功.
        MapCell nearestMapCell = GetNearestMapCell();
        Debug.Log("今の地点の値:" + nearestMapCell.cellNumber.ToString());

        if (nearestMapCell.cellNumber == correctNumber)
        {
            Debug.Log("正解の場所に着きました.");

            return;
        }
        // 終了条件:失敗.
        if (loopCount >= MAX_LOOP_COUNT)
        {
            Debug.Log("規定回数以上に再帰したため、ループを終了します.");
            return;
        }

        // 次の再帰に進むために、条件を変更する.
        RectTransform playerRectTransform = GetComponent<RectTransform>();
        Vector3 playerPosition = playerRectTransform.anchoredPosition;
        playerRectTransform.anchoredPosition = new Vector3(playerPosition.x + 300.0f, playerPosition.y, playerPosition.z);
        MoveToNearestMapCell();
        loopCount++;

        // 関数の再帰呼び出しをする.
        SearchCorrectMapCell();
    }

    // 最も近いMapCellを取得する.
    MapCell GetNearestMapCell()
    {
        MapCell returnMapCell = null;

        GameObject mapParent = GameObject.Find("Map");
        float minDistance = 10000.0f;

        for (int i = 0; i < mapParent.transform.childCount; i++)
        {
            Transform mapCell = mapParent.transform.GetChild(i);
            RectTransform mapCellRectTransform = mapCell.GetComponent<RectTransform>();
            Vector3 mapCellPosition = mapCellRectTransform.anchoredPosition;

            RectTransform playerRectTransform = GetComponent<RectTransform>();
            Vector3 playerPosition = playerRectTransform.anchoredPosition;
            Vector3 diffVector = playerPosition - mapCellPosition;
            float distance = diffVector.magnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                returnMapCell = mapCell.GetComponent<MapCell>();
            }
        }
        return returnMapCell;
    }

    // 最も近いMapCellへ移動する.
    void MoveToNearestMapCell()
    {
        MapCell nearestMapCell = GetNearestMapCell();
        RectTransform mapCellRectTransform = nearestMapCell.transform.GetComponent<RectTransform>();

        RectTransform playerRectTransform = GetComponent<RectTransform>();

        playerRectTransform.anchoredPosition = mapCellRectTransform.anchoredPosition;
    }
}
