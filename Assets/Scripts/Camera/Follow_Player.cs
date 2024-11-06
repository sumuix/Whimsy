using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Player : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector2 camera_Offset;
    private Vector2 threshold;
    [SerializeField] float speed = 3f;
    [SerializeField] float x_Min, y_Min;
    [SerializeField] float x_Max, y_Max;

    private Rigidbody2D rb;


    void Start()
    {
        threshold = calculate_Threshold();
        rb = player.GetComponent<Rigidbody2D>();
    }


    private void LateUpdate()
    {
        Vector2 follow = player.transform.position;
        float xDiff = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDiff = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPos = transform.position;
        if (Mathf.Abs(xDiff) >= threshold.x)
            newPos.x = follow.x;
        if (Mathf.Abs(yDiff) >= threshold.y)
            newPos.y = follow.y;

        float X_clamp, Y_clamp;

        X_clamp = Mathf.Clamp(newPos.x, x_Min, x_Max);

        Y_clamp = Mathf.Clamp(newPos.y + camera_Offset.y, y_Min, y_Max);

        Vector3 clamped_Camera_Pos = new Vector3(X_clamp, Y_clamp, newPos.z);

        // float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;

        //transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, clamped_Camera_Pos, speed * Time.deltaTime);
    }

    private Vector3 calculate_Threshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 temp = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height,
                        Camera.main.orthographicSize);

        temp.x -= camera_Offset.x;
        temp.y -= camera_Offset.y;
        return temp;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = calculate_Threshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}
