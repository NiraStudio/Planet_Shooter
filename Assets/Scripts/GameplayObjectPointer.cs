using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayObjectPointer : MonoBehaviour {
    public Image enemyPointerLeftImage, enemyPointerRightImage;
    public Text enemyPointerLeftText, enemyPointerRightText;

    public List<GameObject> enemies;
    public Collider2D[] temp;
    GamePlayManager GPM;
    public int leftEnemies, rightEnemies;

    float tt;
    Vector2 size,t;
    // Use this for initialization
    void Start () {
        GPM = GamePlayManager.GPM;
	}
	
	// Update is called once per frame
	void Update () {
        
        Camera cam = Camera.main;
        size.y = 2f * cam.orthographicSize;
        size.x = size.y * cam.aspect;

        temp = Physics2D.OverlapBoxAll(cam.transform.position, size, cam.transform.eulerAngles.z,MainBehavior.EnemyLayer);
        enemies = new List<GameObject>();
        foreach (var item in GPM.enemies.ToArray())
        {
            if (!Contains(item)&&item.activeInHierarchy)
                enemies.Add(item);

        }
        rightEnemies = 0;
        leftEnemies = 0;
        foreach (var enemy in enemies.ToArray())
        {
            t = enemy.transform.position - transform.position;
            tt = Vector2.Dot(t, transform.right);
            if (tt > 0)
                rightEnemies++;
            else
                leftEnemies++;
        }

        enemyPointerLeftImage.gameObject.SetActive(leftEnemies > 0);
        enemyPointerRightImage.gameObject.SetActive(rightEnemies > 0);

        enemyPointerRightText.text = rightEnemies.ToString();
        enemyPointerLeftText.text = leftEnemies.ToString();
    }


    bool Contains(GameObject go)
    {
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].gameObject == go)
            {
                return true;

            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        DrawCube(Camera.main.transform.position, Camera.main.transform.rotation, size);
    }
    public static void DrawCube(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Matrix4x4 cubeTransform = Matrix4x4.TRS(position, rotation, scale);
        Matrix4x4 oldGizmosMatrix = Gizmos.matrix;

        Gizmos.matrix *= cubeTransform;

        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

        Gizmos.matrix = oldGizmosMatrix;
    }
}
