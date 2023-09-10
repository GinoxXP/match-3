using UnityEngine;
using UnityEngine.EventSystems;

public class RenderTextureToWorld : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Camera gridCamera;

    private RectTransform textureRectTransform;

    private void Awake()
    {
        textureRectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(textureRectTransform, eventData.position, null, out Vector2 localClick);
        localClick.x = (textureRectTransform.rect.xMin * -1) - (localClick.x * -1);
        localClick.y = (textureRectTransform.rect.yMin * -1) - (localClick.y * -1);

        var viewportClick = new Vector2(localClick.x / textureRectTransform.rect.size.x, localClick.y / textureRectTransform.rect.size.y);

        var layer = LayerMask.GetMask("Region");

        var ray = gridCamera.ViewportPointToRay(new Vector3(viewportClick.x, viewportClick.y, 0));
        Debug.DrawRay(ray.origin, ray.direction, Color.green, 10);

        var raycast = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layer);
        if (raycast && raycast.transform.TryGetComponent<Chip>(out var chip))
            chip.Click();
    }
}

