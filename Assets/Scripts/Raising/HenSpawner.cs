﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raising {
    public class HenSpawner : MonoBehaviour
    {

        [SerializeField]
        public Hen henPrefab;

        [SerializeField]
        public GameObject loveIncreaseIcon;
        [SerializeField]
        public GameObject sizeIncreaseIcon;
        [SerializeField]
        public GameObject fitnessIncreaseIcon;
        [SerializeField]
        public GameObject featherQualityIncreaseIcon;


        // Start is called before the first frame update
        void Start()
        {          
            List<HenInfo> savedData = HenInfoPersist.loadList();
			// savedData.Clear();
            if (savedData.Count>0) {
                foreach(HenInfo h in savedData) {
                    spawnHen(h);
                }
            } else {
                for (int i = 0; i < 4; i++) {
                    spawnHen();
                }
            }
        }

        private void spawnHen(HenInfo info) {
            //TODO fix the bug where hens occasionally spawn inside each other and this causes them to be launched upwards
            Vector3 position = this.transform.position 
            + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f,2f));
            Hen hen = Instantiate(henPrefab, position, Quaternion.identity);
        

            
            if(System.Enum.IsDefined(typeof(HenBreed), info.breedNumber)) {
                hen.breed = (HenBreed) info.breedNumber;
            }

           
			
            hen.henName = info.name;
            hen.love = new Stat(hen, info.loveStat, loveIncreaseIcon);
            hen.size = new Stat(hen, info.sizeStat, sizeIncreaseIcon);
            hen.fitness = new Stat(hen, info.fitnessStat, fitnessIncreaseIcon);
            hen.featherQuality = new Stat(hen, info.feathersStat, featherQualityIncreaseIcon);
        }

        private void spawnHen() {
            string newName = NamesList.getNextName();

            int breedn = Random.Range(0, 3);
            HenBreed breed = (HenBreed) breedn;
            

            spawnHen(new HenInfo(newName, breed, 0, 0, 0, 0));
        }        
        
        public void spawnHen(HenBreed breed) {
            string newName = NamesList.getNextName();            

            spawnHen(new HenInfo(newName, breed, 0, 0, 0, 0));
        }       
    }
}

