using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightNode.DataCollection.Domain.Managers
{
    public interface ISurveyManager
    {
        SurveyPending Create(SurveyPending waterbirdForagingModel);
        Guid NewIdentifier();
        SurveyPending Update(SurveyPending entity);
        bool Delete(Guid guid);
        ISurvey FindBySurveyId(Guid guid, int surveyTypeId);
        IReadOnlyList<ISurvey> FindBySubmitterIdAndSurveyType(int userId, int surveyTypeId);
        SurveyCompleted Finish(SurveyPending survey);
        IReadOnlyList<SurveyListItem> GetSurveyListByTypeAndUser(int surveyTypeId, int? userId = null);
        IReadOnlyList<ForagingSurveyExportItem> ExportAllForagingSurveys();
        IReadOnlyList<RookeryCensusExportItem> ExportAllRookeryCensuses();
        SurveyCompleted Update(SurveyCompleted survey);
    }

    /// <summary>
    /// Domain / business manager for waterbird foraging surveys.
    /// </summary>
    /// <remarks>
    /// This class doesn't inherit from <see cref="DomainManagerBase{TEntity}"/>
    /// because it deals with both pending and completed surveys. Perhaps
    /// duplicate code can be refactored away in the future.
    /// </remarks>
    public class SurveyManager : ISurveyManager
    {
        private readonly ISurveyPersistence _persistence;

        /// <summary>
        /// Constructs a new instance of <see cref="SurveyManager"/>
        /// </summary>
        /// <param name="persistence"></param>
        public SurveyManager(ISurveyPersistence persistence)
        {
            _persistence = persistence ?? throw new ArgumentNullException(nameof(persistence));
        }

        /// <summary>
        /// Looks up either a pending or completed survey by its unique identifier.
        /// </summary>
        /// <param name="surveyIdentifier">Survey's unique identifier</param>
        /// <param name="surveyTypeId">Survey Type Id</param>
        /// <returns>
        /// Either <see cref="SurveyCompleted"/> or <see cref="SurveyPending"/> as an <see cref="ISurvey"/>.
        /// </returns>
        public ISurvey FindBySurveyId(Guid surveyIdentifier, int surveyTypeId)
        {
            ISurvey survey = _persistence.SurveysPending
                                      .FirstOrDefault(x => x.SurveyIdentifier == surveyIdentifier && x.SurveyTypeId == surveyTypeId) as ISurvey
                            ?? _persistence.SurveysCompleted
                                   .FirstOrDefault(x => x.SurveyIdentifier == surveyIdentifier && x.SurveyTypeId == surveyTypeId);

            if (survey == null)
            {
                return null;
            }

            // We are not currently setup to take advantage of EF's hydration option (.Include(path)),
            // so for now do this the manual way
            survey.Observations.AddRange(_persistence.Observations.Where(o => o.SurveyIdentifier == surveyIdentifier));
            survey.Disturbances.AddRange(_persistence.Disturbances.Where(d => d.SurveyIdentifier == surveyIdentifier));

            return survey;
        }

        /// <summary>
        /// Looks up all waterbird survey results submitted by a particular user, whether pending or complete.
        /// </summary>
        /// <param name="userId">
        /// The ID for the user who submitted the surveys.
        /// </param>
        /// <param name="surveyTypeId">Survey Type Id</param>
        /// <returns>
        /// List of both <see cref="SurveyCompleted"/> and <see cref="SurveyPending"/>.
        /// </returns>
        public IReadOnlyList<ISurvey> FindBySubmitterIdAndSurveyType(int userId, int surveyTypeId)
        {
            var list = new List<ISurvey>();

            var pending = FindPendingSurveysSubmittedBy(userId, surveyTypeId);
            list.AddRange(pending);

            var completed = FindCompletedSurveysSubmittedBy(userId, surveyTypeId);
            list.AddRange(completed);

            return LoadLocationNames(list);
        }

        private IReadOnlyList<ISurvey> LoadLocationNames(List<ISurvey> list)
        {
            list.ForEach(x =>
            {
                // due to a foreign key relationship, this should never *not* find a result
                x.LocationName = _persistence.Locations
                                    .First(y => y.Id == x.LocationId)
                                    .SiteName;
            });

            return list;
        }

        private List<SurveyPending> FindPendingSurveysSubmittedBy(int userId, int surveyTypeId)
        {
            return _persistence.SurveysPending
                                .Where(x => x.SubmittedBy == userId && x.SurveyTypeId == surveyTypeId)
                                .ToList();
        }

        private List<SurveyCompleted> FindCompletedSurveysSubmittedBy(int userId, int surveyTypeId)
        {
            return _persistence.SurveysCompleted
                                .Where(x => x.SubmittedBy == userId && x.SurveyTypeId == surveyTypeId)
                                .ToList();
        }

        /// <summary>
        /// Creates a new foraging survey record.
        /// </summary>
        /// <param name="survey">Pending waterbird foraging survey.</param>
        /// <returns>Survey ID</returns>
        public SurveyPending Create(SurveyPending survey)
        {
            if (survey == null)
            {
                throw new ArgumentNullException("survey");
            }

            survey.Validate();


            // TODO: introduce transaction management

            LoadEntitiesIntoPersistenceLayer(survey);

            SaveChanges();

            return survey;
        }

        private void LoadEntitiesIntoPersistenceLayer(SurveyPending survey)
        {
            _persistence.SurveysPending.Add(survey);

            survey.Observations.ForEach(x => _persistence.Observations.Add(x));
            survey.Disturbances.ForEach(x => _persistence.Disturbances.Add(x));
        }

        /// <summary>
        /// Creates a new Guid
        /// </summary>
        /// <returns>Guid</returns>
        public Guid NewIdentifier()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Updates a pending foraging survey record
        /// </summary>
        /// <param name="survey">Update waterbird foraging survey</param>
        public SurveyPending Update(SurveyPending survey)
        {
            if (survey == null)
            {
                throw new ArgumentNullException("survey");
            }

            survey.Validate();

            LoadPendingSurveyIntoPersistenceLayer(survey);
            LoadObservationsIntoPersistenceLayer(survey);
            LoadDisturbancesIntoPersistenceLayer(survey);

            SaveChanges();

            // This object, potentially, has been modified by EF. Return that modified version to the calling method
            return survey;
        }

        /// <summary>
        /// Updates a completed foraging survey record
        /// </summary>
        /// <param name="survey">Update waterbird foraging survey</param>
        public SurveyCompleted Update(SurveyCompleted survey)
        {
            if (survey == null)
            {
                throw new ArgumentNullException("survey");
            }

            survey.Validate();

            LoadCompletedSurveyIntoPersistenceLayer(survey);
            LoadObservationsIntoPersistenceLayer(survey);
            LoadDisturbancesIntoPersistenceLayer(survey);

            SaveChanges();

            // This object, potentially, has been modified by EF. Return that modified version to the calling method
            return survey;
        }

        /// <summary>
        /// Updates an existing survey and converts it to a completed survey.
        /// </summary>
        /// <param name="survey">Update waterbird foraging survey</param>
        public SurveyCompleted Finish(SurveyPending survey)
        {
            if (survey == null)
            {
                throw new ArgumentNullException("survey");
            }

            survey.Validate();

            LoadObservationsIntoPersistenceLayer(survey);
            LoadDisturbancesIntoPersistenceLayer(survey);
            var completed = SwitchToCompletedSurvey(survey);
            RemovePendingSurvey(survey);

            SaveChanges();

            return completed;
        }

        /// <summary>
        /// Gets a list of all of the foraging surveys, whether pending or complete.
        /// </summary>
        /// <param name="surveyTypeId">Survey Type Id</param>
        /// <param name="userId">Optional User Id</param>
        /// <returns>
        /// Read-only list of <see cref="SurveyListItem"/>.
        /// </returns>
        public IReadOnlyList<SurveyListItem> GetSurveyListByTypeAndUser(int surveyTypeId, int? userId = null)
        {
            var surveysPending = _persistence.SurveysPending.AsQueryable();
            var locations = _persistence.Locations.AsQueryable();
            var users = _persistence.Users.AsQueryable();
            var surveysCompleted = _persistence.SurveysCompleted.AsQueryable();


            if (userId.HasValue)
            {
                surveysPending = surveysPending.Where(x => x.SubmittedBy == userId.Value);
                surveysCompleted = surveysCompleted.Where(x => x.SubmittedBy == userId.Value);
            }

            var pending = surveysPending
                        .Where(survey => survey.SurveyTypeId == surveyTypeId)
                        .Join(
                            locations,
                            survey => survey.LocationId,
                            location => location.Id,
                            (survey, location) => new { survey, location }
                        )
                        .Join(
                            users,
                            spl => spl.survey.SubmittedBy,
                            user => user.Id,
                            (spl, user) => new { spl.survey, spl.location, user }
                        )
                        .Select(
                            x => new SurveyListItem
                            {
                                SiteCode = x.location.SiteCode,
                                SiteName = x.location.SiteName,
                                StartDate = x.survey.StartDate.Value,
                                SubmittedBy = (x.user.GivenName + " " + x.user.FamilyName).Trim(),
                                SurveyIdentifier = x.survey.SurveyIdentifier,
                                Status = "Pending"
                            }
                        ).ToList();

            var complete = surveysCompleted
                        .Where(survey => survey.SurveyTypeId == surveyTypeId)
                        .Join(
                            locations,
                            survey => survey.LocationId,
                            location => location.Id,
                            (survey, location) => new { survey, location }
                        )
                        .Join(
                            users,
                            spl => spl.survey.SubmittedBy,
                            user => user.Id,
                            (spl, user) => new { spl.survey, spl.location, user }
                        )
                        .Select(
                            x => new SurveyListItem
                            {
                                SiteCode = x.location.SiteCode,
                                SiteName = x.location.SiteName,
                                StartDate = x.survey.StartDate.Value,
                                SubmittedBy = (x.user.GivenName + " " + x.user.FamilyName).Trim(),
                                SurveyIdentifier = x.survey.SurveyIdentifier,
                                Status = "Complete"
                            }
                        ).ToList();

            pending.AddRange(complete);

            var merged = pending.OrderBy(x => x.StartDate)
                                .ToList();

            return merged;
        }

        /// <summary>
        /// Retrieves all completed Foraging Survey data.
        /// </summary>
        /// <returns>
        /// Read-only collection of <see cref="ForagingSurveyExportItem"/>.
        /// </returns>
        public IReadOnlyList<ForagingSurveyExportItem> ExportAllForagingSurveys()
        {
            return _persistence.ForagingSurveyExport
                                .ToList();
        }

        public IReadOnlyList<RookeryCensusExportItem> ExportAllRookeryCensuses()
        {
            return _persistence.RookeryCensusExport.ToList();
        }

        private void SaveChanges()
        {
            _persistence.SaveChanges();
        }


        private SurveyCompleted SwitchToCompletedSurvey(SurveyPending survey)
        {
            var completed = survey.ToSurveyCompleted();
            _persistence.SurveysCompleted.Add(completed);

            return completed;
        }

        private void RemovePendingSurvey(SurveyPending survey)
        {
            LoadPendingSurveyIntoPersistenceLayer(survey);
            _persistence.SurveysPending.Remove(survey);
        }

        private void LoadPendingSurveyIntoPersistenceLayer(SurveyPending survey)
        {
            survey.Id = _persistence.SurveysPending
                            .Where(x => x.SurveyIdentifier == survey.SurveyIdentifier)
                            .Select(x => x.Id)
                            .FirstOrDefault();
            if (survey.Id == 0)
            {
                throw new InvalidOperationException("No pending survey exists for identifer " + survey.SurveyIdentifier);
            }

            _persistence.SurveysPending.Add(survey);
        }

        private void LoadCompletedSurveyIntoPersistenceLayer(SurveyCompleted survey)
        {
            survey.Id = _persistence.SurveysCompleted
                            .Where(x => x.SurveyIdentifier == survey.SurveyIdentifier)
                            .Select(x => x.Id)
                            .FirstOrDefault();
            if (survey.Id == 0)
            {
                throw new InvalidOperationException("No completed survey exists for identifer " + survey.SurveyIdentifier);
            }

            _persistence.SurveysCompleted.Add(survey);

        }

        private void LoadObservationsIntoPersistenceLayer(ISurvey survey)
        {
            survey.Observations.ForEach(x =>
            {
                _persistence.Observations.Add(x);
            });
        }

        private void LoadDisturbancesIntoPersistenceLayer(ISurvey survey)
        {
            survey.Disturbances.ForEach(x =>
            {
                _persistence.Disturbances.Add(x);
            });
        }

        /// <summary>
        /// Deletes a pending survey (deleting completed surveys is not allowed).
        /// </summary>
        /// <param name="identifier">Survey identifier</param>
        /// <returns>
        /// True if successful
        /// </returns>
        public bool Delete(Guid identifier)
        {
            var record = _persistence.SurveysPending.FirstOrDefault(x => x.SurveyIdentifier == identifier);
            if (record != null)
            {
                _persistence.SurveysPending.Attach(record);
                _persistence.SurveysPending.Remove(record);
                if (_persistence.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
