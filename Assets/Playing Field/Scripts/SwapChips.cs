using DG.Tweening;
using System.Collections;
using UnityEngine;
using Zenject;

public class SwapChips : MonoBehaviour
{
    private PlayingField playingField;

    private Chip chip1;
    private Chip chip2;

    private IEnumerator swapCoroutine;

    private bool isCanSwap = true;

    public bool SetSwapChip(Chip chip)
    {
        if (!isCanSwap)
            return false;

        if (chip1 == null)
        {
            chip1 = chip;
            return true;
        }
        else if (chip2 == null)
        {
            chip2 = chip;
        }

        swapCoroutine = Swap();
        StartCoroutine(swapCoroutine);

        return true;
    }

    private IEnumerator Swap()
    {
        isCanSwap = false;

        if (playingField.IsNeighbour(chip1, chip2))
        {
            yield return DOTween.Sequence()
                .AppendCallback(() =>
                {
                    chip1.Deselect();
                    chip2.Deselect();

                    chip1.transform.DOMove(chip2.transform.position, 0.2f);
                    chip2.transform.DOMove(chip1.transform.position, 0.2f);
                })
                .WaitForCompletion();

            yield return new WaitForSeconds(0.5f);

            yield return DOTween.Sequence()
                .AppendCallback(() =>
                {
                    playingField.Swap(chip1, chip2);

                    var confirm1 = chip1.IsConfirm();
                    var confirm2 = chip2.IsConfirm();

                    if (!confirm1 && !confirm2)
                    {
                        chip1.transform.DOMove(chip2.transform.position, 0.2f);
                        chip2.transform.DOMove(chip1.transform.position, 0.2f);

                        playingField.Swap(chip1, chip2);
                    }
                })
                .WaitForCompletion();

            yield return DOTween.Sequence()
                .AppendCallback(() =>
                {
                    chip1 = null;
                    chip2 = null;
                })
                .WaitForCompletion();
        }
        else
        {
            chip1.Cancel();
            chip2.Cancel();

            chip1 = null;
            chip2 = null;
        }

        isCanSwap = true;

        yield return null;
    }

    [Inject]
    private void Init(PlayingField playingField)
    {
        this.playingField = playingField;
    }
}
