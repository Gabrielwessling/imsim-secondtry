using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Job
{
    [SerializeField] public string jobName;
    [SerializeField] public string description;
    [SerializeField] public float payment;
    [SerializeField] public bool canBeCompleted;
    [SerializeField] public bool isCompleted;
    [SerializeField] public bool isFailed;
    [SerializeField] public string map;
    [SerializeField] public Vector3 spawnPosition;
}

public class JobSystem : MonoBehaviour
{
    public GameObject playerRoot;
    public ChangeSceneInteraction changeSceneInteractable;
    public Job activeJob;
    [SerializeField] public List<Job> availableJobs = new List<Job>();

    [SerializeField] private Character playerCharacter;
    [SerializeField] private TMP_Text jobTitleText;
    [SerializeField] private TMP_Text jobDescriptionText;
    [SerializeField] private JobList jobListUI;

    public List<Job> GetJobList()
    {
        return availableJobs;
    }

    public void SetActiveJob(Job job)
    {
        activeJob = job;
        jobTitleText.text = job.jobName;
        jobDescriptionText.text = job.description;
        changeSceneInteractable.sceneName = job.map;
        changeSceneInteractable.spawnPosition = job.spawnPosition;
    }
}
