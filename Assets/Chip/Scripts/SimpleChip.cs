using System.Collections.Generic;
using UnityEngine;

public class SimpleChip : Chip
{
    public override void Click()
    {
        if (swapChips.SetSwapChip(this))
            base.Click();
    }

    public override bool IsConfirm()
    {
        var chips = new List<Chip>();

        CheckThreeInRow(Vector2Int.left, chips);
        CheckThreeInRow(Vector2Int.right, chips);

        if (chips.Count >= 2)
        {
            DestroyChips(chips);
            return true;
        }

        chips.Clear();

        CheckThreeInRow(Vector2Int.up, chips);
        CheckThreeInRow(Vector2Int.down, chips);

        if (chips.Count >= 2)
        {
            DestroyChips(chips);
            return true;
        }

        return false;
    }

    private void CheckThreeInRow(Vector2Int direction, List<Chip> chips)
    {
        var delta = direction;
        while (true)
        {
            var newPosition = PositionOnField + delta;

            if (newPosition.x < 0 ||
                newPosition.x >= playingField.Field.GetLength(0) ||
                newPosition.y < 0 ||
                newPosition.y >= playingField.Field.GetLength(1))
                return;

            
            var chip = playingField.Field[newPosition.x, newPosition.y];

            if (chip == null)
                break;

            if (chip.ID == id)
            {
                chips.Add(chip);
            }
            else
            {
                break;
            }

            delta += direction;
        }
    }
}
