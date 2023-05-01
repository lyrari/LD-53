using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    public Material RedMaterial;
    public Material BlueMaterial;
    public Material YellowMaterial;

    public Letter LetterPrefab;
    public int LettersInBag = 1;
    
    public void OpenBag()
    {
        for (int i = 0; i < LettersInBag; i++)
        {
            Letter letter = Instantiate(LetterPrefab);
            letter.transform.position = this.transform.position; // TODO: spawn them in better spots?

            int randomInt = Random.Range(0, 3);
            Material matType;
            DepositBin.BinType binType;
            switch (randomInt)
            {
                case 0:
                    matType = RedMaterial;
                    binType = DepositBin.BinType.Red;
                    break;
                case 1:
                    matType = BlueMaterial;
                    binType = DepositBin.BinType.Blue;
                    break;
                case 2:
                    matType = YellowMaterial;
                    binType = DepositBin.BinType.Yellow;
                    break;
                default:
                    Debug.LogWarning("Error: Invalid bintype");
                    return;
            }

            letter.Init(matType, binType);
        }
        Destroy(this.gameObject);
    }
}
