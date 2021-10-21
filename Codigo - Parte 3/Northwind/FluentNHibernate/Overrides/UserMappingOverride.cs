using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
using Northwind.Entities;

namespace Northwind.FluentNHibernate.Overrides
{
    public class UserMappingOverride : IAutoMappingOverride<User>
    {
        public void Override(AutoMapping<User> mapping)
        {
            mapping.Cache.ReadWrite();

            mapping.Id(u => u.Id)
                .GeneratedBy.Foreign("Employee");

            //mapping.Id(u => u.Id)
            //    .GeneratedBy.HiLo("100");

            mapping.Map(u => u.UserName)
                .Not.Nullable()
                .Length(20);

            mapping.Map(u => u.PasswordHash)
                .Not.Nullable();

            mapping.HasOne(u => u.Employee)
                .Constrained()
                .ForeignKey("test");

            //mapping.References(u => u.Employee)
            //    .Unique()
            //    .Cascade.SaveUpdate();

            mapping.HasManyToMany(u => u.UserGroups)
                .Table("UserUserGroup")
                .ParentKeyColumn("UserId")
                .ChildKeyColumn("UserGroupId")
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.SaveUpdate()
                .Cache.ReadWrite();
        }
    }
}