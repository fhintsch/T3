namespace Dp2Lib.Models
{
    public class Reference
    {
        public string Type { get; set; }
        public string Ident { get; set; }
        public object Referer { get; set; }
    }

    public class ReferenceGroup : List<Reference>
    {
        public string Name { get; private set; }

        public ReferenceGroup(string name, List<Reference> references) : base(references)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

}
