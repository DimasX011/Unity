using UnityEngine;

public class Rotatecamera : MonoBehaviour
{
    public float speed = 5f;
    private Transform _rorator;

    private void Start()
    {
        _rorator = GetComponent<Transform>();
    }

    private void Update()
    {
        _rorator.Rotate(0, speed * Time.deltaTime, 0);
    }

}
