using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Text _locationLabel;
    [SerializeField] private PlayerController _player;

    private void Start()
    {
        FocusMouse(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        _locationLabel.text = string.Format("Coordinates: ({0:0.00}, {1:0.00}, {2:0.00})", _player.transform.position.x, _player.transform.position.y, _player.transform.position.z);
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        FocusMouse(focusStatus);
    }

    private void FocusMouse(bool focusStatus)
    {
        StartCoroutine(_FocusMouse(focusStatus));
    }

    private IEnumerator _FocusMouse(bool focusStatus)
    {
        yield return new WaitForEndOfFrame();

        if (focusStatus)
        {
            Debug.Log("FOCUSED");
            yield return new WaitForSeconds(0.1f);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Debug.Log("LOST FOCUS");
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
