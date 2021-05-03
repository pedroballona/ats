using System.Collections.Generic;
using System.Linq;
using HR.ATS.CrossCutting;
using HR.ATS.Domain.Common;

namespace HR.ATS.Domain.Applicant
{
    public class Resume : ValueObject
    {
        public Resume(Text introduction, Experiences experiences)
        {
            Introduction = CheckIntroduction(introduction);
            Experiences = CheckExperiences(experiences);
        }

        public Text Introduction { get; private set; }
        public Experiences Experiences { get; private set; }

        private static Experiences CheckExperiences(Experiences experiences)
        {
            return experiences ?? throw new ValidationFieldRequiredException("Experiences");
        }

        private static Text CheckIntroduction(Text introduction)
        {
            return introduction ?? throw new ValidationFieldRequiredException("Introduction");
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Introduction;
            yield return Experiences;
        }

        public void ChangeIntroduction(Text introduction)
        {
            Introduction = CheckIntroduction(introduction);
        }

        public void ChangeExperiences(Experiences experiences)
        {
            Experiences = CheckExperiences(experiences);
        }
    }

    public class Experience : ValueObject
    {
        public Experience(
            Name company,
            Text description,
            OpenEndedPeriod period
        )
        {
            Company = company ?? throw new ValidationFieldRequiredException("company name");
            Description = CheckDescription(description);
            Period = period ?? throw new ValidationFieldRequiredException("experience period");
        }

        public Name Company { get; private set; }
        public Text Description { get; private set; }
        public OpenEndedPeriod Period { get; private set; }

        private static Text CheckDescription(Text? experienceDescription)
        {
            return experienceDescription ?? throw new ValidationFieldRequiredException("experience description");
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Company;
            yield return Description;
            yield return Period;
        }
    }

    public class Experiences : ValueObject
    {
        public Experiences(IEnumerable<Experience> items)
        {
            Items = CheckItems(items);
        }

        public IEnumerable<Experience> Items { get; private set; }

        private static IEnumerable<Experience> CheckItems(IEnumerable<Experience> items)
        {
            var result = items.ToHashSet();

            if (result is null || result.Count == 0)
                throw new ValidationException("It's required to have at least one experience instance.");

            return result;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            return Items;
        }
    }
}