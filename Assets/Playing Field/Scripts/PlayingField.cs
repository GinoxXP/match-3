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

    private void Fill()
    {
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                var chipObject = container.InstantiatePrefab(GetRandomChip(), grid);

                chipObject.transform.position = new Vector2(w + w * gap.x, h + h * gap.y) + startPoint;

                var chip = chipObject.GetComponent<Chip>();
                chip.PositionOnField = new Vector2Int(w, h);
                Field[w, h] = chip;
            }
        }
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
