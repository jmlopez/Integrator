using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Integrator.HelloWorld.Domain.Persistence;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using ProAceFx.Core.Configuration;
using StructureMap.Configuration.DSL;

namespace Integrator.HelloWorld.Configuration
{
    public class HelloWorldStructureMapRegistry : Registry
    {
        public HelloWorldStructureMapRegistry()
        {
            IncludeRegistry<ProAceCoreRegistry>();

            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("HelloWorld")).ShowSql())
                .Mappings(m =>
                              {
                                  m.FluentMappings.AddFromAssemblyOf<MapMarker>();
                                  m.FluentMappings.Conventions.AddFromAssemblyOf<CollectionAccessConvention>();
                              })
                .ExposeConfiguration(cfg =>
                                         {
                                             var schemaExport = new SchemaExport(cfg);
                                             schemaExport.Drop(true, true);
                                             schemaExport.Create(true, true);
                                             For<NHibernate.Cfg.Configuration>().Use(cfg);
                                         })
                .BuildSessionFactory();

            For<ISessionFactory>()
                .Singleton()
                .Use(sessionFactory);

            For<ISession>()
                .Use(ctx => ctx.GetInstance<ISessionFactory>().OpenSession());
        }
    }
}