using HR.ATS.CrossCutting;
using HR.ATS.Domain.Common;

namespace HR.ATS.Domain.Opening
{
    public class Opening : Entity
    {
        public Opening(
            Name name,
            Text description,
            bool isOpen
        )
        {
            Name = name ?? throw new ValidationFieldRequiredException("name");
            Description = description ?? throw new ValidationFieldRequiredException("opening description");
            IsOpen = isOpen;
        }

        public Name Name { get; private set; }
        public Text Description { get; private set; }
        public bool IsOpen { get; private set; }

        public Application Apply(Applicant.Applicant applicant)
        {
            if (!IsOpen) throw new ValidationException("It's not possible to apply to a closed opening.");

            return new Application(applicant.Id, Id);
        }
    }
}