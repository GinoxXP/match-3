using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayingField : MonoBehaviour
{
    private DiContainer container;

    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [Space]
    [SerializeField]
    private Vector2 startPoint;
    [SerializeField]
    private Vector2 gap;
    [Space]
    [SerializeField]
    private Transform grid;
    [Space]
    [SerializeField]
    private GameObject[] chipsPrefab;

    public Chip[,] Field { get; private set; }

    public int? Seed { get; set; }

    public bool Swap(Chip chip1, Chip chip2)
    {
        if (!IsNeighbour(chip1, chip2))
            return false;

        Field[chip1.PositionOnField.x, chip1.PositionOnField.y] = chip2;
        Field[chip2.PositionOnField.x, chip2.PositionOnField.y] = chip1;

        var bufferChip1Position = chip1.PositionOnField;

        chip1.PositionOnField = chip2.PositionOnField;
        chip2.PositionOnField = bufferChip1Position;

        return true;
    }

    public bool IsNeighbour(Chip chip1, Chip chip2)
    {
        var distance = Vector2.Distance(chip1.PositionOnField, chip2.PositionOnField);
        return distance == 1;
    }

    public void UpdateField()
    {
        for (int x = 0; x < width; x++)
            MoveColumn(x);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (Field[x, y] == null)
                {
                    InsertChip(GetRandomChip(), x, y);
                }
            }
        }
    }

    private void MoveColumn(int column)
    {
        var y = height - 1;
        while (y >= 0)
        {
            if (Field[column, y] == null)
            {
                var buffer = Field[column, y];
                Field[column, y] = Field[column, y + 1];
                Field[column, y + 1] = buffer;

                var chip = Field[column, y];

                if (chip == null)
                    break;

                chip.PositionOnField = new Vector2Int(column, y);
                chip.Move(new Vector2(chip.PositionOnField.x + chip.PositionOnField.x * gap.x, chip.PositionOnField.y + chip.PositionOnField.y * gap.y) + startPoint);

                y = height - 1;
            }

            y--;
        }
    }

    private void Fill()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                InsertChip(GetRandomChip(), x, y);
            }
        }
    }

    private void InsertChip(GameObject randomChip, int x, int y)
    {
        var chipObject = container.InstantiatePrefab(randomChip, grid);

        chipObject.transform.position = new Vector2(x + x * gap.x, y + y * gap.y) + startPoint;

        var chip = chipObject.GetComponent<Chip>();
        chip.PositionOnField = new Vector2Int(x, y);
        Field[x, y] = chip;
    }

    private GameObject GetRandomChip()
    {
        var chip = chipsPrefab[Random.Range(0, chipsPrefab.Length)];
        return chip;
    }

    private void Start()
    {
        if (Seed.HasValue)
            Random.InitState(Seed.Value);

        Field = new Chip[width, height];

        Fill();
    }

    [Inject]
    private void Init(DiContainer container)
    {
        this.container = container;
    }
}
