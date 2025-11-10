using System.Collections.Generic;
using UnityEngine;

public class JobList : MonoBehaviour
{
    public GameObject jobItemPrefab;
    public Transform jobListParent;
    public JobSystem jobSystem;

    void OnEnable()
    {
        if (jobSystem != null)
        {
            PopulateJobList(jobSystem.GetJobList());
        }
    }

    public void PopulateJobList(List<Job> jobs)
    {
        // Clear existing job items
        foreach (Transform child in jobListParent)
        {
            Destroy(child.gameObject);
        }

        // Populate with new job items
        foreach (var job in jobs)
        {
            GameObject jobItem = Instantiate(jobItemPrefab, jobListParent);
            JobPrefab jobPrefab = jobItem.GetComponent<JobPrefab>();
            jobPrefab.jobTitleText.text = job.jobName;
            jobPrefab.jobDescriptionText.text = job.description;
            jobPrefab.jobPaymentText.text = $"${job.payment}";
            jobPrefab.button.onClick.AddListener(() =>
            {
                if (jobSystem != null)
                {
                    jobSystem.SetActiveJob(job);
                }
            });
        }
    }
}
