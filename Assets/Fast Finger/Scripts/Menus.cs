using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FastFinger
{
    public class Menus : MonoBehaviour
    {   //This script is used for navigation through different menus
        [SerializeField]
        private Text mainMenuBestScore;
        [SerializeField]
        private GameObject mainMenu;
        [SerializeField]
        private GameObject statsMenu;
        [SerializeField]
        private GameObject shopMenu;
        [SerializeField]
        private GameObject giftMenu;
        [SerializeField]
        private GameObject settingsMenu;
        [SerializeField]
        private Slider volumeSlider;
        [SerializeField]
        private Toggle vibrationToggle;
        [SerializeField]
        private GameObject gameplayMenu;
        [SerializeField]
        private Text score;
        [SerializeField]
        private GameObject pauseButton;
        [SerializeField]
        private GameObject pauseMenu;
        [SerializeField]
        private GameObject gameOverMenu;
        [SerializeField]
        private MenuFadeInFadeOutAnimation menuAnimation;
        private AudioSource buttonSound;

        void Start()
        {
            mainMenuBestScore.text = "BEST: " + PlayerPrefs.GetInt("BestScore");
            buttonSound = GameObject.Find("ButtonSound").GetComponent<AudioSource>();
            if (PlayerPrefs.GetInt("Vibration") == 1)
            {
                vibrationToggle.SetIsOnWithoutNotify(true);
            }
        }
        public void StartTheGameFadeInFadeOutAnimation()
        {
            menuAnimation.menu = 0;
            menuAnimation.enabled = true;
            buttonSound.Play();
        }

        public void StartTheGame()
        {
            mainMenu.SetActive(false);
            gameplayMenu.SetActive(true);
            GameObject player = Instantiate(Resources.Load("Player")) as GameObject;
            player.name = "Player";
            player.transform.position = new Vector2(0, -5);
            Camera.main.GetComponent<CameraFollow>().player = player;
            Camera.main.GetComponent<CameraFollow>().enabled = true;
            GameObject.Find("Background").GetComponent<BackgroundRepeat>().ResetBackgrounPosition();
            GameObject.Find("Background").GetComponent<BackgroundRepeat>().player = player;
            PlayerPrefs.SetInt("GamesPlayed", (PlayerPrefs.GetInt("GamesPlayed") + 1));
        }
        public void ShowStatsMenu()
        {
            statsMenu.SetActive(true);
            buttonSound.Play();
        }

        public void HideStatsMenu()
        {
            statsMenu.SetActive(false);
            buttonSound.Play();
        }

        public void ShowShopMenu()
        {
            shopMenu.SetActive(true);
            buttonSound.Play();
        }

        public void HideShopMenu()
        {
            shopMenu.SetActive(false);
            buttonSound.Play();
        }

        public void ShowGiftMenu()
        {
            giftMenu.SetActive(true);
            buttonSound.Play();
        }

        public void HideGiftMenu()
        {
            giftMenu.SetActive(false);
            buttonSound.Play();
        }

        public void ShowSettingsMenu()
        {
            settingsMenu.SetActive(true);
            buttonSound.Play();
        }

        public void SetVolume()
        {
            AudioListener.volume = volumeSlider.value;
        }

        public void SetVibration()
        {
            if (vibrationToggle.isOn)
            {
                PlayerPrefs.SetInt("Vibration", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Vibration", 0);
            }
            buttonSound.Play();
        }

        public void HideSettingMenu()
        {
            settingsMenu.SetActive(false);
            buttonSound.Play();
        }

        public void ShowPauseMenu()
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            buttonSound.Play();
        }

        public void HidePauseMenu()
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            Invoke("ActivateThePlayerMovement", 0.1f);
            buttonSound.Play();
        }

        private void ActivateThePlayerMovement()
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        }

        public void ReplayFadeInFadeOutAnimation()
        {
            if (GameObject.Find("Player") != null)
                GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            Time.timeScale = 1;
            menuAnimation.menu = 2;
            menuAnimation.enabled = true;
            buttonSound.Play();
        }

        public void Replay()
        {
            BackToTheMainMenu();
            StartTheGame();
        }

        public void BackToTheMainMenuFadeInFadeOutAnimation()
        {
            if (GameObject.Find("Player") != null)
                GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            Time.timeScale = 1;
            menuAnimation.menu = 1;
            menuAnimation.enabled = true;
            buttonSound.Play();
        }

        public void BackToTheMainMenu()
        {
            if (GameObject.Find("Player") != null)
                Destroy(GameObject.Find("Player"));
            gameplayMenu.SetActive(false);
            pauseButton.SetActive(true);
            score.gameObject.SetActive(true);
            pauseMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            mainMenu.SetActive(true);
            GameObject[] arenas = GameObject.FindGameObjectsWithTag("Arena");
            foreach (GameObject arena in arenas)
            {
                Destroy(arena);
            }
            Vars.currentArena = 0;
            Vars.score = 0;
            mainMenuBestScore.text = "BEST: " + PlayerPrefs.GetInt("BestScore");
        }

        public void UpdateTheScoreUI()
        {
            score.text = "" + (int)Vars.score;
        }

        public void GameOver()
        {
            score.gameObject.SetActive(false);
            pauseButton.SetActive(false);
            Invoke("ShowGameOverMenu", 1.5f);
        }

        public void ShowGameOverMenu()
        {
            gameOverMenu.SetActive(true);
            gameOverMenu.transform.Find("YourScore").GetComponent<Text>().text = "YOUR SCORE: " + (int)Vars.score;
            gameOverMenu.transform.Find("BestScore").GetComponent<Text>().text = "BEST SCORE: " + PlayerPrefs.GetInt("BestScore");

        }

        public void AddStars(int value)
        {
            buttonSound.Play();
            IAPManager.OnPurchaseSuccess = () =>
            {
                PlayerPrefs.SetInt("NumberOfStars", PlayerPrefs.GetInt("NumberOfStars", 0) + value);
                GameObject.Find("ShopMenuNumberOfStarsText").GetComponent<Text>().text =
                    "" + PlayerPrefs.GetInt("NumberOfStars", 0);
            };
                
            switch (value)
            {
                case 10: 
                    IAPManager.Instance.BuyProductID(IAPKey.PACK1);
                    break;
                case 20: 
                    IAPManager.Instance.BuyProductID(IAPKey.PACK2);
                    break;
                case 50: 
                    IAPManager.Instance.BuyProductID(IAPKey.PACK3);
                    break;
                case 100: 
                    IAPManager.Instance.BuyProductID(IAPKey.PACK4);
                    break;
            }
        }
    }
}
