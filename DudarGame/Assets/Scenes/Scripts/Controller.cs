using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private CubePos nowCube = new CubePos(0, 1, 0);
    public float ChangeSpeers = 0.5f;
    public Transform Cubetoplace;

    public GameObject CubeCreate, allcubes;
    private Rigidbody allcubesrb;

    private List<Vector3> allcubeposition = new List<Vector3>
    {
        new Vector3(0, 0, 0),
        new Vector3(1, 0, 0),
        new Vector3(-1, 0, 0),
        new Vector3(0, 1, 0),
        new Vector3(0, 0, 1),
        new Vector3(0, 0, -1),
        new Vector3(1, 0, 1),
        new Vector3(-1, 0, -1),
        new Vector3(-1, 0, 1),
        new Vector3(1, 0, -1),
    };

    private void Start()
    {
        allcubesrb = allcubesrb.GetComponent<Rigidbody>();
        StartCoroutine(StartCoroutine());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
#if  !UNITY_EDITOR
            if (Input.GetTouch(0).phase != TouchPhase.Began)
                return;
#endif
            GameObject newcube = Instantiate(
                 CubeCreate, Cubetoplace.position, Quaternion.identity) as GameObject;
            newcube.transform.SetParent(allcubes.transform);
            nowCube.setVector(Cubetoplace.position);
            allcubeposition.Add(nowCube.GetVector());
            allcubesrb.isKinematic = true;
            allcubesrb.isKinematic = false;
            SpawnPositions();
        }
    }

    IEnumerator StartCoroutine()
    {
        while (true)
        {
            SpawnPositions();

            yield return new WaitForSeconds(ChangeSpeers);
        }
    }
    private void SpawnPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        if(IsPositionEmpty(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z)) && nowCube.x + 1 != Cubetoplace.position.x)
        {
            positions.Add(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z));
        } 
         if (IsPositionEmpty(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z)) && nowCube.x - 1 != Cubetoplace.position.x)
        {
            positions.Add(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z));
        }
         if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z)) && nowCube.y + 1 != Cubetoplace.position.y)
        {
            positions.Add(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z));
        }
         if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z)) && nowCube.y - 1 != Cubetoplace.position.y)
        {
            positions.Add(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z));
        }
         if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1)) && nowCube.z + 1 != Cubetoplace.position.z)
        {
            positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1));
        }
         if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1)) && nowCube.z - 1 != Cubetoplace.position.z)
        {
            positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1));
        }

        Cubetoplace.position = positions[UnityEngine.Random.Range(0, positions.Count)];
    }
    private bool IsPositionEmpty(Vector3 targetPos)
    {
        if(targetPos.y == 0)
        {
            return false;

            foreach(Vector3 pos in allcubeposition)
            {
                if(pos.x == targetPos.x && pos.y == targetPos.y && pos.z == targetPos.z)
                {
                    return false;
                }
                
            }

        }
        return true;
    }


}

struct CubePos
{
    public int x, y, z;

    public CubePos(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3 GetVector()
    {
        return new Vector3(x, y, z);
    }

    public void setVector(Vector3 pos)
    {
        x = Convert.ToInt32(pos.x);
        y = Convert.ToInt32(pos.y);
        z = Convert.ToInt32(pos.z);
    }
}
