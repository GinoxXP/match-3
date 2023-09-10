using DG.Tweening;
using UnityEngine;
using Zenject;

public class Chip : MonoBehaviour
{
    private static readonly float animationTime = 0.2f;
    [SerializeField]
    protected int id;

    protected SwapChips swapChips;

    public Vector2Int PositionOnField { get; set; }

    public bool IsSelected { get; protected set; }

    public int ID => id;

    public virtual void Click()
    {
        if (IsSelected)
            Deselect();
        else
            Select();
    }

    public void Select()
    {
        transform.DOScale(1.2f, animationTime);
    }

    public void Deselect()
    {
        transform.DOScale(1f, animationTime);
    }

    public void Cancel()
    {
        transform
            .DOShakeScale(animationTime)
            .OnKill(() => Deselect());
    }

    public virtual bool IsConfirm() { return true; }

    [Inject]
    private void Init(SwapChips swapChips)
    {
        this.swapChips = swapChips;
    }
}
