using System;
using HR.ATS.Domain.Common;
using HR.ATS.Domain.Person;

namespace HR.ATS.Domain.Applicant
{
    public class Applicant : Entity
    {
        public Applicant(PersonReference personReference, Resume resume)
        {
            PersonReference = personReference ?? throw new ArgumentNullException(nameof(personReference));
            Resume = CheckResume(resume);
        }

        private static Resume CheckResume(Resume resume)
        {
            return resume ?? throw new ArgumentNullException(nameof(resume));
        }

        public PersonReference PersonReference { get; private set; }
        public Resume Resume { get; private set; }

        public void ChangeResume(Resume resume)
        {
            Resume = CheckResume(resume);
        }
    }
}