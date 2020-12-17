using Microsoft.EntityFrameworkCore;

namespace InstantIssueApp.Helpers
{
    public static class NpgsqlHelpers
    {
        public static ModelBuilder ToSnakeCase(this ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                if (entity.BaseType == null)
                    entity.SetTableName(entity.GetTableName().ToSnakeCase());

                foreach (var property in entity.GetProperties())
                    property.SetColumnName(property.GetColumnName().ToSnakeCase());

                foreach (var key in entity.GetKeys())
                    key.SetName(key.GetName().ToSnakeCase());

                foreach (var key in entity.GetForeignKeys())
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());

                foreach (var index in entity.GetIndexes())
                    index.SetDatabaseName(index.GetDatabaseName().ToSnakeCase());
            }

            return builder;
        }
    }
}