namespace Nop.Core.Domain.Questionnaire
{
    public class Step: BaseEntity
    {
        //private Step _no;
        //private Step _yes;
        //private Step(ILazyLoader lazyLoader)
        //{
        //    LazyLoader = lazyLoader;
        //}

        //private ILazyLoader LazyLoader { get; set; }

        public string Title { get; set; }
        public string ImageURL { get; set; }
        public int? YesId { get; set; }
        public int? NoId { get; set; }
        public virtual Step No { get; set; }
        public virtual Step Yes { get; set; }

        //public virtual Step No { get => LazyLoader.Load(this, ref _no); set => _no = value; }
        //public virtual Step Yes { get => LazyLoader.Load(this, ref _yes); set => _yes = value; }
        public virtual Step InverseNo { get; set; }
        public virtual Step InverseYes { get; set; }
    }
}
