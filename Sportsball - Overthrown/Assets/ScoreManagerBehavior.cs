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
        scoreText.text = score.ToString();
    }


    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        hitSound.PlayOneShot(hitSound.clip);
        hitAnim.SetTrigger("Hit");
    }

  

}
