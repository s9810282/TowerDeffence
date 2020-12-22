using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSave : MonoBehaviour
{
    [SerializeField] string fileName;

    [SerializeField] Tilemap tileMap;
    [SerializeField] Tilemap wallTileMap;

    [SerializeField] Tile[] tiles;

    string path;

    List<Dictionary<string, object>> saveData = new List<Dictionary<string, object>>();

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/Resources/MapData/";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveCSVData()
    {
        //Debug.Log(clickPos); //타일맵 중심을 기준으로 0,0  한칸당 1씩 = tile의 사이즈가 1, 1이라서 그런듯
        //Debug.Log(tileMap.GetTile(clickPos)); //
        //Debug.Log(tileMap.cellSize);  //타일 크기 (1,1,0)
        //Debug.Log(tileMap.size);      //타일맵 x*y 크기 알려줌 Vector3

        Vector3 size = tileMap.size;
        Vector3Int cellPos;


        using (var writer = new CSVFileWriter(path + fileName + ".csv"))
        {
            List<string> columns = new List<string>();

            for(int i = 0; i < size.x; i++)
            {
                columns.Add("_" + i);
            }

            writer.WriteRow(columns);
            columns.Clear();

            string type;

            //ㄴPath Tower, Wall 구분해야함 3레이어
            
            for (int i = ((int)size.y / 2 - 1); i > -(size.y / 2) - 1; i--) //중앙을 기준으로 위
            {
                //j = -18 / 2 = -9               -9 < 9             9++
                for (int j = -(int)size.x / 2; j < size.x / 2; j++) //중앙을 기준으로 오른쪽
                {
                    Debug.Log($"Pos : {j}, {i} " + tileMap.GetTile(new Vector3Int(j, i, 0)).name);
                    columns.Add(tileMap.GetTile(new Vector3Int(j, i, 0)).name.ToString());
                }

                writer.WriteRow(columns); //줄바꿈
                columns.Clear();
            }

            writer.WriteRow(columns); //줄바꿈
            columns.Clear();


            //Wall tileMap
            for (int i = ((int)size.y / 2 - 1); i > -(size.y / 2) - 1; i--) //중앙을 기준으로 위
            {
                //j = -18 / 2 = -9               -9 < 9             9++
                for (int j = -(int)size.x / 2; j < size.x / 2; j++) //중앙을 기준으로 오른쪽
                {
                    Debug.Log($"Pos : {j}, {i} " + wallTileMap.GetTile(new Vector3Int(j, i, 0)).name);
                    columns.Add(wallTileMap.GetTile(new Vector3Int(j, i, 0)).name.ToString());
                }

                writer.WriteRow(columns); //줄바꿈
                columns.Clear();
            }

        }
    }








    public void LoadCSVData()
    {
        if (File.Exists(path + fileName + ".csv"))
        {
            saveData = new List<Dictionary<string, object>>();
            saveData = CSVReader.Read("MapData/" + fileName);
        }

        ApplyTileMapToData();
    }



    public void ApplyTileMapToData()
    {
        Vector3 size = tileMap.size;
        Vector3Int cellPos;

        Tile changeTile = tiles[0];

        Debug.Log(size);

        //4 ~ -6
        for (int i = ((int)size.y / 2 - 1); i > -(size.y / 2) - 1; i--) //  Ground (AStarTile) yyyyyy
        {
            //j = -18 / 2 = -9               -9 < 9             9++
            for (int j = -(int)size.x / 2; j < size.x / 2; j++) // xxxxxxxx
            {
                Debug.Log($"Pos : {j}, {i} " + tileMap.GetTile(new Vector3Int(j, i, 0)).name);
                cellPos = new Vector3Int(j, i, 0);

                switch(saveData[Mathf.Abs(i - 4)]["_" + (j + 9)]) //0 ~ 17
                {
                    case "Ground":
                        changeTile = tiles[0];
                        break;

                    case "Start":
                        changeTile = tiles[1];
                        break;

                    case "End":
                        changeTile = tiles[2];
                        break;

                    case "Tower":
                        changeTile = tiles[3];
                        break;

                    case "Path":
                        changeTile = tiles[4];
                        break;
                }

                tileMap.SetTile(cellPos, changeTile);               
            }

        }
    }
}
