using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class FollowObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _child;
    [SerializeField]
    private Animator _fxhit;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float _heightDelta = 0.5f;
    private Vector3 targetPos;
    [SerializeField]
    private float _speed = 10f;
    private float delta;
    private void Start()
    {
        delta = target.transform.position.y + _heightDelta;
    }
    private void Update()
    {
        if (target)
        {
            targetPos = new Vector3(target.transform.position.x, target.transform.position.y + delta, target.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, _speed * Time.deltaTime);
            _fxhit.transform.rotation = Quaternion.LookRotation(target.transform.forward, target.transform.up);
            _fxhit.transform.position = new Vector3(targetPos.x, 0f, targetPos.z);
            if (_fxhit.IsInTransition(0))
            {
                _fxhit.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            _fxhit.gameObject.SetActive(true);
            _child.SetActive(false);
        }

    }
}
