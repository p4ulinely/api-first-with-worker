namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        private List<Error> _erros = new List<Error>();

        public bool Valid { get => _erros.Count == 0; }
        public bool Invalid { get => !Valid; }
        public IEnumerable<Error> Errors { get => _erros.AsEnumerable();  }

        public void AddError(string message)
            => _erros.Add(new Error(default, message));

        public abstract void Validacoes();
    }
}