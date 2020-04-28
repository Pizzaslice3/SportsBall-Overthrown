using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManagerBehavior : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;
    public AudioSource hitSound;

    public Animator hitAnim;
   
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Enemies Hit" + score + "/39";
    }


    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Enemies Hit" + score + "/39";
        hitSound.PlayOneShot(hitSound.clip);
        hitAnim.SetTrigger("Hit");
    }

  

}
