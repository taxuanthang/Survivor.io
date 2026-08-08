using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    [SerializeField] TilemapCollider2D _collider2D;
    [SerializeField] TilemapRenderer _renderer;

    public Room room;
    public bool isClosed = false;

    public void Awake()
    {
        Open();
        EventManager.instance.OnFinishEnemyRoom.AddListener(Open);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((UtilitiesManager.instance.playerLayer.value &(1 << collision.gameObject.layer )) !=0) 
        {
             PlayerManager player = collision.GetComponent<PlayerManager>();
            room.OnPlayerCrossDoor(player);
        }
    }

    public void Open()
    {
        // Implement door opening logic here
        isClosed = false;
        _collider2D.isTrigger = true;
        _renderer.enabled = false;
    }
    public void Close()
    {
        isClosed = true;
        _collider2D.isTrigger = false;
        _renderer.enabled = true;
    }


}
