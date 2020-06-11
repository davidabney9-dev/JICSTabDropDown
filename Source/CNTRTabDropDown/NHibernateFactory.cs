using System.Collections.Generic;
using Jenzabar.Common.Configuration;
using Jenzabar.Portal.Framework.NHibernateFWK;
using NHibernate;
using NHibernate.Cfg;
using System.IO;
using System.Xml;

namespace CNTRTabDropDown
{
    class NHibernateFactory : ICustomSessionFactory
    {
        public ISessionFactory GetSessionFactory()
        {
            var cfg = new Configuration().Configure(XmlReader.Create(new StringReader("<hibernate-configuration xmlns=\"urn:nhibernate-configuration-2.2\"><session-factory></session-factory></hibernate-configuration>")));
            SetNHibernateConfigProperties(cfg);
            return cfg.BuildSessionFactory();
        }

        private void SetNHibernateConfigProperties(Configuration cfg)
        {
            var newProperties = new Dictionary<string, string>
                {
                    {"connection.connection_string", ConfigSettings.Current.DatabaseConnectionInfo.ConnectionString},
                    {"dialect", "NHibernate.Dialect.MsSql2008Dialect"},
                    {"connection.isolation", "ReadUncommitted"},
                    {"connection.provider", "NHibernate.Connection.DriverConnectionProvider"},
                    {"connection.driver_class", "NHibernate.Driver.SqlClientDriver"},
                    {
                        "proxyfactory.factory_class",
                        //"NHibernate.ByteCode.LinFu.ProxyFactoryFactory, NHibernate.ByteCode.LinFu"
                        "NHibernate.Bytecode.DefaultProxyFactoryFactory, NHibernate" // JICS 8.3
                    }
                };
            cfg.SetProperties(newProperties);
            cfg.AddAssembly("CNTRTabDropDown");
        }
    }
}