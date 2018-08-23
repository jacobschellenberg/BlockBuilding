using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private string _playerName = "JealousKnight";
    [SerializeField] private float _checkDistance = 5;
    [SerializeField] private Block _blockPrefab;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SetBlock();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            HitBlock();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.position = new Vector3(0, 10, 0);
        }
    }

    private void SetBlock()
    {
        StartCoroutine(_SetBlock());
    }

    private void HitBlock()
    {
        StartCoroutine(_HitBlock());
    }

    private IEnumerator _SetBlock()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _checkDistance))
        {
            Debug.DrawLine(ray.origin, hit.point);
            //			Debug.Log ("Normal: "  + hit.normal);

            if (hit.transform.GetComponent<Block>().BlockType == BlockType.Dirt)
            {
                if (hit.normal.x > 0)
                {
                    //				Debug.Log("RIGHT");
                    if (!Physics.CheckSphere(new Vector3(hit.collider.bounds.center.x + 1, hit.collider.bounds.center.y, hit.collider.bounds.center.z), 0.2f))
                    {
                        GameObject block = (GameObject)Instantiate(_blockPrefab.gameObject, new Vector3(hit.collider.bounds.center.x + 1, hit.collider.bounds.center.y, hit.collider.bounds.center.z), Quaternion.identity);
                        block.GetComponent<Block>().Owners.Add(_playerName);
                        WorldBuilder.blocks.Add(block.GetComponent<Block>());
                    }
                }
                else if (hit.normal.x < 0)
                {
                    //				Debug.Log("LEFT");
                    if (!Physics.CheckSphere(new Vector3(hit.collider.bounds.center.x - 1, hit.collider.bounds.center.y, hit.collider.bounds.center.z), 0.2f))
                    {
                        GameObject block = (GameObject)Instantiate(_blockPrefab.gameObject, new Vector3(hit.collider.bounds.center.x - 1, hit.collider.bounds.center.y, hit.collider.bounds.center.z), Quaternion.identity);
                        block.GetComponent<Block>().Owners.Add(_playerName);
                        WorldBuilder.blocks.Add(block.GetComponent<Block>());
                    }
                }
                else if (hit.normal.y > 0)
                {
                    //				Debug.Log ("TOP");
                    if (!Physics.CheckSphere(new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y + 1, hit.collider.bounds.center.z), 0.2f))
                    {
                        GameObject block = (GameObject)Instantiate(_blockPrefab.gameObject, new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y + 1, hit.collider.bounds.center.z), Quaternion.identity);
                        block.GetComponent<Block>().Owners.Add(_playerName);
                        WorldBuilder.blocks.Add(block.GetComponent<Block>());
                    }
                }
                else if (hit.normal.y < 0)
                {
                    //				Debug.Log ("BOTTOM");
                    if (!Physics.CheckSphere(new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y - 1, hit.collider.bounds.center.z), 0.2f))
                    {
                        GameObject block = (GameObject)Instantiate(_blockPrefab.gameObject, new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y - 1, hit.collider.bounds.center.z), Quaternion.identity);
                        block.GetComponent<Block>().Owners.Add(_playerName);
                        WorldBuilder.blocks.Add(block.GetComponent<Block>());
                    }
                }
                else if (hit.normal.z > 0)
                {
                    //				Debug.Log ("FRONT");
                    if (!Physics.CheckSphere(new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y, hit.collider.bounds.center.z + 1), 0.2f))
                    {
                        GameObject block = (GameObject)Instantiate(_blockPrefab.gameObject, new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y, hit.collider.bounds.center.z + 1), Quaternion.identity);
                        block.GetComponent<Block>().Owners.Add(_playerName);
                        WorldBuilder.blocks.Add(block.GetComponent<Block>());
                    }
                }
                else if (hit.normal.z < 0)
                {
                    //				Debug.Log ("BACK");
                    if (!Physics.CheckSphere(new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y, hit.collider.bounds.center.z - 1), 0.2f))
                    {
                        GameObject block = (GameObject)Instantiate(_blockPrefab.gameObject, new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y, hit.collider.bounds.center.z - 1), Quaternion.identity);
                        block.GetComponent<Block>().Owners.Add(_playerName);
                        WorldBuilder.blocks.Add(block.GetComponent<Block>());
                    }
                }
            }
        }

        yield return null;
    }

    private IEnumerator _HitBlock()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _checkDistance))
        {
            Debug.DrawLine(ray.origin, hit.point);
            //		Debug.Log (hit.transform.name);

            if (hit.transform.GetComponent<Block>() != null)
            {
                if (hit.transform.GetComponent<Block>().Owners != null)
                {
                    if (hit.transform.GetComponent<Block>().Owners.Contains(_playerName))
                    {
                        if (hit.transform.GetComponent<Block>().BlockType == BlockType.Dirt)
                        {
                            hit.transform.GetComponent<Block>().Hit();
                        }
                    }
                }
            }
        }

        yield return null;
    }
}
