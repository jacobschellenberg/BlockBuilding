using UnityEngine;
using System.Collections.Generic;

public class Block : MonoBehaviour
{
    public float HitPoints { get { return _hitPoints; } set { _hitPoints = value; } }
    public BlockType BlockType { get { return _blockType; } set { _blockType = value; } }
    public Vector3 Position { get { return _position; } set { _position = value; } }
    public List<string> Owners { get { return _owners; } set { _owners = value; } }

    [SerializeField] private float _hitPoints = 1;
    [SerializeField] private BlockType _blockType = BlockType.Dirt;
    [SerializeField] private Vector3 _position;
    [SerializeField] private List<string> _owners = new List<string>();

    public void Hit()
    {
        _hitPoints--;

        if (_hitPoints <= 0)
        {
            //WorldBuilder.blocks.Remove(this);
            //Debug.Log("BLOCKS: " + WorldBuilder.blocks.Count);
            Destroy(gameObject);
        }
    }
}
