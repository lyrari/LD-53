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
    Vector3 LetterOffset = new Vector3(0.05f, 0);
    
    public void OpenBag(Vector3 lettersPosition)
    {
        for (int i = 0; i < LettersInBag; i++)
        {
            Letter letter = Instantiate(LetterPrefab);
            letter.transform.position = lettersPosition + LetterOffset * i; // TODO: spawn them in better spots?

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

            float StampChance = Random.Range(0, 100);
            bool hasStamp = StampChance > 33; // 33%

            letter.Init(matType, binType, hasStamp);
            AkSoundEngine.PostEvent("mailMove", this.gameObject);
        }
        Destroy(this.gameObject);
    }
}
