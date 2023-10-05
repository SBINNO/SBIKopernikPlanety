using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectActionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Transform parent;
    public GameObject gazingLight;

    private void Start()
    {
        parent = transform.parent;
        if (gazingLight != null )
        {
            InitializeLight();
        }
    }


    private void InitializeLight()
    {
        gazingLight = Instantiate(gazingLight, transform.position + Vector3.up, Quaternion.identity, parent);
        gazingLight.SetActive(false);
    }

    private void ChangeLight(bool gazedAt)
    {
        if (gazingLight != null)
        {
            if (gazedAt)
            {
                gazingLight.SetActive(true);
            }
            else
            {
                gazingLight.SetActive(false);
            }
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {

    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        ChangeLight(true);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        ChangeLight(false);
    }
}
