using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Pizzaria.NHibernate.Mappings;

namespace Pizzaria.NHibernate.Helpers
{
    public class NhCastle
    {
        private FluentConfiguration _fluentConfiguration;

        public ISessionFactory InitSessionFactory()
        {
            _fluentConfiguration = Fluently.Configure()
                .Mappings
                (m => m.FluentMappings.AddFromAssemblyOf<PizzaMap>())
                .Database(MsSqlConfiguration.MsSql2008.ShowSql()
                              .IsolationLevel("ReadCommitted")
                              .ConnectionString(c => c.FromConnectionStringWithKey("ConnectionString")).ShowSql()
                );
            return _fluentConfiguration.BuildSessionFactory();
        }

        public void AutoCriarBancoDeDados(ISession session)
        {
            InitSessionFactory();
            var sessionSource = new SessionSource(_fluentConfiguration);
            sessionSource.BuildSchema(session);
            session.Flush();
        }
    }
}