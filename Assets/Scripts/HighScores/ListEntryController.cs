using UnityEngine.UIElements;

public class ListEntryController
{
    Label nameLabel, scoreLabel;

    public void SetVisualElement(VisualElement visualElement)
    {
        SetNameLabel(visualElement);
        SetScoreLabel(visualElement);
    }

    public void SetNameLabel(VisualElement visualElement)
    {
        nameLabel = visualElement.Q<Label>("name");
    }
    public void SetScoreLabel(VisualElement visualElement)
    {
        scoreLabel = visualElement.Q<Label>("score");
    }

    public void SetEntryName(string name)
    {
        nameLabel.text = name;
    }
    public void SetEntryScore(string score)
    {
        scoreLabel.text = score;
    }
}