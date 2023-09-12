using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.Asteroids;

public class Chip : MonoBehaviour
{
    private static readonly float animationTime = 0.2f;
    [SerializeField]
    protected int id;

    protected SwapChips swapChips;
    protected PlayingField playingField;
    protected Score score;

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

    public void Destroy()
    {
        playingField.Field[PositionOnField.x, PositionOnField.y] = null;
        Destroy(gameObject);
    }

    public void Move(Vector2 position)
    {
        transform.DOMove(position, animationTime);
    }

    protected void DestroyChips(List<Chip> chips)
    {
        for (int i = chips.Count - 1; i >= 0; i--)
        {
            var chip = chips[i];
            playingField.Field[chip.PositionOnField.x, chip.PositionOnField.y] = null;
            chip.Destroy();
        }

        score.CommitDestroyedChipsCount(chips.Count + 1);

        playingField.Field[PositionOnField.x, PositionOnField.y] = null;
        playingField.UpdateField();

        Destroy(gameObject);
    }

    [Inject]
    private void Init(
        SwapChips swapChips,
        PlayingField playingField,
        Score score)
    {
        this.swapChips = swapChips;
        this.playingField = playingField;
        this.score = score;
    }
}
