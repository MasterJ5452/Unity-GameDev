using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //translate laser up
        transform.Translate(Vector3.up *  _speed * Time.deltaTime);

        //if laser position is > 7.1 destroy object
        if(transform.position.y >= 7.1f)
        {
            Destroy(this.gameObject);
        }
    }
}
