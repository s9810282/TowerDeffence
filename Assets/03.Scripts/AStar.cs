using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public enum TileType { START, END, TOWER, PATH, GROUND }

public class AStar : MonoBehaviour
{
    [SerializeField] TileType tileType;

    [SerializeField] Tilemap tileMap;
    [SerializeField] Tile[] tiles;

    [SerializeField] Camera camera;
    [SerializeField] LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit.collider != null)
            {
                Vector3 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int clickPos = tileMap.WorldToCell(mouseWorldPos);

                ChangeTile(clickPos);
            }
        }
    }

    public void ChangeTileType(TileButton button)
    {
        tileType = button.MyTileType;
    }

    public void ChangeTile(Vector3Int clickPos)
    {
 
        tileMap.SetTile(clickPos, tiles[(int)tileType]);
        //Debug.Log(clickPos); //타일맵 중심을 기준으로 0,0  한칸당 1씩 = tile의 사이즈가 1, 1이라서 그런듯
        //Debug.Log(tileMap.GetTile(clickPos)); //
        //Log(tileMap.cellSize);  //타일 크기 (1,1,0)
        //Debug.Log(tileMap.size);      //타일맵 x*y 크기 알려줌 Vector3

        //아 시발 내일 할거  맵 저장 및 로드
        //만들때 까지 잘 생각마라 시발람아.

        //만드는 방법 or 가이드

        //1. 타일맵을 for문 돌려서 다 가져옴, 그리고 차례대로 csv든 txt든 박아넣어 
        //와 시발 저장 다했네?


        //2. 로드
        //텍스트를 긁어와서 가로세로 for문 돌리는데 vector3 값 x값을 1씩 늘려가며 csv혹은 txt를 하나씩 읽어와서 그에 따라 Cell을 바꿔준다.
    }
}
