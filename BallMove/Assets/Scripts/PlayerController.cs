using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings Player")]
    [Space]
    [SerializeField] private float speedMove;
    [SerializeField] private GameObject effectGiveCoin;
    [SerializeField] private GameObject effectDead;

    private LineRenderer lineTrajectory;

    private List<Vector3> positionsPath = new List<Vector3>(); //Stores waypoints
    private int pathPoint; //Next point path
    private float distance = 0.2f; //Minimum distance to the next point
    void Start()
    {
        lineTrajectory = GetComponent<LineRenderer>();

        positionsPath.Add(transform.position);
        pathPoint = positionsPath.Count;
    }

    void Update()
    {
        if (GameManager.Instance.IsGame)
            MovePlayer();
    }

    private void MovePlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            positionsPath.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            DrawPath();
        }

        if (positionsPath.Count > 1)
        {
            transform.position = Vector3.Lerp(transform.position, positionsPath[pathPoint], speedMove * Time.deltaTime);

            if (Vector3.Distance(transform.position, positionsPath[pathPoint]) <= distance)
            {
                pathPoint++;

                if (pathPoint >= positionsPath.Count)
                {
                    ResetPath();
                }
            }
        }
    }

    public void DrawPath()
    {
        lineTrajectory.positionCount = positionsPath.Count;

        for (int x = 0; x < positionsPath.Count; x++)
        {
            lineTrajectory.SetPosition(x, positionsPath[x]);
        }
    }

    private void ResetPath()
    {
        positionsPath.Clear();
        positionsPath.Add(transform.position);

        lineTrajectory.positionCount = positionsPath.Count;
        pathPoint = positionsPath.Count;
    }

    public void EffectActivation(GameObject effect)
    {
        GameObject obj = Instantiate(effect);
        obj.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstracle"))
        {
            EffectActivation(effectDead);
            GameManager.Instance.ResultGame(false);

            Destroy(gameObject);
        }

        if (collision.CompareTag("Coin"))
        {
            EffectActivation(effectGiveCoin);
            GameManager.Instance.UpdateCoins();

            Destroy(collision.gameObject);
        }
    }
}
