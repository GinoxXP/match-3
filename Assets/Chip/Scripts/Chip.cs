using UnityEngine;
using Zenject;

public class Chip : MonoBehaviour
{
    protected PlayingField playingField;

    public Vector2Int PositionOnField { get; set; }

    public virtual void Click() { }

    [Inject]
    private void Init(PlayingField playingField)
    {
        this.playingField = playingField;
    }
}
