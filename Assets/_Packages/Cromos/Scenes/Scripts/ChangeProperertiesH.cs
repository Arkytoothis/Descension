namespace Cromos
{
    public class ChangeProperertiesH : ChangeProperties
    {
        void Awake()
        {
            ca = GetComponent<Highlighter>();
        }

        protected override void Start()
        {
            base.Start();
            Highlighter h = ca as Highlighter;
            h.Highlight();
        }

    }

}
