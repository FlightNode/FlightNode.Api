using Dapper;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;

namespace FlightNode.DataCollection.Infrastructure.Persistence
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class DataCollectionContext : DbContext, ILocationPersistence, IWorkLogPersistence, IWorkTypePersistence, IBirdSpeciesPersistence, ISurveyTypePersistence, ISurveyPersistence, IEnumRepository
    {
        #region Collections used by persistence interfaces that inherit from IPersistenceBase

        ICrudSet<SurveyType> IPersistenceBase<SurveyType>.Collection
        {
            get
            {
                return new CrudSetDecorator<SurveyType>(this.SurveyTypes);
            }
        }

        ICrudSet<BirdSpecies> IPersistenceBase<BirdSpecies>.Collection
        {
            get
            {
                return new CrudSetDecorator<BirdSpecies>(this.BirdSpecies);
            }
        }

        ICrudSet<Location> IPersistenceBase<Location>.Collection
        {
            get
            {
                return new CrudSetDecorator<Location>(this.Locations);
            }
        }


        ICrudSet<WorkLog> IPersistenceBase<WorkLog>.Collection
        {
            get
            {
                return new CrudSetDecorator<WorkLog>(this.WorkLogs);
            }
        }


        ICrudSet<WorkType> IPersistenceBase<WorkType>.Collection
        {
            get
            {
                return new CrudSetDecorator<WorkType>(this.WorkTypes);
            }
        }

        #endregion

        #region ISurveyPersistence
        ICrudSet<User> ISurveyPersistence.Users
        {
            get
            {
                return new CrudSetDecorator<User>(this.Users);
            }
        }

        ICrudSet<Location> ISurveyPersistence.Locations
        {
            get
            {
                return new CrudSetDecorator<Location>(this.Locations);
            }
        }

        ICrudSet<SurveyPending> ISurveyPersistence.SurveysPending
        {
            get
            {
                return new CrudSetDecorator<SurveyPending>(this.SurveysPending);
            }
        }

        ICrudSet<SurveyCompleted> ISurveyPersistence.SurveysCompleted
        {
            get
            {
                return new CrudSetDecorator<SurveyCompleted>(this.SurveysCompleted);
            }
        }

        ICrudSet<Disturbance> ISurveyPersistence.Disturbances
        {
            get
            {
                return new CrudSetDecorator<Disturbance>(this.Disturbances);
            }
        }
        ICrudSet<Observation> ISurveyPersistence.Observations
        {
            get
            {
                return new CrudSetDecorator<Observation>(this.Observations);
            }
        }

        IEnumerable<ForagingSurveyExportItem> ISurveyPersistence.ForagingSurveyExport
        {
            get
            {
                // Because we don't need EF to handle this stored procedure, just map it with Dapper
                return this.Database.Connection.Query<ForagingSurveyExportItem>("EXEC dbo.ExportForagingSurveyResults");
            }
        }

        IEnumerable<RookeryCensusExportItem> ISurveyPersistence.RookeryCensusExport
        {
            get
            {
                // Because we don't need EF to handle this procedure, just map it with Dapper
                return this.Database.Connection.Query<RookeryCensusExportItem>("EXEC dbo.ExportRookeryCensusResults");
            }
        }
        #endregion


        #region Specialized Queries for IWorkLogPersistence

        public IEnumerable<WorkLogReportRecord> GetWorkLogReportRecords()
        {
            return WorkLogReportRecords.AsNoTracking();
        }

        public IEnumerable<WorkLogReportRecord> GetWorkLogReportRecords(int userId)
        {
            return WorkLogReportRecords
                .Where(x => x.UserId == userId)
                .AsNoTracking();
        }

        ICrudSet<User> IWorkLogPersistence.Users
        {
            get
            {
                return new CrudSetDecorator<User>(Users);
            }
        }
        #endregion


        #region Specialized for IBirdSpeciesPersistence
        ICrudSet<SurveyType> IBirdSpeciesPersistence.SurveyTypes
        {
            get
            {
                return new CrudSetDecorator<SurveyType>(SurveyTypes);
            }
        }
        #endregion


        #region IModifiable
        IDbEntityEntryDecorator IModifiable.Entry(object entity)
        {
            return new DbEntityEntryDecorator(this.Entry(entity));
        }
        #endregion

        #region IEnumRepository

        public async Task<IReadOnlyCollection<WindDirection>> GetWindDirections()
        {
            return await this.WindDirections.ToListAsync();
        }

        public async Task<IReadOnlyCollection<WindSpeed>> GetWindSpeeds()
        {
            return await this.WindSpeed.ToListAsync();
        }

        public async Task<IReadOnlyCollection<Weather>> GetWeather()
        {
            return await this.Weather.ToListAsync();
        }

        public async Task<IReadOnlyCollection<WaterHeight>> GetWaterHeights()
        {
            return await this.WaterHeights.ToListAsync();
        }
        
        public async Task<IReadOnlyCollection<DisturbanceType>> GetDisturbanceTypes()
        {
            return await this.DisturbanceTypes.ToListAsync();
        }

        public async Task<IReadOnlyCollection<HabitatType>> GetHabitatTypes()
        {
            return await this.HabitatTypes.ToListAsync();
        }

        public async Task<IReadOnlyCollection<FeedingSuccessRate>> GetFeedingSuccessRates()
        {
            return await this.FeedingSuccessRates.ToListAsync();
        }

        public async Task<IReadOnlyCollection<SurveyActivity>> GetSurveyActivities()
        {
            return await this.SurveyActivities.ToListAsync();
        }

        public async Task<IReadOnlyCollection<SiteAssessment>> GetSiteAssessments()
        {
            return await this.SiteAssessments.ToListAsync();
        }

        public async Task<IReadOnlyCollection<VantagePoint>> GetVantagePoints()
        {
            return await this.VantagePoints.ToListAsync();
        }

        public async Task<IReadOnlyCollection<AccessPoint>> GetAccessPoints()
        {
            return await this.AccessPoints.ToListAsync();
        }

        #endregion  

        #region EF DbSets 

        public DbSet<Location> Locations { get; set; }

        public DbSet<WorkLog> WorkLogs { get; set; }

        public DbSet<WorkType> WorkTypes { get; set; }

        public DbSet<BirdSpecies> BirdSpecies { get; set; }

        public DbSet<SurveyType> SurveyTypes { get; set; }

        public DbSet<SurveyCompleted> SurveysCompleted { get; set; }

        public DbSet<SurveyPending> SurveysPending { get; set; }

        public DbSet<Disturbance> Disturbances { get; set; }

        public DbSet<DisturbanceType> DisturbanceTypes { get; set; }

        public DbSet<Observation> Observations { get; set; }

        public DbSet<Weather> Weather { get; set; }

        public DbSet<HabitatType> HabitatTypes { get; set; }

        public DbSet<AccessPoint> AccessPoints { get; set; }

        public DbSet<SiteAssessment> SiteAssessments { get; set; }

        public DbSet<VantagePoint> VantagePoints { get; set; }

        public DbSet<FeedingSuccessRate> FeedingSuccessRates { get; set; }

        public DbSet<SurveyActivity> SurveyActivities { get; set; }

        public DbSet<WaterHeight> WaterHeights { get; set; }

        public DbSet<WorkLogReportRecord> WorkLogReportRecords { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<WindDirection> WindDirections { get; set; }

        public DbSet<WindSpeed> WindSpeed { get; set; }
        #endregion


        public DataCollectionContext()
            : base(Properties.Settings.Default.ConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }


        public void Add<TEntity>(TEntity entity)
            where TEntity: class
        {
            Set<TEntity>().Add(entity);

            //persistenceLayer.Entry(input).State = System.Data.Entity.EntityState.Modified;
        }

        public void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            Set<TEntity>().Attach(entity);
            Entry(entity).State = EntityState.Modified;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>().ToTable("Locations");
            modelBuilder.Entity<WorkType>().ToTable("WorkType");
            modelBuilder.Entity<WorkLog>().ToTable("WorkLog");

            // Entity framework truncates decimal data types to two places unless you tell it otherwise. How dumb.
            // Geographic coordinates need 6 digits to the right of the decimal point, and at most 3 to the left.
            modelBuilder.Entity<Location>().Property(x => x.Longitude).HasPrecision(9, 6);
            modelBuilder.Entity<Location>().Property(x => x.Latitude).HasPrecision(9, 6);


            modelBuilder.Entity<SurveyType>().ToTable("SurveyType");
            modelBuilder.Entity<BirdSpecies>()
                .ToTable("BirdSpecies")
                .HasMany(x => x.SurveyTypes)
                .WithMany(x => x.BirdSpecies)
                .Map(m =>
               {
                   m.MapLeftKey("BirdSpeciesId");
                   m.MapRightKey("SurveyTypeId");
                   m.ToTable("SurveyType_BirdSpecies");
               });

            modelBuilder.Entity<DisturbanceType>()
                .ToTable("DisturbanceTypes");

            modelBuilder.Entity<Disturbance>()
                .ToTable("Disturbances")
                .HasRequired(x => x.DisturbanceType)
                .WithMany(x => x.Disturbances);

            modelBuilder.Entity<SurveyCompleted>()
                .ToTable("SurveyCompleted")
                .Ignore(survey => survey.CreateDate);

            modelBuilder.Entity<Weather>().ToTable("Weather");

            modelBuilder.Entity<SurveyPending>()
                .ToTable("SurveyPending")
                .Ignore(survey => survey.CreateDate);

            modelBuilder.Entity<HabitatType>().ToTable("HabitatTypes");
            modelBuilder.Entity<SurveyActivity>().ToTable("SurveyActivities");
            modelBuilder.Entity<SiteAssessment>().ToTable("SiteAssessments");
            modelBuilder.Entity<VantagePoint>().ToTable("VantagePoints");
            modelBuilder.Entity<AccessPoint>().ToTable("AccessPoints");
            modelBuilder.Entity<FeedingSuccessRate>().ToTable("FeedingSuccessRates");

            modelBuilder.Entity<WaterHeight>().ToTable("WaterHeights");

            modelBuilder.Entity<WorkLogReportRecord>().ToTable("WorkLogReport")
                .HasKey(x => x.Id);        }


        public static DataCollectionContext Create()
        {
            return new DataCollectionContext();
        }

    }
}