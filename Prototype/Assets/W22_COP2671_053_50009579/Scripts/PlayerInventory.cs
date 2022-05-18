using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfDiamonds { get; private set; }
    public UnityEvent<PlayerInventory> OnDiamondCollected;

    public void DiamondsCollected()
    {
        NumberOfDiamonds++;
        OnDiamondCollected.Invoke(this);

        if(NumberOfDiamonds.Equals(5))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(1);
        }
    }
}
