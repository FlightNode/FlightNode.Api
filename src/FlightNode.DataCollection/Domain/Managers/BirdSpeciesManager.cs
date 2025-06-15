using FlightNode.Common.Exceptions;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;

namespace FlightNode.DataCollection.Domain.Managers
{
    public interface IBirdSpeciesDomainManager : ICrudManager<BirdSpecies>
    {
        IEnumerable<BirdSpecies> GetBirdSpeciesBySurveyTypeId(int surveyTypeId);
        void AddSpeciesToSurveyType(int speciesId, int surveyTypeId);
        void RemoveSpeciesFromSurveyType(int speciesId, int surveyTypeId);
    }

    public class BirdSpeciesDomainManager : DomainManagerBase<BirdSpecies>, IBirdSpeciesDomainManager
    {
        private const string SpeciesDoesNotExistPattern = "Species ID {0} not found.";
        private const string SurveyTypeDoesNotExistPattern = "Survey Type ID {0} not found.";

        /// <summary>
        /// Returns the persistence layer as the specific type instead of generic type
        /// </summary>
        private IBirdSpeciesPersistence BirdSpeciesPersistence
        {
            get
            {
                return Persistence as IBirdSpeciesPersistence;
            }
        }


        /// <summary>
        /// Creates a new <see cref="BirdSpeciesDomainManager"/>.
        /// </summary>
        /// <param name="birdSpeciesPersistence">Persistence provider</param>
        public BirdSpeciesDomainManager(IBirdSpeciesPersistence birdSpeciesPersistence) : base(birdSpeciesPersistence)
        {
        }


        public override BirdSpecies FindById(int id)
        {
            var bird = Persistence.Collection
                .FirstOrDefault(x => x.Id == id);

            // Force EF to query for surveys
            // Cannot use "Include" because already using "AsNoTracking" for performance
            Persistence.Collection.Attach(bird);
            Persistence.Entry(bird)
                .Collection(nameof(BirdSpecies.SurveyTypes))
                .Load();

            if (bird == null)
            {
                throw new DoesNotExistException(string.Format(CultureInfo.InvariantCulture, SpeciesDoesNotExistPattern, id));
            }

            return bird.WithFlatSurveyTypeNames();
        }


        public override BirdSpecies Create(BirdSpecies input)
        {

            input = MapSurveyTypesIntoNewObject(input);

            return base.Create(input);
        }


        public override int Update(BirdSpecies input)
        {
            input.Validate();

            // Retrieve the existing entry and update it, so that we can pull 
            // in the survey types and update those correctly.

            // This is the kind of operation that could well be better off in a stored 
            // procedure in general, but the performance hit of using EF is not likely
            // to be high enough, in this circumstance, to be worth the extra effort.

            var original = FindById(input.Id);
            original.CommonAlphaCode = input.CommonAlphaCode;
            original.CommonName = input.CommonName;
            original.Family = input.Family;
            original.Genus = input.Genus;
            original.Order = input.Order;
            original.SubFamily = input.SubFamily;

            original = RemoveUnassignedSurveyTypes(input, original);
            
            original = AddNewSurveyTypes(input, original);


            return UpdateAttachedObject(original);
        }

        public IEnumerable<BirdSpecies> GetBirdSpeciesBySurveyTypeId(int surveyTypeId)
        {
            var returnVal = BirdSpeciesPersistence.Collection
                                    .Where(birdItem => birdItem.SurveyTypes.Any(surveyItem => surveyItem.Id == surveyTypeId))
                                    .ToList();
            return returnVal;
        }

        public void AddSpeciesToSurveyType(int speciesId, int surveyTypeId)
        {
            try
            {
                var bird = FindById(speciesId);
                var surveyType = RetrieveSurveyTypeExpectedToExist(surveyTypeId);

                AddBirdToSurveyType(bird, surveyType);
            }
            catch (DbUpdateException updateException)
            {
                if (DuplicateKeyExceptionOccurred(updateException))
                {
                    // Not worried about duplicate keys. All the user will care about is that the value has been inserted.
                    return;
                }

                throw;
            }
        }

        public void RemoveSpeciesFromSurveyType(int speciesId, int surveyTypeId)
        {
            var bird = FindById(speciesId);

            var toRemove = bird.SurveyTypes
                               .FirstOrDefault(x => x.Id == surveyTypeId);

            if (toRemove == null)
            {
                // This bird is already not associated with this survey type. Don't worry about it
                return;
            }

            bird.SurveyTypes.Remove(toRemove);

            Persistence.SaveChanges();
        }




        private void AddBirdToSurveyType(BirdSpecies bird, SurveyType surveyType)
        {
            bird.SurveyTypes.Add(surveyType);
            BirdSpeciesPersistence.SaveChanges();
        }

        private SurveyType RetrieveSurveyTypeExpectedToExist(int surveyTypeId)
        {
            var surveyType = BirdSpeciesPersistence.SurveyTypes.FirstOrDefault(x => x.Id == surveyTypeId);
            if (surveyType == null)
            {
                throw new DoesNotExistException(string.Format(CultureInfo.InvariantCulture, SurveyTypeDoesNotExistPattern, surveyTypeId));
            }

            return surveyType;
        }

        private static bool DuplicateKeyExceptionOccurred(DbUpdateException updateException)
        {
            return updateException.InnerException != null &&
                                updateException.InnerException.InnerException != null &&
                                updateException.InnerException.InnerException.Message.Contains("Cannot insert duplicate key");
        }


        private static BirdSpecies RemoveUnassignedSurveyTypes(BirdSpecies input, BirdSpecies original)
        {
            foreach (var item in original.SurveyTypes.ToList())
            {
                if (!input.SurveyTypeNames.Any(x => x == item.Description))
                {
                    original.SurveyTypes
                        .Remove(item);
                }
            }

            return original;
        }

        private BirdSpecies AddNewSurveyTypes(BirdSpecies input, BirdSpecies original)
        {
            var allSurveyTypes = BirdSpeciesPersistence.SurveyTypes.ToList();
            foreach (var item in input.SurveyTypeNames)
            {
                var survey = allSurveyTypes.FirstOrDefault(x => x.Description == item);
                if (survey != null)
                {
                    original.SurveyTypes.Add(survey);
                }
            }

            return original;
        }


        private BirdSpecies MapSurveyTypesIntoNewObject(BirdSpecies input)
        {
            var allSurveyTypes = BirdSpeciesPersistence.SurveyTypes.ToList();
            foreach (var item in input.SurveyTypeNames)
            {
                var survey = allSurveyTypes.FirstOrDefault(x => x.Description == item);
                if (survey != null)
                {
                    input.SurveyTypes.Add(survey);
                }
            }

            return input;
        }

    }
}
