using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cup_Diamond_Controller : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch(gameObject.tag)
            {
                case "gun": GameManager.Instance.IsHaveGun = true; break;
                case "fly": GameManager.Instance.isHaveFly = true; break;
                case "cup": GameManager.Instance.IsTouchCup = true; break;
                case "diamond": Constants._curScoresInt += Constants.Diamond_Score; break;
                case "vuong_mien": Constants._curScoresInt += Constants.VuongMien_Score; break;
                case "den": Constants._curScoresInt += Constants.Den_Score; break;
            }
            Constants._isAnKimCuong = true;
            GameManager.Instance.PlayOneShot(_clip);
            Destroy(this.gameObject);
        }
    }
}
