using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiveWaypoint : MonoBehaviour
{
    private Image img;

    [System.NonSerialized]
    public bool off;

    public Transform hiveTransform;
    public Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        img = transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>();
        off = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!off)
        {
            float minX = img.GetPixelAdjustedRect().width / 2;
            float maxX = Screen.width - minX;

            float minY = img.GetPixelAdjustedRect().height / 2;
            float maxY = Screen.height - minY;

            Vector2 pos = Camera.main.WorldToScreenPoint(new Vector3(hiveTransform.position.x, hiveTransform.position.y + 1.8f, hiveTransform.position.z));

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            img.transform.parent.position = pos;

            Vector3 diff = cam.WorldToScreenPoint(hiveTransform.position) - img.transform.position;
            float angle = Mathf.Atan2(diff.y, diff.x);
            img.transform.parent.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
        }
        else
        {
            img.transform.parent.gameObject.SetActive(false);
        }
    }

    public void TurnOn()
    {
        img.transform.parent.gameObject.SetActive(true);
        off = false;
    }
}
