using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    public Transform player;
    private Vector3 playerPos;
    private Vector3 targetPos;
    public float offsetX = 0f;
    public float offsetY = 0f;
    public float speed = 5f;
    public float maxMagnitude = 10f;
    public float maxAngle = 1f;
    public float maxOffset = .5f;
    private float angleAdd;
    private float offsetAddX;
    private float offsetAddY;
    public float magnitude;

    public float screenShakeOption = .7f;

    public static float moveSpeed = 3.5f;
    public bool followPlayer = true;

    // Use this for initialization
    void Start()
    {
        playerPos = player.position;
        StartCoroutine(Shake());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPos = player.position;
        targetPos = new Vector3(playerPos.x + offsetX, playerPos.y + offsetY, -10);
        if (followPlayer) {
            transform.position = new Vector3(Vector3.Lerp(transform.position, targetPos, speed * Time.fixedDeltaTime).x, Vector3.Lerp(transform.position, targetPos, speed * 3 * Time.fixedDeltaTime).y, -10);
        } else {
            transform.position += Vector3.right * Time.deltaTime * moveSpeed;
            transform.position = new Vector3(transform.position.x, Vector3.Lerp(transform.position, targetPos, speed * 3 * Time.fixedDeltaTime).y, -10);
        }
    }
    IEnumerator Shake()
    {
        yield return new WaitForSeconds(.02f);
        if (magnitude > maxMagnitude) {
            magnitude = maxMagnitude;
        }
        if (magnitude > 0) {
            angleAdd = maxAngle * magnitude / 10 * Random.Range(-100f, 100f) / 100;
            offsetAddX = maxOffset * magnitude / 10 * Random.Range(-100f, 100f) / 100;
            offsetAddY = maxOffset * magnitude / 10 * Random.Range(-100f, 100f) / 100;
            transform.position += new Vector3(offsetAddX, offsetAddY) * screenShakeOption;
            transform.rotation = Quaternion.Euler(0f, 0f, (transform.rotation.z + angleAdd) * screenShakeOption);
            yield return new WaitForSeconds(.02f);
            transform.position -= new Vector3(offsetAddX, offsetAddY) * screenShakeOption;
            transform.rotation = Quaternion.Euler(0f, 0f, (transform.rotation.z - angleAdd) * screenShakeOption);
            magnitude -= .5f;
            if (magnitude < 0) {
                magnitude = 0;
            }
        }
        StartCoroutine(Shake());
    }

    public void push(Vector3 dir, float amt)
    {
        transform.position += dir * amt;
    }

    float time = .01f;
    public void TempPause(float time_)
    {
        time = time_;
        StartCoroutine(HitStop());
    }

    public IEnumerator HitStop()
    {
        Time.timeScale = .05f;
        yield return new WaitForSeconds(time * Time.timeScale);
        Time.timeScale = 1f;
    }

    public void SetMagnitude(float amt, bool setMode)
    {
        if (!setMode) {
            magnitude += amt;
        } else {
            magnitude = amt;
        }
    }
}
