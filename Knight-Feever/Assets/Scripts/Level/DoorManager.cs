using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    public Tilemap doorTilemap;
    public Collider2D doorCollider;
    public Tile OpenDoorTile;
    public Tile ClosedDoorTile;
    private void Update() {
        if(!playerData.inRoom)
        {
            doorCollider.isTrigger=true;
            for(int Tile = doorTilemap.cellBounds.xMin; Tile<doorTilemap.cellBounds.xMax; Tile++)
            {
                doorTilemap.SetTile(new Vector3Int(Tile,doorTilemap.cellBounds.yMin,0),OpenDoorTile);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {

        if (other.gameObject.tag == "Player") {
            doorCollider.isTrigger = false;
            for(int Tile = doorTilemap.cellBounds.xMin; Tile<doorTilemap.cellBounds.xMax; Tile++)
            {
                doorTilemap.SetTile(new Vector3Int(Tile,doorTilemap.cellBounds.yMin,0),ClosedDoorTile);
            }
            playerData.inRoom = true;
        }
    }
}
