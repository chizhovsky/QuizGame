using System.Collections.Generic;

[System.Serializable]
public class QuestionsList 
{
    public List<QuestionAndAnswers> questionAndAnswers;
}

[System.Serializable]
public class QuestionAndAnswers 
{
    public string question;
    public string imageUrl;
    public string[] answers;
    public int correctAnswer;
}
