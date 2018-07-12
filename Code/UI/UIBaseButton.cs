namespace LSTools
{
    public class UIBaseButton : AUIButton
    {
        public readonly AEvent OnClicked = new AEvent();

        protected override void HandleButtonClicked()
        {
            OnClicked.Invoke();
        }
    }
}