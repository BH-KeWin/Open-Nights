using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class AnimatronicAI : MonoBehaviour
{
    public int stage;
    Animator animator;

    [SerializeField]AudioSource[] doorHit, ambienceIntenseSound;
    [SerializeField]AudioClip[] moveSounds;
    [SerializeField]AudioSource moveSound;
    [SerializeField]bool randomizePath;
    [SerializeField]AnimatronicExtraAIPaths[] extraPaths;
    [SerializeField]DoorHolder doorHolder;
    [SerializeField]TimePass timePass;
    [SerializeField]float timer;
    [SerializeField]int aiNum;
    [SerializeField]string animatronicName;
    [SerializeField]Transform[] positions;
    [SerializeField]PlayerMovement player;
    [SerializeField]GameObject jsModel, globalStatic, camUI;



    int aiLevel, maxaiLevel = 20, extraItem = 0;
    Vector3 defaultPos;
    Quaternion defaultRot;

    void Start(){
        animator = GetComponent<Animator>();
        defaultPos = transform.position;
        defaultRot = transform.rotation;


        if(PlayerPrefs.GetString("CustomNight", "not active") == "active"){
            aiLevel = PlayerPrefs.GetInt(animatronicName);
            StartCoroutine("AdvanceAI");
            return;
        }
        
        aiLevel = timePass.GetAILevel(aiNum);

        if(PlayerPrefs.GetString("Ice") == "active"){
            PlayerPrefs.SetString("Ice",  "not active");
            extraItem = 3;

            aiLevel -= extraItem;
            if(aiLevel < 0){
                aiLevel = 0;
            }
        }

        StartCoroutine("AdvanceAI");
    }

    public void ResetAnimatronic(){
        StopCoroutine("AdvanceAI");
        stage = 0;
        gameObject.transform.position = defaultPos;
        gameObject.transform.rotation = defaultRot;
        animator.Play(stage.ToString(), 0, 0);
        StartCoroutine("GlobalStaticFader");
        StartCoroutine("AdvanceAI");
    }

    IEnumerator AdvanceAI(){
        if(randomizePath && stage == 0){
            int randomNum = Random.Range(0, extraPaths.Length);
            positions = extraPaths[randomNum].positions;
            doorHolder = extraPaths[randomNum].doorHolder;
        }

        
        if(stage == positions.Length){
            yield return new WaitForSecondsRealtime(timer / 2 + 1.5f);
        }else{
            yield return new WaitForSecondsRealtime(timer + Random.Range(-1.5f, 1.5f));
        }

        if(Random.Range(1, maxaiLevel) <= aiLevel){
            if(positions.Length > stage){
                stage++;
                StartCoroutine("GlobalStaticFader");
                moveSound.clip = moveSounds[Random.Range(0, moveSounds.Length)];
                moveSound.Play();
            }
            else{
                if(doorHolder != null && doorHolder.closed){
                    doorHit[Random.Range(0, doorHit.Length)].Play();
                    stage = 0;
                    gameObject.transform.position = defaultPos;
                    gameObject.transform.rotation = defaultRot;
                    animator.Play(stage.ToString(), 0, 0);
                    StartCoroutine("GlobalStaticFader");
                }else{
                    if(!player.gameObject.activeSelf){
                        camUI.SetActive(false);
                        player.gameObject.SetActive(true);
                    }
                    player.GameOver(jsModel);
                }
            }
        }else{
            if(doorHolder != null && doorHolder.closed && positions.Length == stage){

                Debug.Log("asd");
                doorHit[Random.Range(0, doorHit.Length)].Play();
                stage = 0;
                gameObject.transform.position = defaultPos;
                gameObject.transform.rotation = defaultRot;
                animator.Play(stage.ToString(), 0, 0);
                StartCoroutine("GlobalStaticFader");
            }
            //not moving
        }

        if(stage != 0){
            gameObject.transform.position = positions[stage-1].position;
            gameObject.transform.rotation = positions[stage-1].rotation;
            animator.Play(stage.ToString(), 0, 0);
        }

        if(stage > 3){
            if(!ambienceIntenseSound[0].isPlaying && !ambienceIntenseSound[1].isPlaying){
                ambienceIntenseSound[Random.Range(0, ambienceIntenseSound.Length)].Play();
            }
        }

        StartCoroutine("AdvanceAI");
    }

    IEnumerator GlobalStaticFader(){
        globalStatic.GetComponent<RawImage>().color = new Color32(123, 123, 123, 255);
        yield return new WaitForSecondsRealtime(0.2f);
        
        for(int x = 255; x > 46; x -= 3){
            globalStatic.GetComponent<RawImage>().color = new Color32(123, 123, 123, (byte)x);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        
    }
}
