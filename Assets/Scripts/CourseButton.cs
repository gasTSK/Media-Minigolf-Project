using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CourseButton : MonoBehaviour
{
    public TMP_Text nameText;
    public CourseController theCourse;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = theCourse.courseName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectCourse()
    {
        theCourse.transform.SetParent(null);

        theCourse.gameObject.SetActive(true);

        SceneManager.LoadScene(theCourse.firstScene);
    }
}
