using System;
using System.Collections.Generic;
using System.Linq;
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
            return experiences ?? throw new ArgumentNullException(nameof(experiences));
        }

        private static Text CheckIntroduction(Text introduction)
        {
            return introduction ?? throw new ArgumentNullException(nameof(introduction));
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
        private const int MaximumDescriptionLength = 200;

        public Experience(
            Name company,
            Text experienceDescription,
            OpenEndedPeriod period
        )
        {
            Company = company ?? throw new ArgumentNullException(nameof(company));
            ExperienceDescription = CheckExperienceDescription(experienceDescription);
            Period = period ?? throw new ArgumentNullException(nameof(period));
        }

        public Name Company { get; private set; }
        public Text ExperienceDescription { get; private set; }
        public OpenEndedPeriod Period { get; private set; }

        private static Text CheckExperienceDescription(Text? experienceDescription)
        {
            if (experienceDescription is null || experienceDescription.Length > MaximumDescriptionLength)
            {
                throw new ArgumentNullException(nameof(experienceDescription));
            }

            return experienceDescription ?? throw new ArgumentNullException(nameof(experienceDescription));
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Company;
            yield return ExperienceDescription;
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
            var result = items?.ToHashSet();

            if (result is null || result.Count == 0)
            {
                throw new ArgumentException(nameof(items));
            }

            return result;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            return Items;
        }
    }
}