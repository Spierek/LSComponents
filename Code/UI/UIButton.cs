namespace LSTools
{
    public class UIButton : AUIButton
    {
        public readonly AEvent OnClicked = new AEvent();

        protected override void HandleButtonClicked()
        {
            OnClicked.Invoke();
        }
    }
}