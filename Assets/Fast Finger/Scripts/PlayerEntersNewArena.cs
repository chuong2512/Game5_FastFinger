using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastFinger
{
    public class PlayerEntersNewArena : MonoBehaviour
    {
        void Start()
        {
            InstantiateNewGameArena();
        }
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.name.Equals("StartOfTheArena"))//Inside the each Arena game object there is a "StartOfTheArena" game object that is used to detect when player enter new arena
            {
                GameObject[] gameArenas = GameObject.FindGameObjectsWithTag("Arena");
                foreach (GameObject gameArena in gameArenas)
                {
                    if (gameArena.transform.position.y < transform.position.y)//Destroy game arena objects that are below the player game object
                    {
                        Destroy(gameArena, 2f);
                    }
                }
                InstantiateNewGameArena();
                Destroy(col.gameObject);
            }
        }

        private void InstantiateNewGameArena()
        {
            int arenaNumber = Random.Range(1, 16);//How many ostacles are insde the "Resources" folder
            GameObject newArena = Instantiate(Resources.Load("Arena" + arenaNumber)) as GameObject;//Instantiate random osbstacle from the "Resources" folder
            newArena.gameObject.name = "Arena" + arenaNumber;
            newArena.transform.position = new Vector2(0, Vars.currentArena * 15);
            Vars.currentArena++;
        }
    }
}
